import React from 'react'
import { useEffect, useState, useRef } from 'react';
import { MDBDataTableV5 } from 'mdbreact';

function DailySalaryDetail(props) {
  const [data, setData] = useState([]);
  const [employeeNames, setEmployeeNames] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));

  //  Get info of Daily Salaries
  useEffect(() => {
    fetch('https://localhost:7220/api/DailySalary/dailysalaries', {
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
      .then(dailysalaries => {
        setData(dailysalaries)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

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
        setEmployeeNames(employees)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

  return (
    <div className="manager" style={{ position: "relative", marginTop: "5rem" }}>

      <div className='row'>
        <table className='table'>
          <thead>
            <tr>
              <th>ID</th>
              <th>Employee Name</th>
              <th>Pay Date</th>
              <th>Daily Salary</th>
              <th>Total Salary</th>
            </tr>
          </thead>
          <tbody>
            {data.map(dailysalary => (

              <tr key={dailysalary.dailysalaryId}>
                <td>{dailysalary.dailysalaryId}</td>
                <td>
                  {employeeNames.find(employee => employee.employeeId === dailysalary.employeeId)
                    ? `${employeeNames.find(employee => employee.employeeId === dailysalary.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === dailysalary.employeeId).lastName}`
                    : 'Unknown'}
                </td>
                <td>{dailysalary.date}</td>
                <td>{dailysalary.dailySalary.toLocaleString()}</td>
                <td>{dailysalary.totalSalary.toLocaleString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}
export default DailySalaryDetail;