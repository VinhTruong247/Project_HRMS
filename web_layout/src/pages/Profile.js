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

// function EmployeeDetails(props) {

//     return (
//         <div className="card mb-3">
//             <div className="card-body">
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Full Name</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">{props.firstName} {props.lastName}</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">First Name</h6>
//                     </div>
//                     <div className="col-sm-3 text-secondary">{props.firstName}</div>
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Last Name</h6>
//                     </div>
//                     <div className="col-sm-3 text-secondary">{props.lastName}</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Date of Birth</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">{props.dateOfBirth}</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Email</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">{props.email}</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Phone</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">{props.phoneNumber}</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Address</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">{props.employeeAddress}</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Skills</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">
//                         React, Node.js, MongoDB, Express.js
//                     </div>
//                 </div>
//             </div>
//         </div>
//     );
// }

function EmployeeDetails(props) {
    const [data, setData] = useState(props);
    const [isEditing, setIsEditing] = useState(false);
    const token = JSON.parse(localStorage.getItem('jwtToken'));
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
    };

    const handleSubmit = () => {
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
    };

    if (isEditing) {
        return (
            <div className="card mb-3">
                <div className="card-body">

                    <form onSubmit={handleSubmit}>
                        <div className="row">
                            <div className="col-sm-3">
                                <h6 className="mb-0">First Name</h6>
                            </div>
                            <div className="col-sm-9">
                                <input
                                    type="text"
                                    name="firstName"
                                    value={formData.firstName}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6 className="mb-0">Last Name</h6>
                            </div>
                            <div className="col-sm-9">
                                <input
                                    type="text"
                                    name="lastName"
                                    value={formData.lastName}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6 className="mb-0">Date of Birth</h6>
                            </div>
                            <div className="col-sm-9">
                                <input
                                    type="date"
                                    name="dateOfBirth"
                                    value={formData.dateOfBirth}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6 className="mb-0">Email</h6>
                            </div>
                            <div className="col-sm-9">
                                <input
                                    type="email"
                                    name="email"
                                    value={formData.email}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6 className="mb-0">Phone</h6>
                            </div>
                            <div className="col-sm-9">
                                <input
                                    type="tel"
                                    name="phoneNumber"
                                    value={formData.phoneNumber}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6 className="mb-0">Address</h6>
                            </div>
                            <div className="col-sm-9">
                                <input
                                    type="text"
                                    name="employeeAddress"
                                    value={formData.employeeAddress}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                        <hr />
                        <button type="submit" className="btn btn-primary">
                            Save Changes
                        </button>
                        <button
                            type="button"
                            className="btn btn-secondary ml-2"
                            onClick={() => setIsEditing(false)}
                        >
                            Cancel
                        </button>

                    </form>
                </div>
            </div>
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
