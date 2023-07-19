import React from 'react';
import { useEffect, useState, useRef } from 'react';
import moment from 'moment';
import { MDBDataTableV5 } from 'mdbreact';

function Employee(props) {
    const [data, setData] = useState([]);
    const [showCreateForm, setShowForm] = useState(false);
    const token = JSON.parse(localStorage.getItem('jwtToken'));
    const [updateEmployee, setUpdateEmployee] = useState(null);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [validationError, setValidationError] = useState('');
    const [departmentNames, setDepartmentNames] = useState([]);
    const [jobTitles, setJobTitles] = useState([]);
    const [selectedReport, setSelectedReport] = useState(null);
    const employeeIdPattern = /^EP\d{6}$/;
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const phoneNumberPattern = /^(\+)?\d{10}$/;
    // const atmNumberPattern = /^(\d{4}[- ]?){3}\d{4}$/;
    const atmNumberPattern = /^\d{9}$/;

    const handleEdit = (employee) => {
        setUpdateEmployee(employee);
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

    //  Get info of EMPLOYEE
    useEffect(() => {
        fetch('https://localhost:7220/api/Employee/employees', {
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
            .then(employees => {
                setData(employees)
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }, []);


    // Fetch department names and job titles
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
                    throw new Error('API response was not ok.');
                }
            })
            .then(departments => {
                setDepartmentNames(departments);
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });

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
                    throw new Error('API response was not ok.');
                }
            })
            .then(jobs => {
                setJobTitles(jobs);
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }, []);


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
            status: event.target.elements.status.checked,
        };

        const isDuplicateEmployeeId = data.some(employee => employee.employeeId === formData.employeeId);

        if (!formData.employeeId) {
            setValidationError('Employee ID is required');
            return;
        }

        if (!employeeIdPattern.test(formData.employeeId)) {
            setValidationError('Employee ID must follow EP###### format');
            return;
        }

        if (isDuplicateEmployeeId) {
            setValidationError('Employee ID is already taken');
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

        if (!emailPattern.test(formData.email)) {
            setValidationError('Email form is not valid');
            return;
        }

        if (!formData.phoneNumber) {
            setValidationError('Phone number is required');
            return;
        }

        if (!phoneNumberPattern.test(formData.phoneNumber)) {
            setValidationError('Phone number is not valid');
            return;
        }

        if (!formData.bankAccountNumber) {
            setValidationError('Bank account number is required');
            return;
        }

        if (!atmNumberPattern.test(formData.bankAccountNumber)) {
            setValidationError('Bank account number is not valid');
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

        fetch('https://localhost:7220/api/Employee/create', {
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
            status: event.target.elements.status.value === 'true',
        };

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

        if (!emailPattern.test(formData.email)) {
            setValidationError('Email form is not valid');
            return;
        }

        if (!formData.phoneNumber) {
            setValidationError('Phone number is required');
            return;
        }

        if (!phoneNumberPattern.test(formData.phoneNumber)) {
            setValidationError('Phone number is not valid');
            return;
        }

        if (!formData.bankAccountNumber) {
            setValidationError('Bank account number is required');
            return;
        }

        if (!atmNumberPattern.test(formData.bankAccountNumber)) {
            setValidationError('Bank account number is not valid');
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

        fetch(`https://localhost:7220/api/Employee/update/user/${updateEmployee.employeeId}`, {
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
            <div className='row addbtn'> <button className='btn_create' onClick={() => setShowForm(true)}>Add Employee</button></div>

            <div className="card mb-3">
                <div className="card-body">
                    <div className="row">
                        <div className="col-2">
                            <h3 className="mb-0">Employee Details:</h3>
                        </div>
                        <div className="col-10 text-secondary">
                            {selectedReport && (
                                <div>

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Employee ID:</h4>
                                            <p>{selectedReport.employeeId}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Employee Name:</h4>
                                            <p>{selectedReport.firstName} {selectedReport.lastName}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Date of Birth:</h4>
                                            <p>{selectedReport.dateOfBirth}</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Department:</h4>
                                            <p>
                                                {departmentNames.find(department => department.departmentId === selectedReport.departmentId)
                                                    ? departmentNames.find(department => department.departmentId === selectedReport.departmentId).departmentName
                                                    : ''}
                                            </p>
                                        </div>
                                        <div className="col-sm-8">
                                            <h4>Job:</h4>
                                            <p>
                                                {jobTitles.find(job => job.jobId === selectedReport.jobId)
                                                    ? jobTitles.find(job => job.jobId === selectedReport.jobId).jobTitle
                                                    : 'Unknown'}
                                            </p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Email:</h4>
                                            <p>{selectedReport.phoneNumber}</p>
                                        </div>
                                        <div className="col-sm-8">
                                            <h4>Phone Number:</h4>
                                            <p>{selectedReport.phoneNumber}</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Bank Account Name:</h4>
                                            <p>{selectedReport.bankAccountName}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Bank Account Number:</h4>
                                            <p>{selectedReport.bankAccountNumber}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Bank Name:</h4>
                                            <p>{selectedReport.bankName}</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <h4>Address:</h4>
                                    <p>{selectedReport.employeeAddress}</p>
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
                                label: 'Employee ID',
                                field: 'employeeId',
                                width: 150,
                            },
                            {
                                label: 'Full Name',
                                field: 'fullName',
                                width: 150,
                            },
                            {
                                label: 'Date of Birth',
                                field: 'dateOfBirth',
                                width: 150,
                            },
                            {
                                label: 'Email',
                                field: 'email',
                                width: 200,
                            },
                            {
                                label: 'Phone',
                                field: 'phone',
                                width: 150,
                            },
                            {
                                label: 'Department',
                                field: 'department',
                                width: 150,
                            },
                            {
                                label: 'Job',
                                field: 'job',
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
                        rows: data.map((employee) => ({
                            employeeId: employee.employeeId,
                            fullName: `${employee.firstName} ${employee.lastName}`,
                            dateOfBirth: employee.dateOfBirth,
                            email: employee.email,
                            phone: employee.phoneNumber,
                            department: departmentNames.find(
                                (department) => department.departmentId === employee.departmentId
                            )
                                ? departmentNames.find(
                                    (department) => department.departmentId === employee.departmentId
                                ).departmentName
                                : '',
                            job: jobTitles.find((job) => job.jobId === employee.jobId)
                                ? jobTitles.find((job) => job.jobId === employee.jobId).jobTitle
                                : 'Unknown',
                            status: employee.status ? 'Active' : 'Disable',
                            options: (
                                <button onClick={() => handleEdit(employee)}>Edit</button>
                            ),
                            clickEvent: () => handleDoubleClick(employee)
                        }))
                    }}
                    hover
                    entriesOptions={[5, 10, 20]}
                    entries={10}
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
                        <h3>Create Employee</h3>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}

                        <div className='row'>
                            <div className="col-6 mt-3">
                                <label>First Name:</label>
                                <input type="text" name="firstName" placeholder='First Name' />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Last Name:</label>
                                <input type="text" name="lastName" placeholder='Last Name' />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-6 mt-3">
                                <label>Employee ID:</label>
                                <input type="text" name="employeeId" placeholder='EP######' />
                            </div>
                            <div className="col-6 mt-3">
                                <label>Phone Number:</label>
                                <input type="text" name="phoneNumber" placeholder='1234-567-890' />
                            </div>
                        </div>

                        <div className='row'>
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
                            <div className="col-6 mt-1">
                                {/* <label>Job:</label>
                                <input type="text" name="jobId" placeholder='JB######' /> */}
                                <label>Job:</label>
                                <select name="jobId" onChange={event => console.log(event.target.value)}>
                                    {jobTitles.map(job => (
                                        <option key={job.jobId} value={job.jobId}>
                                            {job.jobTitle}
                                        </option>
                                    ))}
                                </select>
                            </div>
                            <div className="col-6 mt-1">
                                {/* <label>Department:</label>
                                <input type="text" name="departmentId" placeholder='DP######' /> */}
                                <label>Department:</label>
                                <select name="departmentId" onChange={event => console.log(event.target.value)}>
                                    {departmentNames.map(department => (
                                        <option key={department.departmentId} value={department.departmentId}>
                                            {department.departmentName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-3 mt-1"></div>
                            <div className="col-6 mt-1">
                                <label>Status:</label>
                                <select name="status" defaultValue={true} onChange={event => console.log(event.target.value)}>
                                    <option value={true}>Active</option>
                                    <option value={false}>Disable</option>
                                </select>
                            </div>
                            <div className="col-3 mt-1"></div>
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
                                <input type="date" name="dateOfBirth" defaultValue={moment(moment(updateEmployee.dateOfBirth, 'DD-MM-YYYY')).format('YYYY-MM-DD')} />
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
                                {/* <label>Job ID:</label>
                                <input type="text" name="jobId" defaultValue={updateEmployee.jobId} /> */}
                                <label>Job:</label>
                                <select name="jobId" defaultValue={updateEmployee.jobId} onChange={event => console.log(event.target.value)}>
                                    {jobTitles.map(job => (
                                        <option key={job.jobId} value={job.jobId}>
                                            {job.jobTitle}
                                        </option>
                                    ))}
                                </select>
                            </div>

                            <div className="col-6 mt-3">
                                {/* <label>Department ID:</label>
                                <input type="text" name="departmentId" defaultValue={updateEmployee.departmentId} /> */}
                                <label>Department:</label>
                                <select name="departmentId" defaultValue={updateEmployee.departmentId} onChange={event => console.log(event.target.value)}>
                                    {departmentNames.map(department => (
                                        <option key={department.departmentId} value={department.departmentId}>
                                            {department.departmentName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-3 mt-3"></div>
                            <div className="col-6 mt-3">
                                <label>Status:</label>
                                {/* <input type="text" name="status" defaultValue={updateEmployee.status} /> */}
                                <select name="status" defaultValue={updateEmployee.status ? 'true' : 'false'} onChange={event => console.log(event.target.value)}>
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

export default Employee;
