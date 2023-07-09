import React from 'react';
import { useEffect, useState, useRef } from 'react';

function Jobs(props) {
  const [data, setData] = useState([]);
  const [showCreateForm, setShowForm] = useState(false);
  const token = JSON.parse(localStorage.getItem('jwtToken'));
  const [updateJob, setUpdateJob] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [validationError, setValidationError] = useState('');
  const handleEdit = (job) => {
    setUpdateJob(job);
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
        console.log(jobs)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, [token]);



  //  CRATE NEW JOB INFO
  const handleFormSubmit = (event) => {
    event.preventDefault();
    const formData = {
      jobId: event.target.elements.jobId.value,
      jobTitle: event.target.elements.jobTitle.value,
      jobDescription: event.target.elements.jobDescription.value,
      status: event.target.elements.status.value,
    };

    if (!formData.jobId) {
      setValidationError('Job ID is required');
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

    if (!formData.status) {
      setValidationError('Status is required');
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
      jobId: event.target.elements.jobId.value,
      jobTitle: event.target.elements.jobTitle.value,
      jobDescription: event.target.elements.jobDescription.value,
      status: event.target.elements.status.value,
    };

    if (!formData.jobId) {
      setValidationError('Job ID is required');
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

    if (!formData.status) {
      setValidationError('Status is required');
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
      <button className='btn_create' onClick={() => setShowForm(true)}>Add Job</button>
      <div className='row'>
        <table className='table'>
          <thead>
            <tr>
              <th>Job ID</th>
              <th>Job Name</th>
              <th>Job Description</th>
              <th>Status</th>
              <th>Options</th>
            </tr>
          </thead>
          <tbody>
            {data.map(job => (
              <tr key={job.jobId}>
                <td>{job.jobId}</td>
                <td>{job.jobTitle}</td>
                <td>{job.jobDescription}</td>
                <td>
                  {job.status
                    ? 'Active'
                    : 'Inactive'}
                </td>
                <td>
                  <button onClick={() => handleEdit(job)}>Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
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
                <label>Status:</label>
                <input type="text" name="status" placeholder='Active or Disable' />
                {/* <select className="form-select" name="status">
                                    <option value="Active">Active</option>
                                    <option value="Disable">Disable</option>
                                </select> */}
              </div>
            </div>

            <div className='row btn'>
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
                <label>Status:</label>
                <input type="text" name="status" defaultValue={updateJob.status} />
              </div>
            </div>

            <div className='row btn'>
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