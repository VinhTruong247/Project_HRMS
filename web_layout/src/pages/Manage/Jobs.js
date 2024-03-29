import React from 'react';
import { useEffect, useState, useRef } from 'react';
import { MDBDataTableV5 } from 'mdbreact';

function Jobs(props) {
  const [data, setData] = useState([]);
  const [showCreateForm, setShowForm] = useState(false);
  const token = JSON.parse(localStorage.getItem('jwtToken'));
  const [updateJob, setUpdateJob] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [validationError, setValidationError] = useState('');
  const [selectedReport, setSelectedReport] = useState(null);

  const jobIdPattern = /^JB\d{6}$/;
  const handleEdit = (job) => {
    setUpdateJob(job);
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

  //  Get info of Job
  useEffect(() => {
    fetch('https://localhost:7220/api/Job/jobs', {
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
      .then(jobs => {
        setData(jobs)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

  //  CRATE NEW JOB INFO
  const handleFormSubmit = (event) => {
    event.preventDefault();
    const formData = {
      jobId: event.target.elements.jobId.value,
      jobTitle: event.target.elements.jobTitle.value,
      jobDescription: event.target.elements.jobDescription.value,
      baseSalaryPerHour: parseFloat(event.target.elements.baseSalaryPerHour.value.replace(/,/g, '')),
      status: event.target.elements.status.checked,
    };

    const isDuplicateJobId = data.some(job => job.jobId === formData.jobId);

    if (!formData.jobId) {
      setValidationError('Job ID is required');
      return;
    }

    if (!jobIdPattern.test(formData.jobId)) {
      setValidationError('Job ID must follow JB###### format');
      return;
    }

    if (isDuplicateJobId) {
      setValidationError('Job ID is already taken');
      return;
    }

    if (!formData.jobTitle) {
      setValidationError('Job title is required');
      return;
    }

    if (!formData.jobDescription) {
      setValidationError('Description is required');
      return;
    }

    if (!formData.baseSalaryPerHour) {
      setValidationError('Base salary needed is required');
      return;
    }

    if (isNaN(formData.baseSalaryPerHour)) {
      setValidationError('Base salary must be in number format');
      return;
    }

    if (formData.baseSalaryPerHour < 0) {
      setValidationError('Input cannot be negative')
      return;
    }

    fetch('https://localhost:7220/api/Job/create', {
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
      .then(job => {
        setData([...data, job]);
        setShowForm(false);
        setValidationError('');
        console.log('Job information created successfully');
      })
      .catch(error => {
        console.error('Error submitting form:', error);
        setValidationError('An error occurred while submitting the form');
      });
    console.log(event.target.elements)
  };



  //  UPDATE NEW JOB
  const handleUpdate = (event) => {
    event.preventDefault();
    const formData = {
      jobId: updateJob.jobId,
      jobTitle: event.target.elements.jobTitle.value,
      jobDescription: event.target.elements.jobDescription.value,
      baseSalaryPerHour: parseFloat(event.target.elements.baseSalaryPerHour.value.replace(/,/g, '')),
      status: event.target.elements.status.value === 'true',
    };

    if (!formData.jobTitle) {
      setValidationError('Job title is required');
      return;
    }

    if (!formData.jobDescription) {
      setValidationError('Description is required');
      return;
    }

    if (!formData.baseSalaryPerHour) {
      setValidationError('Base Salary is required');
      return;
    }

    if (isNaN(formData.baseSalaryPerHour)) {
      setValidationError('Base salary must be in number format');
      return;
    }

    if (formData.baseSalaryPerHour < 0) {
      setValidationError('Input cannot be negative')
      return;
    }

    fetch(`https://localhost:7220/api/Job/update/job/${updateJob.jobId}`, {
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
      .then(updatedJob => {
        const updatedData = data.map(job => {
          if (job.jobId === updateJob.jobId) {
            return updatedJob;
          } else {
            return job;
          }
        });
        setData(updatedData);
        setShowUpdateForm(false);
        setUpdateJob(null);
        console.log('Job information updated successfully');
      })
      .catch(error => {
        console.error('Error submitting form:', error);
        setValidationError('An error occurred while updating job information');
      });
  };

  return (
    <div className="manager" style={{ position: "relative" }}>
      <div className='row addbtn'><button className='btn_create' onClick={() => setShowForm(true)}>Add Job</button></div>

      <div className="card mb-3">
        <div className="card-body">
          <div className="row">
            <div className="col-2">
              <h3 className="mb-0">Job Details:</h3>
            </div>
            <div className="col-10 text-secondary">
              {selectedReport && (
                <div>
                  <div className="row">
                    <div className="col-sm-4">
                      <h4>Job ID:</h4>
                      <p>{selectedReport.jobId}</p>
                    </div>
                    <div className="col-sm-8">
                      <h4>Job Title:</h4>
                      <p>{selectedReport.jobTitle}</p>
                    </div>
                  </div>
                  <hr />

                  <div className="row">
                    <div className="col-sm-4">
                      <h4>Base Salary per Hour:</h4>
                      <p>{selectedReport.baseSalaryPerHour.toLocaleString()}</p>
                    </div>
                    <div className="col-sm-8">
                      <h4>Bonus:</h4>
                      <p>{selectedReport.bonus.toLocaleString()}</p>
                    </div>
                  </div>
                  <hr />

                  <h4>Description:</h4>
                  <p>{selectedReport.jobDescription}</p>
                  <hr />

                  <h4>Status:</h4>
                  <p>
                    {selectedReport.status
                      ? 'Active'
                      : 'Disable'}
                  </p>
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
                label: 'Job ID',
                field: 'jobId',
                width: 150,
              },
              {
                label: 'Job Title',
                field: 'jobTitle',
                width: 150,
              },
              {
                label: 'Description',
                field: 'jobDescription',
                width: 200,
              },
              {
                label: 'Base Salary per Hour',
                field: 'baseSalaryPerHour',
                width: 150,
              },
              {
                label: 'Status',
                field: 'status',
                width: 100,
              },
              {
                label: 'Option',
                field: 'options',
                sort: 'disabled',
                width: 100,
              },
            ],
            rows: data.map((job) => ({
              jobId: job.jobId,
              jobTitle: job.jobTitle,
              jobDescription: job.jobDescription,
              baseSalaryPerHour: job.baseSalaryPerHour.toLocaleString(),
              status: job.status ? 'Active' : 'Disable',
              options: (
                <button onClick={() => handleEdit(job)}>Edit</button>
              ),
              clickEvent: () => handleDoubleClick(job)
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

      {showCreateForm && (
        <div className="form-container">
          <form className="form" onSubmit={handleFormSubmit}>
            <h3>Create Job</h3>

            {validationError && (
              <div className="error-message-fadeout">
                {validationError}
              </div>
            )}

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Job ID:</label>
                <input type="text" name="jobId" placeholder='JB######' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Job Name:</label>
                <input type="text" name="jobTitle" placeholder='Job Title' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Description:</label>
                <input type="text" name="jobDescription" placeholder='abcxyz' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Base Salary:</label>
                <input
                  type="text"
                  name="baseSalaryPerHour"
                  placeholder='Number'
                  onChange={(e) => {
                    const value = parseInt(e.target.value.replace(/,/g, ''));
                    if (!isNaN(value)) {
                      e.target.value = value.toLocaleString();
                    }
                  }}
                />
              </div>
            </div>

            <div className='row'>
              <div className="col-3 mt-3"></div>
              <div className="col-6 mt-3">
                <label>Status:</label>
                <select name="status" defaultValue={true} onChange={event => console.log(event.target.value)}>
                  <option value={true}>Active</option>
                  <option value={false}>Inactive</option>
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
            <h3>Edit Job Information (ID: {updateJob.jobId})</h3>

            {validationError && (
              <div className="error-message-fadeout">
                {validationError}
              </div>
            )}

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Job Name:</label>
                <input type="text" name="jobTitle" defaultValue={updateJob.jobTitle} />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Description:</label>
                <input type="text" name="jobDescription" defaultValue={updateJob.jobDescription} />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Base Salary:</label>
                <input
                  type="text"
                  name="baseSalaryPerHour"
                  defaultValue={updateJob.baseSalaryPerHour.toLocaleString()}
                  onChange={(e) => {
                    const value = parseInt(e.target.value.replace(/,/g, ''));
                    if (!isNaN(value)) {
                      e.target.value = value.toLocaleString();
                    }
                  }}
                />
              </div>
            </div>

            <div className='row'>
              <div className="col-3 mt-3"></div>
              <div className="col-6 mt-3">
                {/* <label>Status:</label>
                <input type="text" name="status" defaultValue={updateJob.status} /> */}
                <label>Status:</label>
                <select name="status" defaultValue={updateJob.status ? 'true' : 'false'} onChange={event => console.log(event.target.value)}>
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

export default Jobs;
