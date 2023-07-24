import React, { useState, useEffect, useRef } from "react";
import useData from "../hooks/useData";
import moment from "moment";

function Profile(props) {

    const data = useData();
    const token = JSON.parse(localStorage.getItem('jwtToken'));
    const [selectedPayslip, setPayslip] = useState(null);
    const [showPayslipForm, setShowPayslipForm] = useState(false);

    useEffect(() => {
        fetch(`https://localhost:7220/api/PaySlip/get/payslip/${data.id}`, {
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
                    throw new Error("Api response was not ok.");
                }
            })
            .then((payslips) => {
                setPayslip(payslips);
            })
            .catch((error) => {
                console.error("There was a problem with the fetch operation:", error);
            });
    }, []);

    return data && (
        <div className="main-body payslip">
            <button className="view-payslip-button btn btn-primary mb-3" onClick={() => setShowPayslipForm(true)}>View Payslip</button>
            <div className="row gutters-sm">
                <div className="col-md-4">
                    <EmployeeCard
                        lastName={data.lastName}
                        firstName={data.firstName}
                        employeeId={data.id}
                        departmentId={data.departmentId}
                        jobId={data.jobId}
                    />
                    <EmployeeLinks />
                </div>
                <div className="col-md-8">
                    <EmployeeDetails
                        employeeId={data.employeeId}
                        lastName={data.lastName}
                        firstName={data.firstName}
                        dateOfBirth={data.dateOfBirth}
                        email={data.email}
                        phoneNumber={data.phoneNumber}
                        employeeAddress={data.employeeAddress}
                    />
                </div>
            </div>

            {showPayslipForm && (
                <div className="form-container">
                    <form className="formpayslip">
                        <h3></h3>
                        <div>
                            <div className="row">
                                <div className="col-sm-4">
                                    <h4>Payslip ID:</h4>
                                    <p>{selectedPayslip.payslipId}</p>
                                </div>
                                <div className="col-sm-8">
                                    <h4>Employee ID:</h4>
                                    <p>{selectedPayslip.employeeId}</p>
                                </div>
                            </div>
                            <hr />

                            <div className="row">
                                <div className="col-sm-4">
                                    <h4>Pay Period</h4>
                                    <p>{selectedPayslip.payPeriod}</p>
                                </div>
                                <div className="col-sm-8">
                                    <h4>Total Salary:</h4>
                                    <p>{selectedPayslip.totalSalary}</p>
                                </div>
                            </div>
                            <hr />

                            <h4>Note:</h4>
                            <p>{selectedPayslip.note}</p>
                            <hr />

                            <h4>Status:</h4>
                            <p>
                                {selectedPayslip.status}
                            </p>
                            <hr />

                            <div className='row butt' style={{ justifyContent: 'right' }}>
                                <button onClick={() => setShowPayslipForm(false)}>Cancel</button>
                            </div>
                        </div>

                    </form>
                </div>
            )}

        </div>
    );
}

function EmployeeCard(props) {
    const [department, setDepartment] = useState('');
    const [jobTitle, setJobTitle] = useState('');
    const token = JSON.parse(localStorage.getItem('jwtToken'));

    useEffect(() => {
        async function fetchData() {
            try {
                const departmentResponse = await fetch(`https://localhost:7220/api/Department/departments`, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token.token}`
                    }
                });
                const jobTitleResponse = await fetch(`https://localhost:7220/api/Job/jobs`, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token.token}`
                    }
                });

                const departmentData = await departmentResponse.json();
                const jobTitleData = await jobTitleResponse.json();

                const matchedDepartment = departmentData.find(department => department.departmentId === props.departmentId);
                if (matchedDepartment) {
                    setDepartment(matchedDepartment.departmentName);
                }

                const matchedJobTitle = jobTitleData.find(jobTitle => jobTitle.jobTitleId === props.jobTitleId);
                if (matchedJobTitle) {
                    setJobTitle(matchedJobTitle.jobTitle);
                }
            } catch (error) {
                console.error(error);
            }
        }

        fetchData();
    }, [props.departmentId, props.jobTitleId, token.token]);

    return (
        <div className="card">
            <div className="card-body">
                <div className="d-flex flex-column align-items-center text-center text-xxl">
                    <img src="#" alt="employees-img" className="rounded-circle" />
                    <div className="mt-3">
                        <h4>{props.firstName} {props.lastName}</h4>
                        {/* Display the department and job title */}
                        <p className="text-secondary mb-1">{department}</p>
                        <p className="text-secondary mb-1">{jobTitle}</p>
                        <p className="text-secondary mb-1">{props.employeeId}</p>
                    </div>
                </div>
            </div>
        </div>
    );
}

