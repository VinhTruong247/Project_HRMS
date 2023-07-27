import React from 'react';

import { BrowserRouter as Router, Route, Link, Outlet, NavLink } from 'react-router-dom';
import DataProvider from '../contexts/DataContext';
function Manage() {
  return (

    <div className="managepage">
        <div className="navbar-brand">Manage Page</div>
       <nav className='mininav'>
       <NavLink to="employee">Employee </NavLink>
        <NavLink to="department">Department </NavLink>
        <NavLink to="jobs">Jobs </NavLink>
        <NavLink to="report">Report </NavLink>
        <NavLink to="overtime">Overtime </NavLink>
        <NavLink to="allowance">Allowances </NavLink>
       </nav>
       <DataProvider><Outlet /></DataProvider>
    </div>
  );
}

export default Manage;
