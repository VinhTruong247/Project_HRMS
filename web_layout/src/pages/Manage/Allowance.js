import React from 'react';
import { useEffect, useState, useRef } from 'react';
import { MDBDataTableV5 } from 'mdbreact';

function Allowance(props) {
  const [data, setData] = useState([]);
  const [showCreateForm, setShowForm] = useState(false);
  const token = JSON.parse(localStorage.getItem('jwtToken'));
  const [updateAllowance, setUpdateAllowance] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [validationError, setValidationError] = useState('');
  const [filteredData, setFilteredData] = useState(null);

  const handleEdit = (allowance) => {
    setUpdateAllowance(allowance);
    setShowUpdateForm(true);
  };

  const handleFilter = (allowanceType) => {
    if (allowanceType === 'All') {
      setFilteredData(null); // Show all data
    } else {
      const filtered = data.filter((allowance) => allowance.allowanceType === allowanceType);
      setFilteredData(filtered);
    }
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
      allowanceName: event.target.elements.allowanceType.value,
      allowanceType: event.target.elements.allowanceType.value,
      amount: parseFloat(event.target.elements.amount.value.replace(/,/g, '')),
      status: event.target.elements.status.checked,
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

    if (formData.amount < 0) {
      setValidationError('Input cannot be negative')
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
      allowanceId: updateAllowance.allowanceId,
      allowanceName: event.target.elements.allowanceType.value,
      allowanceType: event.target.elements.allowanceType.value,
      amount: parseFloat(event.target.elements.amount.value.replace(/,/g, '')),
      status: event.target.elements.status.value === 'true',
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

    if (formData.amount < 0) {
      setValidationError('Input cannot be negative')
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
      <div className='row addbtn' >
        <button className='btn_create' onClick={() => setShowForm(true)}>Add Allowance</button>
      </div>

      <div className='row'>
        <select onChange={(e) => handleFilter(e.target.value)} style={{ maxWidth: '12%', marginLeft: '1.32rem' }}>
          <option value="All">All</option>
          <option value="Daily">Daily</option>
          <option value="Monthly">Monthly</option>
        </select>
        <MDBDataTableV5
          className='custom-table'
          data={{
            columns: [
              {
                label: 'Allowance ID',
                field: 'allowanceId',
                width: 150,
              },
              {
                label: 'Allowance Name',
                field: 'allowanceName',
                width: 150,
              },
              {
                label: 'Allowance Type',
                field: 'allowanceType',
                width: 150,
              },
              {
                label: 'Amount',
                field: 'amount',
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
            rows: (filteredData || data).map((allowance) => ({
              allowanceId: allowance.allowanceId,
              allowanceName: allowance.allowanceName,
              allowanceType: allowance.allowanceType,
              amount: allowance.amount.toLocaleString(),
              status: allowance.status ? 'Active' : 'Disable',
              options: (
                <button onClick={() => handleEdit(allowance)}>Edit</button>
              ),
              // clickEvent: () => handleDoubleClick(allowance)
            }))
          }}
          hover
          entriesOptions={[5, 10, 20]}
          entries={10}
          pagesAmount={5}
          searchTop
          searchBottom={false}
        // tbodyCustomRow={(row, rowIndex) => {
        //   return {
        //     onDoubleClick: row.clickEvent
        //   };
        // }}
        />
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
                <label>Allowance Name:</label>
                <input type="text" name="allowanceName" placeholder='Name' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Allowance Type:</label>
                <select name="allowanceType" onChange={event => console.log(event.target.value)}>
                  <option value="Daily">Daily</option>
                  <option value="Weekly">Weekly</option>
                  <option value="Monthly">Monthly</option>
                </select>
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
                <label>Allowance Name:</label>
                <input type="text" name="allowanceName" placeholder='Name' />
              </div>
            </div>

            <div className='row'>
              <div className="col-12 mt-3">
                <label>Allowance Type:</label>
                <select name="allowanceType" defaultValue={updateAllowance.allowanceType} onChange={event => console.log(event.target.value)}>
                  <option value="Daily">Daily</option>
                  <option value="Weekly">Weekly</option>
                  <option value="Monthly">Monthly</option>
                </select>
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
                <select name="status" defaultValue={updateAllowance.status ? 'true' : 'false'} onChange={event => console.log(event.target.value)}>
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

export default Allowance;
