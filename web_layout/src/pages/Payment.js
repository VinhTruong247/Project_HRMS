import React from 'react';

function Payment() {
    /**
     * Calculates the payment for an HR employee based on the hours worked and hourly rate.
     *
     * @param {number} hoursWorked - The number of hours worked by the employee.
     * @param {number} hourlyRate - The hourly rate of the employee.
     * @returns {number} The total payment for the employee.
     */
  
    const calculateEmployeePayment = (hoursWorked, hourlyRate) => {
      try {
        // Check if both arguments are numbers
        if (typeof hoursWorked !== 'number' || typeof hourlyRate !== 'number') {
          throw new TypeError('Both arguments must be numbers');
        }
  
        // Calculate the payment
        const payment = hoursWorked * hourlyRate;
  
        // Return the payment
        return payment;
      } catch (error) {
        // Log the error
        console.error('Error:', error.message);
        return 0;
      }
    };
  
    // Usage example
    const hoursWorked = 40;
    const hourlyRate = 10;
    const payment = calculateEmployeePayment(hoursWorked, hourlyRate);
  
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