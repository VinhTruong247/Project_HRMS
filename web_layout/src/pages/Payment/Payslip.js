import React from "react";
import { useEffect, useState, useRef } from "react";
import moment from "moment";
import { MDBDataTableV5 } from "mdbreact";

function Payslip(props) {
    const [data, setData] = useState([]);
    const [showCreateForm, setShowForm] = useState(false);
    const token = JSON.parse(localStorage.getItem("jwtToken"));
    const [updatePayslip, setUpdatePayslip] = useState(null);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [validationError, setValidationError] = useState("");
    const [employeeId, setEmployeeId] = useState([]);
    const [employeeNames, setEmployeeName] = useState([]);
    const [selectedReport, setSelectedReport] = useState(null);
    // const percentageRegex = /^\d+(\.\d{1,2})?%?$/;

    const handleEdit = (payslip) => {
        setUpdatePayslip(payslip);
        setShowUpdateForm(true);
    };

    const timeoutRef = useRef(null);

    const handleDoubleClick = (report) => {
        setSelectedReport(report);
    };


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
        fetch(`https://localhost:7220/api/PaySlip/get/paysliplist`, {
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
                setEmployeeName(employees);
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
            bhytPercentage: parseFloat(event.target.elements.bhytPercentage.value) / 100,
            note: event.target.elements.note.value,
        };

        if (!formData.payPeriod) {
            setValidationError("Pay period is required");
            return;
        }

        if (!formData.paidDate) {
            setValidationError('Pay date is required');
            return;
        }

        if (!formData.note) {
            setValidationError("Note is required");
            return;
        }

        if (!formData.bhytPercentage) {
            setValidationError('Input needed is required');
            return;
        }

        if (isNaN(formData.bhytPercentage)) {
            setValidationError('Input must be in number format');
            return;
        }

        if (formData.bhytPercentage < 0) {
            setValidationError('Input cannot be negative')
            return;
        }

        if (formData.bhytPercentage < 0 || formData.bhytPercentage > 100) {
            setValidationError('Value must be between 0 and 100')
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

        if (!formData.paidDate) {
            setValidationError('Pay date is required');
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
            <div className="row addbtn">
                <button className="btn_create" onClick={() => setShowForm(true)}>
                    Add Payslip
                </button>
            </div>

            <div className="card mb-3">
                <div className="card-body">
                    <div className="row">
                        <div className="col-2">
                            <h3 className="mb-0">Payment Details:</h3>
                        </div>
                        <div className="col-10 text-secondary">
                            {selectedReport && (
                                <div>

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Employee ID:</h4>
                                            <p>{selectedReport.employeeId}</p>
                                        </div>
                                        <div className="col-sm-8">
                                            <h4>Employee Name:</h4>
                                            <p>
                                                {employeeNames.find(employee => employee.employeeId === selectedReport.employeeId)
                                                    ? `${employeeNames.find(employee => employee.employeeId === selectedReport.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === selectedReport.employeeId).lastName}`
                                                    : 'Unknown'}
                                            </p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Pay Period:</h4>
                                            <p>{selectedReport.payPeriod}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Pay Date:</h4>
                                            <p>{selectedReport.paidDate}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Work Hour Status (OT Hours):</h4>
                                            <p>{selectedReport.actualWorkHours}H/{selectedReport.standardWorkHours}H ({selectedReport.otHours})</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Base Salary per Hour:</h4>
                                            <p>{selectedReport.baseSalaryPerHour.toLocaleString()}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Total Base Salary:</h4>
                                            <p>{selectedReport.baseSalary.toLocaleString()}</p>
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>Allowance:</h4>
                                            <p>{selectedReport.allowance.toLocaleString()}</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Health Insurance Deduction Amount (%):</h4>
                                            <p>{selectedReport.bhytAmount.toLocaleString()} ({selectedReport.bhytPercentage * 100}%)</p>
                                        </div>
                                        <div className="col-sm-8">
                                            <h4>Before Deduction:</h4>
                                            <p>{selectedReport.totalBeforeDeduction.toLocaleString()}</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Personal Exemption:</h4>
                                            <p>{selectedReport.giamTruGiaCanh.toLocaleString()}</p>
                                        </div>
                                        <div className="col-sm-8">
                                            <h4>Dependent Exemption ({selectedReport.dependent} person(s)):</h4>
                                            <p>{selectedReport.giamTruGiaCanhNguoiPhuThuoc.toLocaleString()}</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-4">
                                            <h4>Tax Income:</h4>
                                            <p style={{color: 'red', fontWeight: 'bold'}}>{selectedReport.taxIncome}</p>
                                        </div>
                                        <div className="col-sm-8">
                                            <h4>Tax:</h4>
                                            <p>{selectedReport.tax}</p>
                                        </div>
                                    </div>
                                    <hr />

                                    <div className="row">
                                        <div className="col-sm-8">
                                            <h4>Final Outcome:</h4>
                                            <p style={{color: 'green', fontWeight: 'bold'}}>{selectedReport.totalSalary.toLocaleString()}</p>
                                        </div>
                                        <div className="col-sm-2">
                                            <h4>Contract ID:</h4>
                                            <p>{selectedReport.contractId}</p>
                                        </div>
                                        <div className="col-sm-2">
                                            <h4>Status:</h4>
                                            <p>{selectedReport.status}</p>
                                        </div>
                                    </div>

                                </div>
                            )}
                        </div>
                    </div>
                </div>
            </div>

            <div className="row">
                <MDBDataTableV5
                    className='custom-table'
                    data={{
                        columns: [
                            {
                                label: 'Payslip ID',
                                field: 'payslipId',
                                width: 150,
                            },
                            {
                                label: 'Employee Name',
                                field: 'employeeName',
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
                        rows: data.map((payslip) => ({
                            payslipId: payslip.payslipId,
                            employeeName: employeeNames.find(employee => employee.employeeId === payslip.employeeId)
                                ? `${employeeNames.find(employee => employee.employeeId === payslip.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === payslip.employeeId).lastName}`
                                : 'Unknown',
                            status: payslip.status,
                            options: (
                                <button onClick={() => handleEdit(payslip)}>Edit</button>
                            ),
                            clickEvent: () => handleDoubleClick(payslip)
                        }))
                    }}
                    hover
                    entriesOptions={[5, 10, 20]}
                    entries={5}
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
                        <h3>Generate Payslip</h3>

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
                                <input type="date" name="paidDate" style={{ height: '3.3rem' }} />
                            </div>
                        </div>

                        <div className='row'>
                            <div className="col-12 mt-3">
                                <label>Note:</label>
                                <textarea type="text" name="note" placeholder='Type in what you want...' style={{ height: '15rem' }} />
                            </div>
                        </div>

                        <div className="row">
                            <div className="col-12 mt-3">
                                <label>BHYT:</label>
                                <input type="text" name="bhytPercentage" placeholder='Number' />
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
                <div className="form-container">
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
                                <input type="date" name="paidDate" defaultValue={moment(moment(updatePayslip.paidDate, 'DD-MM-YYYY')).format('YYYY-MM-DD')} style={{ height: '3.3rem' }} />
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
        </div>
    );
}

export default Payslip;