function EmployeeLinks(props) {
    return (
        <div className="card mt-3">
            <ul className="list-group">
                <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                    <h6>
                        <i className="fa fa-linkedin"></i> LinkedIn
                    </h6>
                    <span className="text-secondary">#</span>
                </li>
                <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                    <h6>
                        <i className="fa fa-github"></i> Github
                    </h6>
                    <span className="text-secondary">bootdey</span>
                </li>
                <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                    <h6>
                        <i className="fa fa-twitter"></i> Twitter
                    </h6>
                    <span className="text-secondary">@bootdey</span>
                </li>
                <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                    <h6>
                        <i className="fa fa-facebook"></i> Facebook
                    </h6>
                    <span className="text-secondary">bootdey</span>
                </li>
            </ul>
        </div>
    );
}

function EmployeeDetails(props) {
    const [data, setData] = useState(props);
    const [isEditing, setIsEditing] = useState(false);
    const token = JSON.parse(localStorage.getItem('jwtToken'));
    const [validationErrors, setValidationErrors] = useState({});
    const [showCreateForm, setShowForm] = useState(false);
    const [updateReport, setUpdateReport] = useState(null);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [validationError, setValidationError] = useState('');
    const handleEdit = (report) => {
        setUpdateReport(report);
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

    const [formData, setFormData] = useState({
        firstName: props.firstName,
        lastName: props.lastName,
        dateOfBirth: props.dateOfBirth,
        email: props.email,
        phoneNumber: props.phoneNumber,
        employeeAddress: props.employeeAddress,
    });

    const handleInputChange = (event) => {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value,
        });

        setValidationErrors((prevErrors) => {
            return {
                ...prevErrors,
                [event.target.name]: '',
            };
        });
    };

    const validateForm = () => {
        const errors = {};

        if (!formData.firstName) {
            errors.firstName = 'First name is required';
        }

        if (!formData.lastName) {
            errors.lastName = 'Last name is required';
        }

        if (!formData.dateOfBirth) {
            errors.dateOfBirth = 'Date of birth is required';
        }

        setValidationErrors(errors);
        return Object.keys(errors).length === 0;
    };

    const handleSubmit = () => {
        const isValid = validateForm();
        if (isValid) {
            fetch(`https://localhost:7220/api/Employee/update/profile/${props.employeeId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token.token}`,
                },
                body: JSON.stringify(formData),
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
                    console.log('Employee updated successfully');
                })
                .catch(error => {
                    console.error('Error submitting form:', error);
                });
        }
    };

    const handleFormSubmit = (event) => {
        event.preventDefault();
        const formData = {
            employeeId: event.target.elements.employeeId.value,
            reason: event.target.elements.reason.value,
            content: event.target.elements.content.value,
            issueDate: event.target.elements.content.value,
        };

        if (!formData.reason) {
            setValidationError('Reason type is required');
            return;
        }

        if (!formData.content) {
            setValidationError('Content needed is required');
            return;
        }

        fetch('https://localhost:7220/api/Report/create', {
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
            .then(report => {
                setData([...data, report]);
                setShowForm(false);
                setValidationError('');
                console.log('Report form created successfully');
            })
            .catch(error => {
                console.error('Error submitting form:', error);
                setValidationError('An error occurred while submitting the form');
            });
        console.log(event.target.elements)
    };

    if (isEditing) {
        return (
            <div className="card mb-3">
                <div className="card-body">

                    <form onSubmit={handleSubmit}>
                        <div className="row">
                            <div className="col-3 sm-3">
                                <h6 className="mb-0">First Name</h6>
                            </div>
                            <div className="col-9 sm-3">
                                <input
                                    type="text"
                                    name="firstName"
                                    value={props.firstName}
                                    onChange={handleInputChange}
                                />
                                {validationErrors.firstName && (
                                    <div className="error-message">{validationErrors.firstName}</div>
                                )}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-3 sm-3">
                                <h6 className="mb-0">Last Name</h6>
                            </div>
                            <div className="col-9 sm-3">
                                <input
                                    type="text"
                                    name="lastName"
                                    value={props.lastName}
                                    onChange={handleInputChange}
                                />
                                {validationErrors.lastName && (
                                    <div className="error-message">{validationErrors.lastName}</div>
                                )}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-3 sm-3">
                                <h6 className="mb-0">Date of Birth</h6>
                            </div>
                            <div className="col-3 sm-3">
                                <input
                                    type="date"
                                    name="dateOfBirth"
                                    defaultValue={moment(moment(props.dateOfBirth, 'DD-MM-YYYY')).format('YYYY-MM-DD')}
                                    onChange={handleInputChange}
                                />
                            </div>
                            {validationErrors.dateOfBirth && (
                                <div className="error-message">{validationErrors.dateOfBirth}</div>
                            )}
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-3 sm-3">
                                <h6 className="mb-0">Email</h6>
                            </div>
                            <div className="col-9 sm-3">
                                <input
                                    type="email"
                                    name="email"
                                    value={props.email}
                                    onChange={handleInputChange}
                                />
                            </div>
                            {validationErrors.email && (
                                <div className="error-message">{validationErrors.email}</div>
                            )}
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-3 sm-3">
                                <h6 className="mb-0">Phone</h6>
                            </div>
                            <div className="col-9 sm-3">
                                <input
                                    type="tel"
                                    name="phoneNumber"
                                    value={props.phoneNumber}
                                    onChange={handleInputChange}
                                />
                            </div>
                            {validationErrors.phoneNumber && (
                                <div className="error-message">{validationErrors.phoneNumber}</div>
                            )}
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-3 sm-3">
                                <h6 className="mb-0">Address</h6>
                            </div>
                            <div className="col-9 sm-3">
                                <input
                                    type="text"
                                    name="employeeAddress"
                                    value={props.employeeAddress}
                                    onChange={handleInputChange}
                                />
                            </div>
                            {validationErrors.employeeAddress && (
                                <div className="error-message">{validationErrors.employeeAddress}</div>
                            )}
                        </div>
                        <hr />

                        <div className='row butt'>
                            <div className="col-6 mt-2">

                            </div>
                            <div className="col-6 mt-2" style={{ float: 'right' }}>
                                <button type="submit" className="btn btn-primary">
                                    Save Changes
                                </button>
                                <button
                                    type="button"
                                    className="btn btn-secondary ml-2"
                                    onClick={() => setIsEditing(false)}
                                    style={{ marginLeft: '1rem' }}
                                >
                                    Cancel
                                </button>
                            </div>
                        </div>


                    </form>
                </div >
            </div >
        );
    }

    return (
        <div className="card mb-3">
            <div className="card-body">
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Full Name</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">{props.firstName} {props.lastName}</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">First Name</h6>
                    </div>
                    <div className="col-sm-3 text-secondary">{props.firstName}</div>
                    <div className="col-sm-3">
                        <h6 className="mb-0">Last Name</h6>
                    </div>
                    <div className="col-sm-3 text-secondary">{props.lastName}</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Date of Birth</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">{props.dateOfBirth}</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Email</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">{props.email}</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Phone</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">{props.phoneNumber}</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Address</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">{props.employeeAddress}</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Skills</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">
                        React, Node.js, MongoDB, Express.js
                    </div>
                </div>
                <hr />
                <div className="row form" style={{ justifyContent: 'right' }}>
                    <div className="col-sm-1">
                        <button
                            type="button"
                            className="btn btn-edit"
                            onClick={() => setIsEditing(true)}
                        >
                            Edit
                        </button>
                    </div>
                    <div className="col-sm-2">
                        <button
                            type="button"
                            className='btn btn-primary'
                            onClick={() => setShowForm(true)}
                        >
                            Create Report
                        </button>
                    </div>
                </div>
            </div>

            {showCreateForm && (
                <div className="form-container">
                    <form className="formProfile" onSubmit={handleFormSubmit}>
                        <h3>Create Report</h3>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Employee ID:</label>
                                <input type="text" defaultValue={props.employeeId} name="employeeId" placeholder='EP######' />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Reason:</label>
                                <input type="text" name="reason" placeholder='Type in your reason' />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Content:</label>
                                <textarea type="text" name="content" placeholder='Type in what you want...' style={{ height: '15rem' }} />
                            </div>
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

        </div>
    );
}

export default Profile;