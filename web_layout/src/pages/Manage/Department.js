import React from 'react';
import { useEffect, useState, useRef } from 'react';

function Department(props) {
    const [data, setData] = useState([]);
    const [showCreateForm, setShowForm] = useState(false);
    const token = JSON.parse(localStorage.getItem('jwtToken'));
    const [updateDepartment, setUpdateDepartment] = useState(null);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [validationError, setValidationError] = useState('');
    const departmentIdPattern = /^DP\d{6}$/;
    const handleEdit = (department) => {
        setUpdateDepartment(department);
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

    //  Get info of Department
    useEffect(() => {
        fetch('https://localhost:7220/api/Department/departments', {
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
            .then(departments => {
                setData(departments)
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }, []);



    //  CRATE NEW DEPARTMENT
    const handleFormSubmit = (event) => {
        event.preventDefault();
        const formData = {
            departmentId: event.target.elements.departmentId.value,
            departmentName: event.target.elements.departmentName.value,
            description: event.target.elements.description.value,
            status: Boolean(event.target.elements.status.value),
        };

        if (!formData.departmentId) {
            setValidationError('Department ID is required');
            return;
        }

        if (!departmentIdPattern.test(formData.departmentId)) {
            setValidationError('Department ID must follow DP###### format');
            return;
        }

        if (!formData.departmentName) {
            setValidationError('Department name is required');
            return;
        }

        if (!formData.description) {
            setValidationError('Description is required');
            return;
        }

        fetch('https://localhost:7220/api/Department/create', {
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
            .then(department => {
                setData([...data, department]);
                setShowForm(false);
                setValidationError('');
                console.log('Department created successfully');
            })
            .catch(error => {
                console.error('Error submitting form:', error);
                setValidationError('An error occurred while submitting the form');
            });
        console.log(event.target.elements)
    };



    //  UPDATE NEW Department
    const handleUpdate = (event) => {
        event.preventDefault();
        const formData = {
            departmentId: updateDepartment.departmentId,
            departmentName: event.target.elements.departmentName.value,
            description: event.target.elements.description.value,
            status: Boolean(event.target.elements.status.value),
        };

        if (!formData.departmentId) {
            setValidationError('Department ID is required');
            return;
        }

        if (!formData.departmentName) {
            setValidationError('Department name is required');
            return;
        }

        if (!formData.description) {
            setValidationError('Description is required');
            return;
        }

        fetch(`https://localhost:7220/api/Department/update/department/${updateDepartment.departmentId}`, {
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
            .then(updatedDepartment => {
                const updatedData = data.map(department => {
                    if (department.departmentId === updatedDepartment.departmentId) {
                        return updatedDepartment;
                    } else {
                        return department;
                    }
                });
                setData(updatedData);
                setShowUpdateForm(false);
                setUpdateDepartment(null);
                console.log('Department updated successfully');
            })
            .catch(error => {
                console.error('Error submitting form:', error);
                setValidationError('An error occurred while updating department information');
            });
    };

    return (
        <div className="manager" style={{ position: "relative" }}>
            <button className='btn_create' onClick={() => setShowForm(true)}>Add Department</button>
            <div className='row'>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Department ID</th>
                            <th>Department Name</th>
                            <th>Description</th>
                            <th>Status</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(department => (
                            <tr key={department.departmentId}>
                                <td>{department.departmentId}</td>
                                <td>{department.departmentName}</td>
                                <td>{department.description}</td>
                                <td>
                                    {department.status
                                        ? 'Active'
                                        : 'Inactive'}
                                </td>
                                <td>
                                    <button onClick={() => handleEdit(department)}>Edit</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {showCreateForm && (
                <div className="form-container">
                    <form className="form" onSubmit={handleFormSubmit}>
                        <h3>Create Department</h3>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}
                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Department ID:</label>
                                <input type="text" name="departmentId" placeholder='DP######' />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Department Name:</label>
                                <input type="text" name="departmentName" placeholder='Department Name' />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Description:</label>
                                <input type="text" name="description" placeholder='abcxyz' />
                            </div>
                        </div>
                        <div className='row'>
                            <div className="col-3 mt-3"></div>
                            <div className="col-6 mt-3">
                                <label>Status:</label>
                                <select name="status" defaultValue={true}>
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
                        <h3>Edit Department (ID: {updateDepartment.departmentId})</h3>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}

                        <div className='row name'>
                            <div className="col-12 mt-3">
                                <label>Department Name:</label>
                                <input type="text" name="departmentName" defaultValue={updateDepartment.departmentName} />
                            </div>
                        </div>
                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Description:</label>
                                <input type="text" name="description" defaultValue={updateDepartment.description} />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-3 mt-3"></div>
                            <div className="col-6 mt-3">
                                <label>Status:</label>
                                <input type="text" name="status" defaultValue={updateDepartment.status} />
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

export default Department;
