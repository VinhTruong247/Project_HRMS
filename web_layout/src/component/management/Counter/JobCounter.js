import React from "react";
import { useState, useEffect } from "react";

function JobCounter() {
  const [data, setData] = useState([]);
  const token = JSON.parse(localStorage.getItem('jwtToken'));

  useEffect(() => {
    fetch('https://localhost:7220/api/Job/jobs', {
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

  const countTotalJob = data.length;

  const countActiveJob = data.filter(job => job.status).length;
  const countInactiveJob = data.filter(job => !job.status).length;

  return (
    <div className="row">
      <div className="col-4">
        <h2>{countTotalJob}</h2>
        <p className="text-secondary mb-1">Total</p>
      </div>

      <div className="col-4">
        <h2>{countActiveJob}</h2>
        <p className="text-secondary mb-1">Active</p>
      </div>

      <div className="col-4">
        <h2>{countInactiveJob}</h2>
        <p className="text-secondary mb-1">Inactive</p>
      </div>
    </div>
  );
}
export default JobCounter;
