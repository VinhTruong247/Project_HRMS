import React from 'react'
import { useEffect, useState, useRef } from 'react';

function DailySalaryDetail(props) {
  const [dailySalaries, setDailySalaries] = useState([]);
  const [employeeData, setEmployeeData] = useState([]);
  const [expandedRows, setExpandedRows] = useState([]);
  const [departmentNames, setDepartmentNames] = useState([]);
  const [jobTitles, setJobTitles] = useState([]);
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
        setDailySalaries(dailysalaries)
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
        setEmployeeData(employees)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

  const handleRowExpand = (employeeId) => {
    const isRowExpanded = expandedRows.includes(employeeId);
    if (isRowExpanded) {
      setExpandedRows(expandedRows.filter(id => id !== employeeId));
    } else {
      setExpandedRows([...expandedRows, employeeId]);
    }
  };

  // Fetch department names and job titles
  useEffect(() => {
    fetch('https://localhost:7220/api/Department/departments', {
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
          throw new Error('API response was not ok.');
        }
      })
      .then(departments => {
        setDepartmentNames(departments);
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });

    fetch('https://localhost:7220/api/Job/jobs', {
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
          throw new Error('API response was not ok.');
        }
      })
      .then(jobs => {
        setJobTitles(jobs);
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
              <th>Employee ID</th>
              <th>Employee Name</th>
              <th>Email</th>
              <th>Department</th>
              <th>Job</th>
            </tr>
          </thead>
          <tbody>
            {employeeData.map(employee => (
              <React.Fragment key={employee.employeeId}>
                <tr onClick={() => handleRowExpand(employee.employeeId)}>
                  <td>{employee.employeeId}</td>
                  <td>{employee.firstName} {employee.lastName}</td>
                  <td>{employee.email}</td>
                  <td>
                    {departmentNames.find(
                      (department) => department.departmentId === employee.departmentId
                    )
                      ? departmentNames.find(
                        (department) => department.departmentId === employee.departmentId
                      ).departmentName
                      : ''}
                  </td>
                  <td>
                    {jobTitles.find((job) => job.jobId === employee.jobId)
                      ? jobTitles.find((job) => job.jobId === employee.jobId).jobTitle
                      : 'Unknown'}
                  </td>
                </tr>
                {expandedRows.includes(employee.employeeId) && (
                  <tr>
                    <td colSpan="5">
                      <table className='inner-table'>
                        <thead>
                          <tr>
                            <th>Pay Date</th>
                            <th>Salary per Hour</th>
                            <th>OT Hours</th>
                            <th>Daily Salary</th>
                            <th>Total Salary</th>
                          </tr>
                        </thead>
                        <tbody>
                          {dailySalaries
                          .filter((dailySalary) => dailySalary.employeeId === employee.employeeId)
                          .sort((a, b) => new Date(b.date) - new Date(a.date))
                          .map((dailySalary) => (
                            <tr key={dailySalary.dailysalaryId}>
                              <td>{dailySalary.date}</td>
                              <td>{dailySalary.salaryPerHour}</td>
                              <td>{dailySalary.otHours}</td>
                              <td>{dailySalary.dailySalary}</td>
                              <td>{dailySalary.totalSalary}</td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </td>
                  </tr>
                )}
              </React.Fragment>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default DailySalaryDetail;