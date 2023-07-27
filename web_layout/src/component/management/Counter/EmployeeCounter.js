import React from "react";
import { useState, useEffect } from "react";

function EmployeeCounter() {
  const [data, setData] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));

  useEffect(() => {
    fetch('https://gearheadhrmsdb.azurewebsites.net/api/Employee/employees', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token.token}`
      }
    })
      .then(response => response.json())
      .then(data => setData(data))
      .catch(error => console.error(error));
  }, []);

  const countTotalEmployee = data.length;

  const countActiveEmployee = data.filter(employee => employee.status).length;
  const countInactiveEmployee = data.filter(employee => !employee.status).length;

  return (
    <div className="row">
      <div className="col-4">
        <h2>{countTotalEmployee}</h2>
        <p className="text-secondary mb-1">Total</p>
      </div>

      <div className="col-4">
        <h2>{countActiveEmployee}</h2>
        <p className="text-secondary mb-1">Active</p>
      </div>

      <div className="col-4">
        <h2>{countInactiveEmployee}</h2>
        <p className="text-secondary mb-1">Inactive</p>
      </div>
    </div>
  );
}
export default EmployeeCounter;