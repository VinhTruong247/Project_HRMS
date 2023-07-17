import React from 'react';
import { useEffect, useState, useRef } from 'react';

function Report(props) {
  const [data, setData] = useState([]);
  const [showCreateForm, setShowForm] = useState(false);
  const token = JSON.parse(localStorage.getItem('jwtToken'));
  const [updateReport, setUpdateReport] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [validationError, setValidationError] = useState('');
  const reportIdPattern = /^RP\d{6}$/;
  const handleEdit = (report) => {
    setUpdateReport(report);
    setShowUpdateForm(true);
  };

  const timeoutRef = useRef(null);

  useEffect(() => {
    if (validationError) {
      timeoutRef.current = setTimeout(() => {
        setValidationError('');
      }, 3000);
    }
    return () => {
      clearTimeout(timeoutRef.current);
    };
  }, [validationError]);

  //  Get info of Report
  useEffect(() => {
    fetch('https://localhost:7220/api/Report/get/reports', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token.token}`
      }
    })
      .then(response => {
        if (response.ok) {
          return response.json();
        } else {
          throw new Error('Api response was not ok.');
        }
      })
      .then(reports => {
        setData(reports)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

  //  UPDATE NEW REPORT
  const handleUpdate = (event) => {
    event.preventDefault();
    const formData = {
      reportId: updateReport.reportId,
      status: event.target.elements.status.value,
    };

    fetch(`https://localhost:7220/api/Report/update/report/${updateReport.employeeId}/${updateReport.reportId}/`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token.token}`
      },
      body: JSON.stringify(formData)
    })
      .then(response => {
        if (response.ok) {
          return response.json();
        } else {
          throw new Error('Api response was not ok.');
        }
      })
      .then(updateReport => {
        const updatedData = data.map(report => {
          if (report.reportId === updateReport.reportId) {
            return updateReport;
          } else {
            return report;
          }
        });
        setData(updatedData);
        setShowUpdateForm(false);
        setUpdateReport(null);
        console.log('Report information updated successfully');
      })
      .catch(error => {
        console.error('Error submitting form:', error);
        setValidationError('An error occurred while updating report information');
      });
  };

  return (
    <div className="manager" style={{ position: "relative", marginTop: '2.5rem' }}>
      <div className='row'>
        <table className='table'>
          <thead>
            <tr>
              <th>Report ID</th>
              <th>Employee ID</th>
              <th>Reason</th>
              <th>Content</th>
              <th>Issue Date</th>
              <th>Status</th>
              <th>Options</th>
            </tr>
          </thead>
          <tbody>
            {data.map(report => (
              <tr key={report.reportId}>
                <td>{report.reportId}</td>
                <td>{report.employeeId}</td>
                <td>{report.reason}</td>
                <td>{report.content}</td>
                <td>{report.issueDate}</td>
                <td>
                  {report.status}
                </td>
                <td>
                  <button onClick={() => handleEdit(report)}>Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {showUpdateForm && (
        <div className="form-container">
          <form className="form" onSubmit={handleUpdate}>
            <h3>Edit Report Information (ID: {updateReport.reportId})</h3>

            {validationError && (
              <div className="error-message-fadeout">
                {validationError}
              </div>
            )}

            <div className='row'>
              <div className="col-3 mt-3"></div>
              <div className="col-6 mt-3">
                <label>Status:</label>
                <select name="status" defaultValue={updateReport.status} onChange={event => console.log(event.target.value)}>
                  <option value="Approved">Approved</option>
                  <option value="Pending">Pending</option>
                  <option value="Declined">Declined</option>
                </select>

              </div>
              <div className="col-3 mt-3"></div>
            </div>

            <div className='row butt'>
              <div className="col-5 mt-3">
                <button type="submit">Update</button>
              </div>
              <div className="col-5 mt-3">
                <button type="button" onClick={() => setShowUpdateForm(false)}>Cancel</button>
              </div>
            </div>

          </form>
        </div>
      )}
    </div>
  )
}

export default Report;
