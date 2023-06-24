// import React, { useState, useEffect } from "react";
// import 'bootstrap/dist/css/bootstrap.min.css';

// const { decodedToken } = props.location.state;

// function EmployeeCard() {
//     return (
//         <div className="card">
//             <div className="card-body">
//                 <div className="d-flex flex-column align-items-center text-center">
//                     <img src="#" alt="employees-img" className="rounded-circle" />
//                     <div className="mt-3">
//                         <h4>{decodedToken.username}</h4>
//                         <p className="text-secondary mb-1">Full Stack Developer</p>
//                         <p className="text-secondary mb-1">Back-end #1</p>
//                         <p className="text-secondary mb-1">ID0001</p>
//                     </div>
//                 </div>
//             </div>
//         </div>
//     );
// }

// function EmployeeLinks() {
//     return (
//         <div className="card mt-3">
//             <ul className="list-group">
//                 <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
//                     <h6>
//                         <i className="fa fa-linkedin"></i> LindIn
//                     </h6>
//                     <span className="text-secondary">#</span>
//                 </li>
//                 <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
//                     <h6>
//                         <i className="fa fa-github"></i> Github
//                     </h6>
//                     <span className="text-secondary">bootdey</span>
//                 </li>
//                 <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
//                     <h6>
//                         <i className="fa fa-twitter"></i> Twitter
//                     </h6>
//                     <span className="text-secondary">@bootdey</span>
//                 </li>
//                 <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
//                     <h6>
//                         <i className="fa fa-facebook"></i> Facebook
//                     </h6>
//                     <span className="text-secondary">bootdey</span>
//                 </li>
//             </ul>
//         </div>
//     );
// }

// function EmployeeDetails() {
//     return (
//         <div className="card mb-3">
//             <div className="card-body">
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Full Name</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">Robert Truong</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">First Name</h6>
//                     </div>
//                     <div className="col-sm-3 text-secondary">Robert</div>
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Last Name</h6>
//                     </div>
//                     <div className="col-sm-3 text-secondary">Truong</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Date of Birth</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">24-7-1999</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Email</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">
//                         {decodedToken.email}
//                     </div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Phone</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">123-247-2001</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Mobile</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">123-247-2001</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Address</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">HCM</div>
//                 </div>
//                 <hr />
//                 <div className="row">
//                     <div className="col-sm-3">
//                         <h6 className="mb-0">Address</h6>
//                     </div>
//                     <div className="col-sm-9 text-secondary">HCM</div>
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

// function Profile() {
//     return (
//         <div className="main-body">
//             <div className="row gutters-sm">
//                 <div className="col-md-4 mb-3">
//                     <EmployeeCard />
//                     <EmployeeLinks />
//                 </div>
//                 <div className="col-md-8">
//                     <EmployeeDetails />
//                 </div>
//             </div>
//         </div>
//     );
// }

// export default Profile;


import React from "react";
import { useLocation } from 'react-router-dom';

function Profile(props) {
    const location = useLocation();
    const decodedToken = location.state.decodedToken;

    console.log({decodedToken})
    return (
        <div className="main-body">
            <div className="row gutters-sm">
                <div className="col-md-4 mb-3">
                    <EmployeeCard decodedToken={decodedToken} />
                    <EmployeeLinks decodedToken={decodedToken} />
                </div>
                <div className="col-md-8">
                    <EmployeeDetails decodedToken={decodedToken} />
                </div>
            </div>
        </div>
    );
}

function EmployeeCard(props) {
    const decodedToken = props.decodedToken;

    return (
        <div className="card">
            <div className="card-body">
                <div className="d-flex flex-column align-items-center text-center">
                    <img src="#" alt="employees-img" className="rounded-circle" />
                    <div className="mt-3">
                        <h4>{decodedToken.username}</h4>
                        <p className="text-secondary mb-1">Full Stack Developer</p>
                        <p className="text-secondary mb-1">Back-end #1</p>
                        <p className="text-secondary mb-1">ID0001</p>
                    </div>
                </div>
            </div>
        </div>
    );
}

function EmployeeLinks(props) {
    const decodedToken = props.decodedToken;

    return (
        <div className="card mt-3">
            <ul className="list-group">
                <li className="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                    <h6>
                        <i className="fa fa-linkedin"></i> LindIn
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
    const decodedToken = props.decodedToken;

    return (
        <div className="card mb-3">
            <div className="card-body">
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Full Name</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">Robert Truong</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">First Name</h6>
                    </div>
                    <div className="col-sm-3 text-secondary">Robert</div>
                    <div className="col-sm-3">
                        <h6 className="mb-0">Last Name</h6>
                    </div>
                    <div className="col-sm-3 text-secondary">Truong</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Date of Birth</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">24-7-1999</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Email</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">{decodedToken.email}</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Phone</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">123-247-2001</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Mobile</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">123-247-2001</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Address</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">HCM</div>
                </div>
                <hr />
                <div className="row">
                    <div className="col-sm-3">
                        <h6 className="mb-0">Address</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">HCM</div>
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
            </div>
        </div>
    );
}

export default Profile;