import React from "react";
import { useState, useEffect } from "react";

function DepartmentCounter() {
  const [data, setData] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));

  useEffect(() => {
    fetch('https://gearheadhrmsdb.azurewebsites.net/api/Department/departments', {
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

  const countTotalDepartment = data.length;

  const countActiveDepartment = data.filter(department => department.status).length;
  const countInactiveDepartment = data.filter(department => !department.status).length;

  return (
    <div className="row">
      <div className="col-4">
        <h2>{countTotalDepartment}</h2>
        <p className="text-secondary mb-1">Total</p>
      </div>

      <div className="col-4">
        <h2>{countActiveDepartment}</h2>
        <p className="text-secondary mb-1">Active</p>
      </div>

      <div className="col-4">
        <h2>{countInactiveDepartment}</h2>
        <p className="text-secondary mb-1">Inactive</p>
      </div>
    </div>
  );
}
export default DepartmentCounter;