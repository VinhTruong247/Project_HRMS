import React from 'react';
import { useEffect } from 'react';

function Manage(props) {
    const [data, setData] = React.useState([]);
    const [showForm, setShowForm] = React.useState(false);
    const token = localStorage.getItem('jwtToken');
    const [updateEmployee, setUpdateEmployee] = React.useState(null);
    const [showUpdateForm, setShowUpdateForm] = React.useState(false);

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

    const handleFormSubmit = (event) => {
        event.preventDefault();
        const dob = new Date(event.target.elements.dateOfBirth.value);
        const formattedDob = dob.toISOString().slice(0, 10);
        const formData = {
            employeeId: event.target.elements.employeeId.value,
            firstName: event.target.elements.firstName.value,
            lastName: event.target.elements.lastName.value,
            employeeImage: event.target.elements.employeeImage.value,
            dateOfBirth: formattedDob,
            employeeAddress: event.target.elements.employeeAddress.value,
            email: event.target.elements.email.value,
            phoneNumber: event.target.elements.phoneNumber.value,
            bankAccountNumber: event.target.elements.bankAccountNumber.value,
            bankAccountName: event.target.elements.bankAccountName.value,
            bankName: event.target.elements.bankName.value,
            userId: event.target.elements.userId.value,
            jobId: event.target.elements.jobId.value,
            departmentId: event.target.elements.departmentId.value,
            status: event.target.elements.status.value,
        };

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
                console.log('Employee created successfully');
            })
            .catch(error => {
                console.error('Error submitting form:', error);
            });
        console.log(event.target.elements)
    }

    const handleEdit = (employee) => {
        setUpdateEmployee(employee);
        setShowUpdateForm(true);
    };

    const handleStatusChange = (event) => {
        setUpdateEmployee({
          ...updateEmployee,
          status: event.target.value
        });
      };

    const handleUpdate = (event) => {
        event.preventDefault();
        const dob = new Date(event.target.elements.dateOfBirth.value);
        const formattedDob = dob.toISOString().slice(0, 10);
        const formData = {
            employeeId: updateEmployee.employeeId,
            firstName: event.target.elements.firstName.value,
            lastName: event.target.elements.lastName.value,
            employeeImage: event.target.elements.employeeImage.value,
            dateOfBirth: formattedDob,
            employeeAddress: event.target.elements.employeeAddress.value,
            email: event.target.elements.email.value,
            phoneNumber: event.target.elements.phoneNumber.value,
            bankAccountNumber: event.target.elements.bankAccountNumber.value,
            bankAccountName: event.target.elements.bankAccountName.value,
            bankName: event.target.elements.bankName.value,
            userId: event.target.elements.userId.value,
            jobId: event.target.elements.jobId.value,
            departmentId: event.target.elements.departmentId.value,
            status: event.target.elements.status.value,
        };

        fetch(`https://localhost:7220/api/Employee/update`, {
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
            });
    };



    return (
        <div className="manager" style={{ position: "relative" }}>
            <div className='row'>
                <button onClick={() => setShowForm(true)}>Add employee</button>
                <table className='table table-striped'>
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
                                    <button onClick={() => handleEdit(employee)}>Edit</button> |
                                    {/* <button onClick={() => handleDisable(employee.employeeId)}>Disable</button> */}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {showForm && (
                <div className="form-container">
                    <form className="form" onSubmit={handleFormSubmit}>
                        <h3>Create Employee</h3>

                        <div className='row name'>
                            <div class="col-6 mt-3">
                                <label>Employee ID:</label>
                                <input type="text" name="employeeId" placeholder='EP######' />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Department:</label>
                                <input type="text" name="departmentId" placeholder='DP######' />
                            </div>
                        </div>

                        <div className='row name'>
                            <div class="col-6 mt-3">
                                <label>First Name:</label>
                                <input type="text" name="firstName" placeholder='First Name' />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Last Name:</label>
                                <input type="text" name="lastName" placeholder='Last Name' />
                            </div>
                        </div>

                        <div className='row name'>
                            <div class="col-6 mt-3">
                                <label>Employee Image:</label>
                                <input type="text" name="employeeImage" placeholder='string' />
                            </div>

                            <div class="col-6 mt-3">
                                <label>Date of Birth:</label>
                                <input type="date" name="dateOfBirth" placeholder='dd-MMM-YYYY' />
                            </div>
                        </div>

                        <label>Address:</label>
                        <input type="text" name="employeeAddress" placeholder='123 Street, City, Country' />

                        <label>Email:</label>
                        <input type="email" name="email" placeholder='example@mail.com' />

                        <label>Phone Number:</label>
                        <input type="text" name="phoneNumber" placeholder='1234-567-890' />

                        <div className='row name'>
                            <div class="col-6 mt-3">
                                <label>Bank Account Number:</label>
                                <input type="text" name="bankAccountNumber" placeholder='123-456-789' />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Bank Account Name:</label>
                                <input type="text" name="bankAccountName" placeholder='Holder Name' />
                            </div>
                        </div>

                        <label>Bank Name:</label>
                        <input type="text" name="bankName" placeholder='Bank Name' />

                        <div className='row name'>
                            <div class="col-6 mt-3">
                                <label>Job:</label>
                                <input type="text" name="jobId" placeholder='JB######' />
                            </div>
                            <div class="col-6 mt-3">
                                <label>User ID:</label>
                                <input type="text" name="userId" placeholder='US######' />
                            </div>
                        </div>

                        <label>Status:</label>
                        <select name="status">
                            <option value="Active">Active</option>
                            <option value="Disable">Disable</option>
                        </select>

                        <div className='row btn'>
                            <div class="col-5 mt-3">
                                <button type="submit">Submit</button>
                            </div>
                            <div class="col-5 mt-3">
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
                        <div className='row name'>
                            <div class="col-6 mt-3">
                                <label>First Name:</label>
                                <input type="text" name="firstName" defaultValue={updateEmployee.firstName} />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Last Name:</label>
                                <input type="text" name="lastName" defaultValue={updateEmployee.lastName} />
                            </div>
                        </div>
                        <div className='row'>
                            <div class="col-6 mt-3">
                                <label>Employee Image:</label>
                                <input type="text" name="employeeImage" defaultValue={updateEmployee.employeeImage} />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Date of Birth:</label>
                                <input type="date" name="dateOfBirth" defaultValue={updateEmployee.dateOfBirth} />
                            </div>
                        </div>
                        <div className='row'>
                            <div class="col-6 mt-3">
                                <label>Address:</label>
                                <input type="text" name="employeeAddress" defaultValue={updateEmployee.employeeAddress} />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Email:</label>
                                <input type="email" name="email" defaultValue={updateEmployee.email} />
                            </div>
                        </div>
                        <div className='row'>
                            <div class="col-6 mt-3">
                                <label>Phone Number:</label>
                                <input type="text" name="phoneNumber" defaultValue={updateEmployee.phoneNumber} />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Bank Account Number:</label>
                                <input type="text" name="bankAccountNumber" defaultValue={updateEmployee.bankAccountNumber} />
                            </div>
                        </div>
                        <div className='row'>
                            <div class="col-6 mt-3">
                                <label>Bank Account Name:</label>
                                <input type="text" name="bankAccountName" defaultValue={updateEmployee.bankAccountName} />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Bank Name:</label>
                                <input type="text" name="bankName" defaultValue={updateEmployee.bankName} />
                            </div>
                        </div>
                        <div className='row'>
                            <div class="col-6 mt-3">
                                <label>User ID:</label>
                                <input type="text" name="userId" defaultValue={updateEmployee.userId} />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Job ID:</label>
                                <input type="text" name="jobId" defaultValue={updateEmployee.jobId} />
                            </div>
                        </div>
                        <div className='row'>
                            <div class="col-6 mt-3">
                                <label>Department ID:</label>
                                <input type="text" name="departmentId" defaultValue={updateEmployee.departmentId} />
                            </div>
                            <div class="col-6 mt-3">
                                <label>Status:</label>
                                <select name="status" value={updateEmployee.status} onChange={handleStatusChange}>
                                    <option value="Active">Active</option>
                                    <option value="Disable">Disable</option>
                                </select>
                            </div>
                        </div>

                        <div className='row btn'>
                            <div class="col-5 mt-3">
                                <button type="submit">Update</button>
                            </div>
                            <div class="col-5 mt-3">
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