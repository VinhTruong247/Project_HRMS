import React from "react";
import { useEffect, useState, useRef } from "react";
import moment from "moment";

function Payslip(props) {
    const [data, setData] = useState([]);
    const [showCreateForm, setShowForm] = useState(false);
    const token = JSON.parse(localStorage.getItem("jwtToken"));
    const [updatePayslip, setUpdatePayslip] = useState(null);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [validationError, setValidationError] = useState("");
    const [employeeId, setEmployeeId] = useState([]);
    const handleEdit = (payslip) => {
        setUpdatePayslip(payslip);
        setShowUpdateForm(true);
    };

    const timeoutRef = useRef(null);

    useEffect(() => {
        if (validationError) {
            timeoutRef.current = setTimeout(() => {
                setValidationError("");
            }, 3000);
        }
        return () => {
            clearTimeout(timeoutRef.current);
        };
    }, [validationError]);

    //  Get fulllist of PAYSLIP
    useEffect(() => {
        fetch("https://localhost:7220/api/PaySlip/get/paysliplist", {
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
                setData(payslips);
            })
            .catch((error) => {
                console.error("There was a problem with the fetch operation:", error);
            });
    }, []);

    // Fetch employee
    useEffect(() => {
        fetch('https://localhost:7220/api/Employee/employees', {
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
                    throw new Error("API response was not ok.");
                }
            })
            .then((employees) => {
                setEmployeeId(employees);
            })
            .catch((error) => {
                console.error("There was a problem with the fetch operation:", error);
            });
    }, []);

    //  GENERATES NEW PAYSLIP
    const handleFormSubmit = (event) => {
        event.preventDefault();
        const formData = {
            employeeId: event.target.elements.employeeId.value,
            payPeriod: event.target.elements.payPeriod.value,
            paidDate: event.target.elements.paidDate.value,
            note: event.target.elements.note.value,
        };

        if (!formData.payPeriod) {
            setValidationError("Pay period is required");
            return;
        }

        if (!formData.note) {
            setValidationError("Note is required");
            return;
        }

        fetch("https://localhost:7220/api/Payslip/generate", {
            method: "POST",
            headers: {
                'Content-Type': "application/json",
                'Authorization': `Bearer ${token.token}`,
            },
            body: JSON.stringify(formData),
        })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error("Api response was not ok.");
                }
            })
            .then((payslip) => {
                setData([...data, payslip]);
                setShowForm(false);
                setValidationError("");
                console.log("Payslip created successfully");
            })
            .catch((error) => {
                console.error("Error submitting form:", error);
                setValidationError("An error occurred while submitting the form");
            });
        console.log(event.target.elements);
    };

    //  UPDATE NEW EMPLOYEE
    const handleUpdate = (event) => {
        event.preventDefault();
        const formData = {
            payslipId: updatePayslip.payslipId,
            payPeriod: event.target.elements.payPeriod.value,
            paidDate: event.target.elements.paidDate.value,
            note: event.target.elements.note.value,
            status: event.target.elements.status.value,
        };

        if (!formData.payPeriod) {
            setValidationError("Pay period is required");
            return;
        }

        if (!formData.note) {
            setValidationError("Note is required");
            return;
        }

        fetch(
            `https://localhost:7220/api/Payslip/update/payslip/${updatePayslip.employeeId}/${updatePayslip.payslipId}`,
            {
                method: "PUT",
                headers: {
                    'Content-Type': "application/json",
                    'Authorization': `Bearer ${token.token}`,
                },
                body: JSON.stringify(formData),
            }
        )
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error("Api response was not ok.");
                }
            })
            .then((updatedPayslip) => {
                const updatedData = data.map((payslip) => {
                    if (payslip.payslipId === updatedPayslip.payslipId) {
                        return updatedPayslip;
                    } else {
                        return payslip;
                    }
                });
                setData(updatedData);
                setShowUpdateForm(false);
                setUpdatePayslip(null);
                console.log("Payslip updated successfully");
            })
            .catch((error) => {
                console.error("Error submitting form:", error);
                setValidationError(
                    "An error occurred while updating payslip information"
                );
            });
    };

    return (
        <div className="manager" style={{ position: "relative" }}>
            {showCreateForm && (
                <div className="form-container">
                    <form className="form" onSubmit={handleFormSubmit}>
                        <h3>Create Payslip</h3>

                        {validationError && (
                            <div className="error-message-fadeout">{validationError}</div>
                        )}

                        <div className='row name'>
                            <div className="col-12 mt-3">
                                <label>Employee Id:</label>
                                <select name="employeeId" onChange={event => console.log(event.target.value)}>
                                    {employeeId.map(employee => (
                                        <option key={employee.employeeId} value={employee.employeeId}>
                                            {employee.employeeId}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className='row'>
                        <div className="col-6 mt-3">
                                <label>Pay Period:</label>
                                <select name="payPeriod" defaultValue="Q1" onChange={event => console.log(event.target.value)}>
                                    <option value="Q1">Q1</option>
                                    <option value="Q2">Q2</option>
                                    <option value="Q3">Q3</option>
                                    <option value="Q4">Q4</option>
                                </select>
                            </div>

                            <div className="col-6 mt-3">
                                <label>Paid Date:</label>
                                <input type="date" name="paidDate" style={{ height: '3.3rem' }}/>
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Note:</label>
                                <textarea type="text" name="note" placeholder='Type in what you want...' style={{ height: '15rem' }} />
                            </div>
                        </div>

                        <div className="row butt">
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
                <div className="detail-container">
                    <form className="form" onSubmit={handleUpdate}>
                        <h3>Edit Payslip ID: {updatePayslip.payslipId}</h3>
                        <h4>(Employee: {updatePayslip.employeeId})</h4>

                        {validationError && (
                            <div className="error-message-fadeout">
                                {validationError}
                            </div>
                        )}

                        <div className='row'>
                            <div className="col-6 mt-3">
                                <label>Pay Period:</label>
                                <select name="payPeriod" defaultValue={updatePayslip.payPeriod} onChange={event => console.log(event.target.value)}>
                                    <option value="Q1">Q1</option>
                                    <option value="Q2">Q2</option>
                                    <option value="Q3">Q3</option>
                                    <option value="Q4">Q4</option>
                                </select>
                            </div>

                            <div className="col-6 mt-3">
                                <label>Paid Date:</label>
                                <input type="date" name="paidDate" defaultValue={moment(moment(updatePayslip.paidDate, 'DD-MM-YYYY')).format('YYYY-MM-DD')} style={{ height: '3.3rem' }}/>
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Note:</label>
                                <textarea type="text" name="note" defaultValue={updatePayslip.note} style={{ height: '15rem' }} />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-3 mt-3"></div>
                            <div className="col-6 mt-3">
                                <label>Status:</label>
                                <select name="status" defaultValue={updatePayslip.status} onChange={event => console.log(event.target.value)}>
                                    <option value="Approved">Approved</option>
                                    <option value="Pending">Pending</option>
                                    <option value="Declined">Declined</option>
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
            <div className="row addbtn">
                {" "}
                <button className="btn_create" onClick={() => setShowForm(true)}>
                    Add Payslip
                </button>
            </div>

            <div className="row">
                <table className="table">
                    <thead>
                        <tr>
                            <th>Payslip ID</th>
                            <th>Employee ID</th>
                            <th>Pay Period</th>
                            <th>Paid Date</th>
                            <th>Base Salary</th>
                            <th>OT Hours</th>
                            <th>OT Salary</th>
                            <th>Allowance</th>
                            <th>Tax Income</th>
                            <th>Tax</th>
                            <th>Total Salary</th>
                            <th>Status</th>
                            <th>Option</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map((payslip) => (
                            <tr key={payslip.payslipId}>
                                <td>{payslip.payslipId}</td>
                                <td>{payslip.employeeId}</td>
                                <td>{payslip.payPeriod}</td>
                                <td>{payslip.paidDate}</td>
                                <td>{payslip.baseSalary.toLocaleString()}</td>
                                <td>{payslip.otHours}</td>
                                <td>{payslip.otSalary ? payslip.otSalary.toLocaleString() : 'N/A'}</td>
                                <td>{payslip.allowance ? payslip.allowance.toLocaleString() : 'N/A'}</td>
                                <td>{payslip.taxIncome ? payslip.taxIncome.toLocaleString() : 'N/A'}</td>
                                <td>{payslip.tax ? payslip.tax.toLocaleString() : 'N/A'}</td>
                                {/* <td>
                                    {jobTitles.find(job => job.jobId === payslip.employeeId)
                                        ? jobTitles.find(job => job.jobId === payslip.employeeId).jobTitle
                                        : 'Unknown'}
                                </td> */}
                                <td>{payslip.totalSalary.toLocaleString()}</td>
                                <td>{payslip.status}</td>
                                <td>
                                    <button onClick={() => handleEdit(payslip)}>Edit</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default Payslip;