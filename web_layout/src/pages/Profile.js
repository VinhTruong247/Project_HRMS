import React, { useState, useEffect, useRef } from "react";
import useData from "../hooks/useData";
import moment from "moment";

function Profile(props) {

    const _dataContext = useData();
    const token = JSON.parse(localStorage.getItem('jwtToken'));
    const [selectedPayslip, setPayslip] = useState([]);
    const [showPayslipForm, setShowPayslipForm] = useState(false);

    const today = new Date();
    const year = today.getFullYear();
    const month = ('0' + (today.getMonth() + 1)).slice(-2);
    const todayString = `${month}-${year}`;

    const [selectedMonth, setSelectedMonth] = useState(todayString);
    const [displayMonth, setDisplayMonth] = useState(todayString);
    const [apiMonth, setApiMonth] = useState(todayString);

    const handleMonthChange = (event) => {
        const selectedDate = event.target.value;
        const [year, month] = selectedDate.split('-');
        const formattedMonth = `${month}-01-${year}`;
        setSelectedMonth(formattedMonth);
        setDisplayMonth(selectedDate);
        setApiMonth(formattedMonth);
    };

    const handleCancel = () => {
        setSelectedMonth(todayString);
        setShowPayslipForm(false);
    };

    useEffect(() => {
        const fetchData = async () => {
            if (_dataContext.employeeId) {
                try {
                    const response = await fetch(`https://localhost:7220/api/PaySlip/get/payslip/${_dataContext.employeeId}/${apiMonth}`, {
                        method: 'GET',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${token.token}`,
                        },
                    });

                    if (!response.ok) {
                        throw new Error("Api response was not ok.");
                    }

                    const payslips = await response.json();
                    setPayslip(payslips);
                } catch (error) {
                    console.error("There was a problem with the fetch operation:", error);
                }
            }
        };

        fetchData();
    }, [_dataContext.employeeId, selectedMonth]);

    return _dataContext && (
        <div className="main-body payslip">
            <button className="view-payslip-button btn btn-primary mb-3" onClick={() => setShowPayslipForm(true)}>View Payslip</button>
            <div className="row gutters-sm">
                <div className="col-md-4">
                    <EmployeeCard
                        lastName={_dataContext.lastName}
                        firstName={_dataContext.firstName}
                        employeeId={_dataContext.employeeId}
                        departmentId={_dataContext.departmentId}
                        jobId={_dataContext.jobId}
                    />
                    <EmployeeLinks />
                </div>
                <div className="col-md-8">
                    <EmployeeDetails
                        employeeId={_dataContext.employeeId}
                        lastName={_dataContext.lastName}
                        firstName={_dataContext.firstName}
                        dateOfBirth={_dataContext.dateOfBirth}
                        email={_dataContext.email}
                        phoneNumber={_dataContext.phoneNumber}
                        employeeAddress={_dataContext.employeeAddress}
                    />
                </div>
            </div>

            {showPayslipForm && (
                <div className="form-container">
                    <form className="formpayslip">

                        <div className="row mb-2">
                            <div className="col-sm-5">
                                <h4>Select Month:</h4>
                                <input
                                    type="month"
                                    value={displayMonth}
                                    onChange={handleMonthChange}
                                />
                            </div>
                        </div>

                        <div>
                            <div className="row">
                                <div className="col-sm-3">
                                    <h4>Payslip ID:</h4>
                                    <p>{selectedPayslip.payslipId}</p>
                                </div>
                                <div className="col-sm-9">
                                    <h4>Employee ID:</h4>
                                    <p>{selectedPayslip.employeeId}</p>
                                </div>
                            </div>
                            <hr />

                            <div className="row">
                                <div className="col-sm-3">
                                    <h4>Pay Period:</h4>
                                    <p>{selectedPayslip.payPeriod}</p>
                                </div>
                                <div className="col-sm-9">
                                    <h4>Pay Date:</h4>
                                    <p>{selectedPayslip.paidDate}</p>
                                </div>
                            </div>
                            <hr />

                            <div className="row">
                                <div className="col-sm-3">
                                    <h4>Base Salary per Hour:</h4>
                                    <p>{selectedPayslip.baseSalaryPerHour.toLocaleString()}</p>
                                </div>
                                <div className="col-sm-3">
                                    <h4>Work Hour Status (OT Hours):</h4>
                                    <p>{selectedPayslip.actualWorkHours}H/{selectedPayslip.standardWorkHours}H ({selectedPayslip.otHours})</p>
                                </div>
                                <div className="col-sm-3">
                                    <h4>Total Base Salary:</h4>
                                    <p>{selectedPayslip.baseSalary.toLocaleString()}</p>
                                </div>
                                <div className="col-sm-3">
                                    <h4>OT Salary:</h4>
                                    <p>{selectedPayslip.otSalary.toLocaleString()}</p>
                                </div>
                            </div>
                            <hr />

                            <div className="row">
                                <div className="col-sm-6">
                                    <h4>Health Insurance Deduction Amount (%):</h4>
                                    <p>{selectedPayslip.bhytAmount.toLocaleString()} ({selectedPayslip.bhytPercentage * 100}%)</p>
                                </div>
                                <div className="col-sm-3">
                                    <h4>Before Deduction:</h4>
                                    <p>{selectedPayslip.totalBeforeDeduction.toLocaleString()}</p>
                                </div>
                                <div className="col-sm-3">
                                    <h4>Allowance:</h4>
                                    <p>{selectedPayslip.allowance.toLocaleString()}</p>
                                </div>
                            </div>
                            <hr />

                            <div className="row">
                                <div className="col-sm-3">
                                    <h4>Personal Exemption:</h4>
                                    <p>{selectedPayslip.giamTruGiaCanh.toLocaleString()}</p>
                                </div>
                                <div className="col-sm-9">
                                    <h4>Dependent Exemption ({selectedPayslip.dependent} person(s)):</h4>
                                    <p>{selectedPayslip.giamTruGiaCanhNguoiPhuThuoc.toLocaleString()}</p>
                                </div>
                            </div>
                            <hr />

                            <div className="row">
                                <div className="col-sm-3">
                                    <h4>Tax Income:</h4>
                                    <p style={{ color: 'red', fontWeight: 'bold' }}>{selectedPayslip.taxIncome}</p>
                                </div>
                                <div className="col-sm-9">
                                    <h4>Tax:</h4>
                                    <p>{selectedPayslip.tax}</p>
                                </div>
                            </div>
                            <hr />

                            <div className="row">
                                <div className="col-sm-8">
                                    <h4>Final Outcome:</h4>
                                    <p style={{ color: 'green', fontWeight: 'bold' }}>{selectedPayslip.totalSalary.toLocaleString()}</p>
                                </div>
                                <div className="col-sm-2">
                                    <h4>Contract ID:</h4>
                                    <p>{selectedPayslip.contractId}</p>
                                </div>
                                <div className="col-sm-2">
                                    <h4>Status:</h4>
                                    <p>{selectedPayslip.status}</p>
                                </div>
                            </div>
                            <hr />

                            <div className='row butt' style={{ justifyContent: 'right' }}>
                                <button onClick={handleCancel}>Cancel</button>
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
    const [showOTForm, setOTForm] = useState(false);
    const [validationError, setValidationError] = useState('');

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

    const handleOTFormSubmit = (event) => {
        event.preventDefault();
        const formData = {
            employeeId: event.target.elements.employeeId.value,
            overtimeType: event.target.elements.overtimeType.value,
            day: event.target.elements.day.value,
            overtimeHours: event.target.elements.overtimeHours.value,
        };

        const selectedDate = new Date(formData.day);
        const currentDate = new Date();

        const twoDaysFromNow = new Date();
        twoDaysFromNow.setDate(currentDate.getDate() + 2);

        if (selectedDate < twoDaysFromNow) {
            setValidationError('The day must be 2 days ahead from today');
            return;
        }

        if (!formData.overtimeHours) {
            setValidationError('Content needed is required');
            return;
        }

        const timeRegex = /^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$/;
        if (!timeRegex.test(formData.overtimeHours)) {
            setValidationError('Invalid time format. Please use the format HH:MM:SS');
            return;
        }

        const hours = parseInt(formData.overtimeHours.split(':')[0], 10);
        if (hours > 24) {
            setValidationError('Invalid hours. Hours must not exceed 24.');
            return;
        }

        fetch('https://localhost:7220/api/Overtime/create', {
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
            .then(overtime => {
                setData([...data, overtime]);
                setOTForm(false);
                setValidationError('');
                console.log('Report form created successfully');
            })
            .catch(error => {
                setValidationError('');
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
                                    defaultValue={props.firstName}
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
                                    defaultValue={props.lastName}
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
                                    defaultValue={props.email}
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
                                    defaultValue={props.phoneNumber}
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
                                    defaultValue={props.employeeAddress}
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
                <div className="row form">
                    <div className="col-sm-8" style={{ justifyContent: 'left' }}>
                        <button
                            type="button"
                            className="btn btn-edit"
                            onClick={() => setIsEditing(true)}
                        >
                            Edit
                        </button>
                    </div>

                    <div className="col-sm-2" style={{ justifyContent: 'right' }}>
                        <button
                            type="button"
                            className='btn btn-primary'
                            onClick={() => setOTForm(true)}
                        >
                            Request OT
                        </button>
                    </div>

                    <div className="col-sm-2" style={{ justifyContent: 'right' }}>
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
                                <input type="text" defaultValue={props.employeeId} name="employeeId" readOnly />
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

            {showOTForm && (
                <div className="form-container">
                    <form className="formProfile" onSubmit={handleOTFormSubmit}>
                        <h3>Request OT</h3>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Employee ID:</label>
                                <input type="text" defaultValue={props.employeeId} name="employeeId" readOnly />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Overtime Type:</label>
                                <select name="overtimeType" onChange={event => console.log(event.target.value)}>
                                    <option value="Night Shift">Night Shift</option>
                                    <option value="Weekend">Weekend</option>
                                    <option value="Holiday">Holiday</option>
                                </select>
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Day:</label>
                                <input type="date" name="day" />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Overtime Hours Required:</label>
                                <input type="text" name="overtimeHours" placeholder='HH:MM:SS' />
                            </div>
                        </div>

                        <div className='row butt'>
                            <div className="col-5 mt-3">
                                <button type="submit">Submit</button>
                            </div>
                            <div className="col-5 mt-3">
                                <button onClick={() => setOTForm(false)}>Cancel</button>
                            </div>
                        </div>

                    </form>
                </div>
            )}

        </div>
    );
}

export default Profile;
