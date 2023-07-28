import React from 'react'
import { useEffect, useState, useRef } from 'react';

function Attendance(props) {
  const [attenDances, setDailySalaries] = useState([]);
  const [employeeData, setEmployeeData] = useState([]);
  const [expandedRows, setExpandedRows] = useState([]);
  const [departmentNames, setDepartmentNames] = useState([]);
  const [jobTitles, setJobTitles] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));

  function formatTimeSpan(timeSpan) {
    const totalSeconds = Math.floor(timeSpan / 1000);
    const hours = Math.floor(totalSeconds / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;
  
    const formattedTime = `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`;
    return formattedTime;
  }

  //  Get info of Daily Salaries
  useEffect(() => {
    fetch('https://localhost:7220/api/Timesheet/get/timesheets', {
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
      .then(attendances => {
        setDailySalaries(attendances)
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

  useEffect(() => {
    fetch('https://gearheadhrmsdb.azurewebsites.net/api/Employee/employees', {
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
    fetch('https://gearheadhrmsdb.azurewebsites.net/api/Department/departments', {
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

    fetch('https://gearheadhrmsdb.azurewebsites.net/api/Job/jobs', {
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
                            <th>Day</th>
                            <th>Time In</th>
                            <th>Time Out</th>
                            {/* <th>Late Hours</th>
                            <th>Early Leave Hours</th> */}
                            <th>Total Hours</th>
                          </tr>
                        </thead>
                        <tbody>
                          {attenDances
                            .filter((attendanceList) => attendanceList.employeeId === employee.employeeId)
                            .map((attendanceList) => (
                              <tr key={attendanceList.dailysalaryId}>
                                <td>{attendanceList.day}</td>
                                <td>{formatTimeSpan(Date.parse(`1970-01-01T${attendanceList.timeIn}Z`))}</td>
                                <td>{formatTimeSpan(Date.parse(`1970-01-01T${attendanceList.timeOut}Z`))}</td>
                                {/* <td style={{ color: 'red', fontWeight: '500' }}>{formatTimeSpan(Date.parse(`1970-01-01T${attendanceList.lateHours}Z`))}</td>
                                <td style={{ color: 'green', fontWeight: '500' }}>{formatTimeSpan(Date.parse(`1970-01-01T${attendanceList.earlyLeaveHours}Z`))}</td> */}
                                <td>{formatTimeSpan(Date.parse(`1970-01-01T${attendanceList.totalWorkHours}Z`))}</td>
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

export default Attendance;