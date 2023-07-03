import React from 'react';
import { useEffect, useState, useRef } from 'react';

function Manage(props) {
    const [data, setData] = useState([]);
    const [showCreateForm, setShowForm] = useState(false);
    const token = localStorage.getItem('jwtToken');
    const [updateEmployee, setUpdateEmployee] = useState(null);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [validationError, setValidationError] = useState('');
    const handleEdit = (employee) => {
        setUpdateEmployee(employee);
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

    //  Get info of EMPLOYEE
    useEffect(() => {
        fetch('https://localhost:7220/api/Employee/employees', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Api response was not ok.');
                }
            })
            .then(employees => {
                setData(employees)
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }, [token]);



    //  CRATE NEW EMPLOYEE    
    const handleFormSubmit = (event) => {
        event.preventDefault();
        const formData = {
            employeeId: event.target.elements.employeeId.value,
            firstName: event.target.elements.firstName.value,
            lastName: event.target.elements.lastName.value,
            employeeImage: event.target.elements.employeeImage.value,
            dateOfBirth: event.target.elements.dateOfBirth.value,
            employeeAddress: event.target.elements.employeeAddress.value,
            email: event.target.elements.email.value,
            phoneNumber: event.target.elements.phoneNumber.value,
            bankAccountNumber: event.target.elements.bankAccountNumber.value,
            bankAccountName: event.target.elements.bankAccountName.value,
            bankName: event.target.elements.bankName.value,
            jobId: event.target.elements.jobId.value,
            departmentId: event.target.elements.departmentId.value,
            status: event.target.elements.status.value,
        };

        if (!formData.employeeId) {
            setValidationError('Employee ID is required');
            return;
        }

        if (!formData.firstName) {
            setValidationError('First name is required');
            return;
        }

        if (!formData.lastName) {
            setValidationError('Last name is required');
            return;
        }

        if (!formData.dateOfBirth) {
            setValidationError('Date of birth is required');
            return;
        }

        if (!formData.employeeAddress) {
            setValidationError('Employee address is required');
            return;
        }

        if (!formData.email) {
            setValidationError('Email is required');
            return;
        }

        if (!formData.phoneNumber) {
            setValidationError('Phone number is required');
            return;
        }

        if (!formData.bankAccountNumber) {
            setValidationError('Bank account number is required');
            return;
        }

        if (!formData.bankAccountName) {
            setValidationError('Bank account name is required');
            return;
        }

        if (!formData.bankName) {
            setValidationError('Bank name is required');
            return;
        }

        if (!formData.jobId) {
            setValidationError('Job ID is required');
            return;
        }

        if (!formData.departmentId) {
            setValidationError('Department ID is required');
            return;
        }

        if (!formData.status) {
            setValidationError('Status is required');
            return;
        }

        fetch('https://localhost:7220/api/Employee/create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
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
            .then(employee => {
                setData([...data, employee]);
                setShowForm(false);
                setValidationError('');
                console.log('Employee created successfully');
            })
            .catch(error => {
                console.error('Error submitting form:', error);
                setValidationError('An error occurred while submitting the form');
            });
        console.log(event.target.elements)
    };



    //  UPDATE NEW EMPLOYEE        
    const handleUpdate = (event) => {
        event.preventDefault();
        const formData = {
            employeeId: updateEmployee.employeeId,
            firstName: event.target.elements.firstName.value,
            lastName: event.target.elements.lastName.value,
            employeeImage: event.target.elements.employeeImage.value,
            dateOfBirth: event.target.elements.dateOfBirth.value,
            employeeAddress: event.target.elements.employeeAddress.value,
            email: event.target.elements.email.value,
            phoneNumber: event.target.elements.phoneNumber.value,
            bankAccountNumber: event.target.elements.bankAccountNumber.value,
            bankAccountName: event.target.elements.bankAccountName.value,
            bankName: event.target.elements.bankName.value,
            jobId: event.target.elements.jobId.value,
            departmentId: event.target.elements.departmentId.value,
            status: event.target.elements.status.value,
        };

        if (!formData.employeeId) {
            setValidationError('Employee ID is required');
            return;
        }

        if (!formData.firstName) {
            setValidationError('First name is required');
            return;
        }

        if (!formData.lastName) {
            setValidationError('Last name is required');
            return;
        }

        if (!formData.dateOfBirth) {
            setValidationError('Date of birth is required');
            return;
        }

        if (!formData.employeeAddress) {
            setValidationError('Employee address is required');
            return;
        }

        if (!formData.email) {
            setValidationError('Email is required');
            return;
        }

        if (!formData.phoneNumber) {
            setValidationError('Phone number is required');
            return;
        }

        if (!formData.bankAccountNumber) {
            setValidationError('Bank account number is required');
            return;
        }

        if (!formData.bankAccountName) {
            setValidationError('Bank account name is required');
            return;
        }

        if (!formData.bankName) {
            setValidationError('Bank name is required');
            return;
        }

        if (!formData.jobId) {
            setValidationError('Job ID is required');
            return;
        }

        if (!formData.departmentId) {
            setValidationError('Department ID is required');
            return;
        }

        if (!formData.status) {
            setValidationError('Status is required');
            return;
        }

        fetch(`https://localhost:7220/api/Employee/update/user/${updateEmployee.employeeId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
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
            .then(updatedEmployee => {
                const updatedData = data.map(employee => {
                    if (employee.employeeId === updatedEmployee.employeeId) {
                        return updatedEmployee;
                    } else {
                        return employee;
                    }
                });
                setData(updatedData);
                setShowUpdateForm(false);
                setUpdateEmployee(null);
                console.log('Employee updated successfully');
            })
            .catch(error => {
                console.error('Error submitting form:', error);
                setValidationError('An error occurred while updating employee information');
            });
    };

    return (
        <div className="manager" style={{ position: "relative" }}>
            <button className='btn_create' onClick={() => setShowForm(true)}>Add Employee</button>
            <div className='row'>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Employee ID</th>
                            <th>Full Name</th>
                            <th>Birthday</th>
                            <th>Email</th>
                            <th>Phone</th>
                            <th>Department</th>
                            <th>Job</th>
                            <th>Status</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(employee => (
                            <tr key={employee.employeeId}>
                                <td>{employee.employeeId}</td>
                                <td>{employee.firstName} {employee.lastName}</td>
                                <td>{employee.dateOfBirth}</td>
                                <td>{employee.email}</td>
                                <td>{employee.phoneNumber}</td>
                                <td>{employee.departmentId}</td>
                                <td>{employee.jobId}</td>
                                <td>{employee.status}</td>
                                <td>
                                    <button onClick={() => handleEdit(employee)}>Edit</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {showCreateForm && (
                <div className="form-container">
                    <form className="form" onSubmit={handleFormSubmit}>
                        <h3>Create Employee</h3>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}

                        <div className='row name'>
                            <div className="col-6 mt-3">
                                <label>First Name:</label>
                                <input type="text" name="firstName" placeholder='First Name' />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Last Name:</label>
                                <input type="text" name="lastName" placeholder='Last Name' />
                            </div>
                        </div>

                        <div className='row name'>
                            <div className="col-6 mt-3">
                                <label>Employee ID:</label>
                                <input type="text" name="employeeId" placeholder='EP######' />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Phone Number:</label>
                                <input type="text" name="phoneNumber" placeholder='1234-567-890' />
                            </div>
                        </div>

                        <div className='row name'>
                            <div className="col-6 mt-3">
                                <label>Employee Image:</label>
                                <input type="text" name="employeeImage" placeholder='string' />
                            </div>

                            <div className="col-6 mt-3">
                                <label>Date of Birth:</label>
                                <input type="date" name="dateOfBirth" placeholder='dd-MM-YYYY' />
                            </div>
                        </div>

                        <label>Address:</label>
                        <input type="text" name="employeeAddress" placeholder='123 Street, City, Country' />

                        <label>Email:</label>
                        <input type="email" name="email" placeholder='example@mail.com' />

                        <div className='row name'>
                            <div className="col-6 mt-3">
                                <label>Bank Account Number:</label>
                                <input type="text" name="bankAccountNumber" placeholder='123-456-789' />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Bank Account Name:</label>
                                <input type="text" name="bankAccountName" placeholder='Holder Name' />
                            </div>
                        </div>

                        <label>Bank Name:</label>
                        <input type="text" name="bankName" placeholder='Bank Name' />

                        <div className='row name'>
                            <div className="col-6 mt-3">
                                <label>Job:</label>
                                <input type="text" name="jobId" placeholder='JB######' />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Department:</label>
                                <input type="text" name="departmentId" placeholder='DP######' />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-6 mt-3">
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
                        <h3>Edit Employee (ID: {updateEmployee.employeeId})</h3>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}

                        <div className='row name'>
                            <div className="col-6 mt-3">
                                <label>First Name:</label>
                                <input type="text" name="firstName" defaultValue={updateEmployee.firstName} />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Last Name:</label>
                                <input type="text" name="lastName" defaultValue={updateEmployee.lastName} />
                            </div>
                        </div>
                        <div className='row'>
                            <div className="col-6 mt-3">
                                <label>Employee Image:</label>
                                <input type="text" name="employeeImage" defaultValue={updateEmployee.employeeImage} />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Date of Birth:</label>
                                <input type="date" name="dateOfBirth" defaultValue={updateEmployee.dateOfBirth} />
                            </div>
                        </div>

                        <label>Address:</label>
                        <input type="text" name="employeeAddress" defaultValue={updateEmployee.employeeAddress} />

                        <label>Email:</label>
                        <input type="email" name="email" defaultValue={updateEmployee.email} />

                        <label>Phone Number:</label>
                        <input type="text" name="phoneNumber" defaultValue={updateEmployee.phoneNumber} />

                        <div className='row'>
                            <div className="col-6 mt-3">
                                <label>Bank Account Name:</label>
                                <input type="text" name="bankAccountName" defaultValue={updateEmployee.bankAccountName} />

                            </div>
                            <div className="col-6 mt-3">
                                <label>Bank Account Number:</label>
                                <input type="text" name="bankAccountNumber" defaultValue={updateEmployee.bankAccountNumber} />
                            </div>
                        </div>

                        <label>Bank Name:</label>
                        <input type="text" name="bankName" defaultValue={updateEmployee.bankName} />

                        <div className='row'>
                            <div className="col-6 mt-3">
                                <label>Job ID:</label>
                                <input type="text" name="jobId" defaultValue={updateEmployee.jobId} />
                            </div>

                            <div className="col-6 mt-3">
                                <label>Department ID:</label>
                                <input type="text" name="departmentId" defaultValue={updateEmployee.departmentId} />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-6 mt-3">
                                <label>Status:</label>
                                <input type="text" name="status" defaultValue={updateEmployee.status} />
                                {/* <select name="status" value={updateEmployee.status} onChange={handleStatusChange}>
                                    <option value="Active">Active</option>
                                    <option value="Disable">Disable</option>
                                </select> */}
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

export default Manage;