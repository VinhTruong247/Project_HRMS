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
    <div className="manager" style={{ position: "relative" }}>

      <div className='row'>

        <MDBDataTableV5
          className='custom-table'
          data={{
            columns: [
              {
                label: 'ID',
                field: 'dailysalaryId',
                width: 150,
              },
              {
                label: 'Employee Name',
                field: 'employeeName',
                width: 150,
              },
              {
                label: 'Pay Date',
                field: 'date',
                width: 150,
              },
              {
                label: 'Daily Salary',
                field: 'dailySalary',
                width: 150,
              },
              {
                label: 'Total Salary',
                field: 'totalSalary',
                width: 150,
              },
            ],
            rows: data.map((dailysalary) => ({
              dailysalaryId: dailysalary.dailysalaryId,
              employeeName: employeeNames.find(employee => employee.employeeId === dailysalary.employeeId)
                ? `${employeeNames.find(employee => employee.employeeId === dailysalary.employeeId).firstName} ${employeeNames.find(employee => employee.employeeId === dailysalary.employeeId).lastName}`
                : 'Unknown',
              date: dailysalary.date,
              dailySalary: dailysalary.dailySalary.toLocaleString(),
              totalSalary: dailysalary.totalSalary.toLocaleString(),
            }))
          }}
          hover
          entriesOptions={[5, 10, 20]}
          entries={10}
          pagesAmount={5}
          searchTop
          searchBottom={false}
        />
      </div>
    </div>
  )
}
export default DailySalaryDetail;