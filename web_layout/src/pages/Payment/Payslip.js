import React from 'react';
import { useEffect, useState, useRef } from 'react';

function Payslip(props) {
    const token = JSON.parse(localStorage.getItem('jwtToken'));
    const [employeeId, setEmployeeId] = useState('');
    const [payPeriod, setPayPeriod] = useState('');
    const [paidDate, setPaidDate] = useState('');
    const [note, setNote] = useState('');
    const [bankAccountNumber, setBankAccountNumber] = useState(0);
    const [bankAccountName, setBankAccountName] = useState('');
    const [bankName, setBankName] = useState('');
    const [approval, setApproval] = useState('');
    const [status, setStatus] = useState('');

    const handleEmployeeIdChange = (event) => {
        setEmployeeId(event.target.value);
    };

    const handlePayPeriodChange = (event) => {
        setPayPeriod(event.target.value);
    };

    const handlePaidDateChange = (event) => {
        setPaidDate(event.target.value);
    };

    const handleNoteChange = (event) => {
        setNote(event.target.value);
    };

    const handleBankAccountNumberChange = (event) => {
        setBankAccountNumber(event.target.value);
    };

    const handleBankAccountNameChange = (event) => {
        setBankAccountName(event.target.value);
    };

    const handleBankNameChange = (event) => {
        setBankName(event.target.value);
    };

    const handleApprovalChange = (event) => {
        setApproval(event.target.value);
    };

    const handleStatusChange = (event) => {
        setStatus(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        const paymentData = {
            employeeId: employeeId,
            payPeriod: payPeriod,
            paidDate: paidDate,
            note: note,
            bankAccountNumber: bankAccountNumber,
            bankAccountName: bankAccountName,
            bankName: bankName,
            approval: approval,
            status: status
        };

        fetch('https://localhost:7220/api/PaySlip/generate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token.token}`
            },
            body: JSON.stringify(paymentData)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
            })
            .catch(error => {
                console.log(error);

            });
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <label>
                    Employee ID:
                    <input type="text" value={employeeId} onChange={handleEmployeeIdChange} />
                </label>
                <br />
                <label>
                    Pay Period:
                    <input type="text" value={payPeriod} onChange={handlePayPeriodChange} />
                </label>
                <br />
                <label>
                    Paid Date:
                    <input type="date" value={paidDate} onChange={handlePaidDateChange} />
                </label>
                <br />
                <label>
                    Note:
                    <input type="text" value={note} onChange={handleNoteChange} />
                </label>
                <br />
                <label>
                    Bank Account Number:
                    <input type="number" value={bankAccountNumber} onChange={handleBankAccountNumberChange} />
                </label>
                <br />
                <label>
                    Bank Account Name:
                    <input type="text" value={bankAccountName} onChange={handleBankAccountNameChange} />
                </label>
                <br />
                <label>
                    Bank Name:
                    <input type="text" value={bankName} onChange={handleBankNameChange} />
                </label>
                <br />
                <label>
                    Approval:
                    <input type="text" value={approval} onChange={handleApprovalChange} />
                </label>
                <br />
                <label>
                    Status:
                    <input type="text" value={status} onChange={handleStatusChange} />
                </label>
                <br />
                <input type="submit" value="Submit Payment" />
            </form>
        </div>
    );
}
export default Payslip;
