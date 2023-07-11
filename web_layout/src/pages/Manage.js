import React from 'react';

import { BrowserRouter as Router, Route, Link, Outlet, NavLink } from 'react-router-dom';
import DataProvider from '../contexts/DataContext';
function Manage() {
  return (
    <div className="managepage">
        <div className="navbar-brand">Manage Page</div>
       <nav className='mininav'>
        <NavLink to="employee">Employee Management</NavLink>
        <NavLink to="department">Department Management</NavLink>
        <NavLink to="jobs">Jobs Management</NavLink>
        <NavLink to="report">Report Management</NavLink>
        <NavLink to="allowance">Allowances Management</NavLink>
       </nav>
       <DataProvider><Outlet /></DataProvider>
    </div>
  );
}

export default Manage;
