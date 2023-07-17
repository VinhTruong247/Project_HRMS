import React from 'react';

function Payment() {
  
    return (
      <div>
        <h2>Employee Payment Calculator</h2>
        <p>Hours Worked: {hoursWorked}</p>
        <p>Hourly Rate: ${hourlyRate}</p>
        <p>Total Payment: ${payment}</p>
      </div>
    );
  }
 
export default Payment;