import React from 'react';
import { useEffect, useState, useRef } from 'react';
import Payslip from './Payment/Payslip';



function Payment() {
  
    return (
      <div className='payment_page'>
        <div className="navbar-brand">Payment Details</div>
        <div className='row'>
        <Payslip/>
      </div>
      </div>
    );
  }
 
export default Payment;