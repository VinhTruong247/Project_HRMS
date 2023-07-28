import React from 'react';
import { useEffect, useState, useRef } from 'react';
import { MDBDataTableV5 } from 'mdbreact';

function OverTime(props) {
  const [data, setData] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));
  const [updateOvertime, setUpdateOvertime] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [validationError, setValidationError] = useState('');
  const [employeeNames, setEmployeeName] = useState([]);
  const [selectedOvertime, setSelectedOvertime] = useState(null);

  const handleEdit = (overtime) => {
    setUpdateOvertime(overtime);
    setShowUpdateForm(true);
  };

  const timeoutRef = useRef(null);

  const handleDoubleClick = (overtime) => {
    setSelectedOvertime(overtime);
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

  //  Get info of Overtime
  useEffect(() => {
    fetch('https://localhost:7220/api/Overtime/get/overtime', {
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
      .then(overtimes => {
        setData(overtimes)
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
      overtimeId: updateOvertime.overtimeId,
      status: event.target.elements.status.value,
    };

    fetch(`https://localhost:7220/api/Overtime/update/overtime/${updateOvertime.employeeId}/${updateOvertime.overtimeId}/`, {
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
      .then(updateOvertime => {
        const updatedData = data.map(overtime => {
          if (overtime.overtimeId === updateOvertime.overtimeId) {
            return updateOvertime;
          } else {
            return overtime;
          }
        });
        setData(updatedData);
        setShowUpdateForm(false);
        setUpdateOvertime(null);
        console.log('Overtime information updated successfully');
      })
      .catch(error => {
        console.error('Error submitting form:', error);
        setValidationError('An error occurred while updating overtime information');
      });
  };

  return (
    <div className="manager" style={{ position: "relative", marginTop: '2.5rem' }}>

      <div className="card mb-3">
        <div className="card-body">
          <div className="row">
            <div className="col-2">
              <h3 className="mb-0">Overtime Details:</h3>
            </div>
            <div className="col-10 text-secondary">
              {selectedOvertime && (
                <div>

                  <div className="row">
                    <div className="col-sm-4">
                      <h4>Overtime ID:</h4>
                      <p>{selectedOvertime.overtimeId}</p>
                    </div>
                    <div className="col-sm-8">
                      <h4>Employee Name:</h4>
                      <p>
                        {employeeNames.find(employee => employee.employeeId === selectedOvertime.employeeId)
                          ? `${employeeNames.find(employee => employee.employeeId === selectedOvertime.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === selectedOvertime.employeeId).lastName}`
                          : 'Unknown'}
                      </p>
                    </div>

                  </div>
                  <hr />

                  <div className="row">
                    <div className="col-sm-4">
                      <h4>Day:</h4>
                      <p>{selectedOvertime.day}</p>
                    </div>
                    <div className="col-sm-4">
                      <h4>Overtime Hours</h4>
                      <p>{selectedOvertime.overtimeHours}</p>
                    </div>
                    <div className="col-sm-4">
                      <h4>Overtime Type</h4>
                      <p>{selectedOvertime.overtimeType}</p>
                    </div>
                  </div>
                  <hr />

                  <h4>Status:</h4>
                  <p>{selectedOvertime.status}</p>
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
                label: 'Overtime ID',
                field: 'overtimeId',
                width: 150
              },
              {
                label: 'Employee Name',
                field: 'employeeName',
                width: 150
              },
              {
                label: 'Overtime Type',
                field: 'overtimeType',
                width: 200
              },
              {
                label: 'Day:',
                field: 'day',
                width: 200
              },
              {
                label: 'Overtime Hours',
                field: 'overtimeHours',
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
            rows: data
              .map(overtime => ({
                overtimeId: overtime.overtimeId,
                employeeName: employeeNames.find(employee => employee.employeeId === overtime.employeeId)
                  ? `${employeeNames.find(employee => employee.employeeId === overtime.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === overtime.employeeId).lastName}`
                  : 'Unknown',
                overtimeType: overtime.overtimeType,
                day: overtime.day,
                overtimeHours: overtime.overtimeHours,
                status: overtime.status,
                options: (
                  <button onClick={() => handleEdit(overtime)}>Edit</button>
                ),
                clickEvent: () => handleDoubleClick(overtime)
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
            <h3>Edit Overtime Information (ID: {updateOvertime.overtimeId})</h3>

            {validationError && (
              <div className="error-message-fadeout">
                {validationError}
              </div>
            )}

            <div className='row'>
              <div className="col-3 mt-3"></div>
              <div className="col-6 mt-3">
                <label>Status:</label>
                <select name="status" defaultValue={updateOvertime.status} onChange={event => console.log(event.target.value)}>
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

export default OverTime;