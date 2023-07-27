import React from "react";
import { useState, useEffect } from "react";

function LeaveCounter() {
  const [data, setData] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));

  useEffect(() => {
    fetch('https://gearheadhrmsdb.azurewebsites.net/api/Leave/leaves', {
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

  const countTotalLeave = data.length;

  const countActiveLeave = data.filter(leave => leave.status).length;
  const countInactiveLeave = data.filter(leave => !leave.status).length;

  return (
    <div className="row">
      <div className="col-4">
        <h2>{countTotalLeave}</h2>
        <p className="text-secondary mb-1">Total</p>
      </div>

      <div className="col-4">
        <h2>{countActiveLeave}</h2>
        <p className="text-secondary mb-1">Approved</p>
      </div>

      <div className="col-4">
        <h2>{countInactiveLeave}</h2>
        <p className="text-secondary mb-1">Declined</p>
      </div>
    </div>
  );
}
export default LeaveCounter;
