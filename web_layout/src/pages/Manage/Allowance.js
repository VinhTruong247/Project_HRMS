import React from 'react';
import { useEffect, useState, useRef } from 'react';

function Allowance(props) {
  const [data, setData] = useState([]);
  const [showCreateForm, setShowForm] = useState(false);
  const token = JSON.parse(localStorage.getItem('jwtToken'));
  const [updateAllowance, setUpdateAllowance] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [validationError, setValidationError] = useState('');
  const allowanceIdPattern = /^AL\d{6}$/;
  const handleEdit = (allowance) => {
    setUpdateAllowance(allowance);
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

  //  Get info of Allowance
  useEffect(() => {
    fetch('https://localhost:7220/api/Allowance/allowances', {
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
      .then(allowances => {
        setData(allowances)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

  //  CRATE NEW ALLOWANCE INFO
  const handleFormSubmit = (event) => {
    event.preventDefault();
    const formData = {
      allowanceId: event.target.elements.allowanceId.value,
      allowanceType: event.target.elements.allowanceType.value,
      amount: parseFloat(event.target.elements.baseSalaryPerHour.value.replace(/,/g, '')),
      status: Boolean(event.target.elements.status.value),
    };

    if (!formData.allowanceId) {
      setValidationError('Allowance ID is required');
      return;
    }

    if (!allowanceIdPattern.test(formData.allowanceId)) {
      setValidationError('Allowance ID must follow AL###### format');
      return;
    }

    if (!formData.allowanceType) {
      setValidationError('Allowance type is required');
      return;
    }

    if (!formData.amount) {
      setValidationError('Amount needed is required');
      return;
    }

    if (isNaN(formData.amount)) {
      setValidationError('Amount must be in number format');
      return;
    }

    fetch('https://localhost:7220/api/Allowance/create', {
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
      .then(allowance => {
        setData([...data, allowance]);
        setShowForm(false);
        setValidationError('');
        console.log('Allowance information created successfully');
      })
      .catch(error => {
        console.error('Error submitting form:', error);
        setValidationError('An error occurred while submitting the form');
      });
    console.log(event.target.elements)
  };



  //  UPDATE NEW ALLOWANCE
  const handleUpdate = (event) => {
    event.preventDefault();
    const formData = {
      allowanceId: event.target.elements.allowanceId.value,
      allowanceType: event.target.elements.allowanceType.value,
      amount: parseFloat(event.target.elements.baseSalaryPerHour.value.replace(/,/g, '')),
      status: Boolean(event.target.elements.status.value),
    };

    if (!formData.allowanceType) {
      setValidationError('Allowance type is required');
      return;
    }

    if (!formData.amount) {
      setValidationError('Amount needed is required');
      return;
    }

    if (isNaN(formData.amount)) {
      setValidationError('Amount must be in number format');
      return;
    }

    fetch(`https://localhost:7220/api/Allowance/update/allowance/${updateAllowance.allowanceId}`, {
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
      .then(updateAllowance => {
        const updatedData = data.map(allowance => {
          if (allowance.allowanceId === updateAllowance.allowanceId) {
            return updateAllowance;
          } else {
            return allowance;
          }
        });
        setData(updatedData);
        setShowUpdateForm(false);
        setUpdateAllowance(null);
        console.log('Allowance information updated successfully');
      })
      .catch(error => {
        console.error('Error submitting form:', error);
        setValidationError('An error occurred while updating allowance information');
      });
  };

  return (
    <div className="manager" style={{ position: "relative" }}>
      <div className='row addbtn' ><button className='btn_create' onClick={() => setShowForm(true)}>Add Allowance</button></div>
      <div className='row'>
        <table className='table'>
          <thead>
            <tr>
              <th>Allowance ID</th>
              <th>Allowance Type</th>
              <th>Amount</th>
              <th>Status</th>
              <th>Options</th>
            </tr>
          </thead>
          <tbody>
            {data.map(allowance => (
              <tr key={allowance.allowanceId}>
                <td>{allowance.allowanceId}</td>
                <td>{allowance.allowanceType}</td>
                <td>{allowance.amount.toLocaleString()}</td>
                <td>
                  {allowance.status
                    ? 'Active'
                    : 'Inactive'}
                </td>
                <td>
                  <button onClick={() => handleEdit(allowance)}>Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {showCreateForm && (
        <div className="form-container">
          <form className="form" onSubmit={handleFormSubmit}>
            <h3>Create Allowance</h3>

            {validationError && (
              <div className="error-message-fadeout">
                {validationError}
              </div>
            )}

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Allowance ID:</label>
                <input type="text" name="allowanceId" placeholder='AL######' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Allowance Type:</label>
                <input type="text" name="allowanceType" placeholder='Allowance Type' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Amount:</label>
                <input
                  type="text"
                  name="amount"
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
            <h3>Edit Allowance Information (ID: {updateAllowance.allowanceId})</h3>

            {validationError && (
              <div className="error-message-fadeout">
                {validationError}
              </div>
            )}

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Allowance Type:</label>
                <input type="text" name="allowanceType" defaultValue={updateAllowance.allowanceType} />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Amount:</label>
                <input
                  type="text"
                  name="amount"
                  defaultValue={updateAllowance.amount.toLocaleString()}
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
                <input type="text" name="status" defaultValue={updateAllowance.status} /> */}
                <label>Status:</label>
                <select name="status" defaultValue={updateAllowance.status} onChange={event => console.log(event.target.value)}>
                  <option value={true}>Active</option>
                  <option value={false}>Inactive</option>
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

export default Allowance;