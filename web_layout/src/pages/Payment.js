import React from 'react';
import { useEffect, useState, useRef } from 'react';
import Payslip from './Payment/Payslip';
import { BrowserRouter as Router, Route, Link, Outlet, NavLink } from 'react-router-dom';
import DataProvider from '../contexts/DataContext';


function Payment() {
  
    return (
      <div className='payment_page'>
        <div className="navbar-brand">Payment</div>
        <nav className='mininav'>
        <NavLink to="payslip">PaySlip Management</NavLink>
        <NavLink to="dailysalarydetail">Daily Salary Detail</NavLink>
       </nav>
        <div className='row'>

      </div>
      <DataProvider><Outlet /></DataProvider>
      </div>
    );
  }
 
export default Payment;