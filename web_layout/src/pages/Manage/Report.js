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

  //  CRATE NEW REPORT INFO
  const handleFormSubmit = (event) => {
    event.preventDefault();
    const formData = {
      reportId: event.target.elements.reportId.value,
      employeeId: event.target.elements.employeeId.value,
      reason: event.target.elements.reason.value,
      content: event.target.elements.content.value,
      issueDate: event.target.elements.content.value,
      status: event.target.elements.status.checked,
    };

    if (!formData.reportId) {
      setValidationError('Report ID is required');
      return;
    }

    if (!reportIdPattern.test(formData.reportId)) {
      setValidationError('Report ID must follow RP###### format');
      return;
    }

    if (!formData.reason) {
      setValidationError('Reason type is required');
      return;
    }

    if (!formData.content) {
      setValidationError('Content needed is required');
      return;
    }

    fetch('https://localhost:7220/api/Report/create', {
      method: 'POST',
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
      .then(report => {
        setData([...data, report]);
        setShowForm(false);
        setValidationError('');
        console.log('Report form created successfully');
      })
      .catch(error => {
        console.error('Error submitting form:', error);
        setValidationError('An error occurred while submitting the form');
      });
    console.log(event.target.elements)
  };



  //  UPDATE NEW REPORT
  const handleUpdate = (event) => {
    event.preventDefault();
    const formData = {
      reportId: updateReport.reportId,
      status: event.target.elements.status.value === 'true',
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
    <div className="manager" style={{ position: "relative" }}>
      <div className='row addbtn' ><button className='btn_create' onClick={() => setShowForm(true)}>Add Report</button></div>
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
                  {report.status
                    ? 'Approved'
                    : 'Declined'}
                </td>
                <td>
                  <button onClick={() => handleEdit(report)}>Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {showCreateForm && (
        <div className="form-container">
          <form className="form" onSubmit={handleFormSubmit}>
            <h3>Create Report</h3>

            {validationError && (
              <div className="error-message-fadeout">
                {validationError}
              </div>
            )}

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Report ID:</label>
                <input type="text" name="reportId" placeholder='AL######' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Employee ID:</label>
                <input type="text" name="reportId" placeholder='EP######' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Reason:</label>
                <input type="text" name="reportType" placeholder='Type in your reason' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Content:</label>
                <input type="text" name="reportType" placeholder='Type in what you want...' />
              </div>
            </div>

            <div className='row'>
              <div className="col-3 mt-3"></div>
              <div className="col-6 mt-3">
                <label>Status:</label>
                <select name="status" defaultValue={true} onChange={event => console.log(event.target.value)}>
                  <option value={true}>Approved</option>
                  <option value={false}>Declined</option>
                </select>
              </div>
              <div className="col-3 mt-3"></div>
            </div>

            <div className='row butt'>
              <div className="col-5 mt-3">
                <button type="submit">Submit</button>
              </div>
              <div className="col-5 mt-3">
                <button onClick={() => setShowForm(false)}>Cancel</button>
              </div>
            </div>

          </form>
        </div>
      )}

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
                {/* <label>Status:</label>
                <input type="text" name="status" defaultValue={updateReport.status} /> */}
                <label>Status:</label>
                <select name="status" defaultValue={updateReport.status ? 'true' : 'false'} onChange={event => console.log(event.target.value)}>
                  <option value="true">Active</option>
                  <option value="false">Inactive</option>
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