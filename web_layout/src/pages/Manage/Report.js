import React from 'react';
import { useEffect, useState, useRef } from 'react';
import { MDBDataTableV5 } from 'mdbreact';

function Report(props) {
  const [data, setData] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));
  const [updateReport, setUpdateReport] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [validationError, setValidationError] = useState('');
  const [selectedReport, setSelectedReport] = useState(null);
  const [employeeNames, setEmployeeName] = useState([]);

  const handleEdit = (report) => {
    setUpdateReport(report);
    setShowUpdateForm(true);
  };

  const timeoutRef = useRef(null);

  const handleDoubleClick = (report) => {
    setSelectedReport(report);
  };

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

  // Fetch employee
  useEffect(() => {
    fetch('https://localhost:7220/api/Employee/employees', {
      method: "GET",
      headers: {
        'Content-Type': "application/json",
        'Authorization': `Bearer ${token.token}`,
      },
    })
      .then((response) => {
        if (response.ok) {
          return response.json();
        } else {
          throw new Error("API response was not ok.");
        }
      })
      .then((employees) => {
        setEmployeeName(employees);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
      });
  }, []);

  //  UPDATE REPORT
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

      <div className="card mb-3">
        <div className="card-body">
          <div className="row">
            <div className="col-2">
              <h3 className="mb-0">Report Details:</h3>
            </div>
            <div className="col-10 text-secondary">
              {selectedReport && (
                <div>

                  <div className="row">
                    <div className="col-sm-6">
                      <h4>Report ID:</h4>
                      <p>{selectedReport.reportId}</p>
                    </div>
                    <div className="col-sm-3">
                      <h4>Employee Name:</h4>
                      <p>
                        {employeeNames.find(employee => employee.employeeId === selectedReport.employeeId)
                          ? `${employeeNames.find(employee => employee.employeeId === selectedReport.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === selectedReport.employeeId).lastName}`
                          : 'Unknown'}
                      </p>
                    </div>
                    <div className="col-sm-3">
                      <h4>Employee ID:</h4>
                      <p>{selectedReport.employeeId}</p>
                    </div>
                  </div>
                  <hr />

                  <div className="row">
                    <div className="col-sm-6">
                      <h4>Reason:</h4>
                      <p>{selectedReport.reason}</p>
                    </div>
                    <div className="col-sm-6">
                      <h4>Issue Date:</h4>
                      <p>{selectedReport.issueDate}</p>
                    </div>
                  </div>
                  <hr />


                  <h4>Content:</h4>
                  <p>{selectedReport.content}</p>
                  <hr />

                  <h4>Status:</h4>
                  <p>{selectedReport.status}</p>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>

      <div className='row'>
        <MDBDataTableV5
          className='custom-table'
          data={{
            columns: [
              {
                label: 'Report ID',
                field: 'reportId',
                width: 150
              },
              {
                label: 'Employee Name',
                field: 'employeeName',
                width: 150
              },
              {
                label: 'Reason',
                field: 'reason',
                width: 200
              },
              {
                label: 'Content',
                field: 'content',
                width: 200
              },
              {
                label: 'Issue Date',
                field: 'issueDate',
                width: 150
              },
              {
                label: 'Status',
                field: 'status',
                width: 150
              },
              {
                label: 'Options',
                field: 'options',
                sort: 'disabled',
                width: 100
              }
            ],
            rows: data.map(report => ({
              reportId: report.reportId,
              employeeName: employeeNames.find(employee => employee.employeeId === report.employeeId)
                ? `${employeeNames.find(employee => employee.employeeId === report.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === report.employeeId).lastName}`
                : 'Unknown',
              reason: report.reason,
              content: report.content.length > 30 ? `${report.content.slice(0, 30)}...` : report.content,
              issueDate: report.issueDate,
              status: report.status,
              options: (
                <button onClick={() => handleEdit(report)}>Edit</button>
              ),
              clickEvent: () => handleDoubleClick(report)
            }))
          }}
          hover
          entriesOptions={[5, 10, 20]}
          entries={5}
          pagesAmount={5}
          searchTop
          searchBottom={false}
          tbodyCustomRow={(row, rowIndex) => {
            return {
              onDoubleClick: row.clickEvent
            };
          }}
        />
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