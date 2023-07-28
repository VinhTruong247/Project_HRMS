import React from "react";
import { useState, useEffect } from "react";

function ReportCounter() {
    const [data, setData] = useState([]);
    const token = JSON.parse(localStorage.getItem('jwtToken'));

    useEffect(() => {
      fetch('https://localhost:7220/api/Report/get/reports', {
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

    const countTotalReport = data.length;

    const countActiveReport = data.filter(report => report.status).length;
    const countInactiveReport = data.filter(report => !report.status).length;

    return (
      <div className="row">
        <div className="col-4">
          <h2>{countTotalReport}</h2>
          <p className="text-secondary mb-1">Total</p>
        </div>

        <div className="col-4">
          <h2>{countActiveReport}</h2>
          <p className="text-secondary mb-1">Approved</p>
        </div>

        <div className="col-4">
          <h2>{countInactiveReport}</h2>
          <p className="text-secondary mb-1">Declined</p>
        </div>
      </div>
    );
  }
  export default ReportCounter;
