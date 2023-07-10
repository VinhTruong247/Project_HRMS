import React, { useState, useEffect } from "react";
import useData from "../hooks/useData";

function Profile(props) {

    const data = useData()

    return data && (
        <div className="main-body">
            <div className="row gutters-sm">
                <div className="col-md-4 mb-3">
                    <EmployeeCard lastName={data.lastName} firstName={data.firstName} employeeId={data.id} departmentId={data.departmentId} jobId={data.jobId} />
                    <EmployeeLinks />
                </div>
                <div className="col-md-8">
                    <EmployeeDetails employeeId={data.employeeId} lastName={data.lastName} firstName={data.firstName} dateOfBirth={data.dateOfBirth} email={data.email} phoneNumber={data.phoneNumber} employeeAddress={data.employeeAddress} />
                </div>
            </div>
        </div>
    );
}

function EmployeeCard(props) {
    return (
        <div className="card">
            <div className="card-body">
                <div className="d-flex flex-column align-items-center text-center">
                    <img src="#" alt="employees-img" className="rounded-circle" />
                    <div className="mt-3">
                        <h4>{props.firstName} {props.lastName}</h4>
                        <p className="text-secondary mb-1">{props.departmentId}</p>
                        <p className="text-secondary mb-1">{props.jobId}</p>
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
                                    value={props.dateOfBirth}
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
                <button
                    type="button"
                    className="btn btn-primary"
                    onClick={() => setIsEditing(true)}
                >
                    Edit
                </button>
            </div>
        </div>
    );
}
export default Profile;
