import React, { useEffect, useState } from 'react';

function Groups(props) {
  const [selectedDepartment, setSelectedDepartment] = useState([]);
  const [departmentList, setDepartmentList] = useState([]);
  const token = JSON.parse(localStorage.getItem("jwtToken"));

  useEffect(() => {
    fetch(`https://localhost:7220/api/DepartmentMember/get/DepartmentMemberList`, {
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
      .then((departments) => {
        setSelectedDepartment(departments);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
      });
  }, []);

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
        setDepartmentList(departments);
      })
      .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
      });
  }, []);

  return (
    <div className="main-body">
      <div className="inbox-header">
      </div>
      <div className="inbox-messages">
        {selectedDepartment.map(department => (
          <div key={department.departmentId} className="inbox-message">
            <div className="message-header">
              <h2>
                Department: {departmentList.find(department => department.departmentId === selectedDepartment.departmentId)
                  ? departmentList.find(department => department.departmentId === selectedDepartment.departmentId).departmentName
                  : ''}
              </h2>
            </div>
            <div className="message-body">
              <p>Employee: {selectedDepartment.map((employee) => employee.employeeId).join(', ')}</p>
              <p>
                {departmentList.find(department => department.departmentId === selectedDepartment.departmentId)
                  ? departmentList.find(department => department.departmentId === selectedDepartment.departmentId).description
                  : ''}
              </p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Groups;