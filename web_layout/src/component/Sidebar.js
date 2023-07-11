import '../css/style.css';
import { NavLink, Outlet } from 'react-router-dom';
import React from 'react';
import Protected from './Protected';
import DataProvider from '../contexts/DataContext';

function Sidebar() {
  return (
    <div className='container'>
      <aside>
        <div className="top">
          <div className="logo">
            <img src="/img/logo.png" alt="Company logo" />
            <h3>COMPANY</h3>
          </div>
          <div className="close" id="close-btn">
            <span className="material-icons-outlined">close</span>
          </div>
        </div>

        <div className="sidebar">

          <NavLink to="/" className="nav_link">
            <span className="material-icons-outlined">dashboard</span>
            <h3>Dashboard</h3>
          </NavLink>

          <NavLink to="/profile" className="nav_link">
            <span className="material-icons-outlined">person_outline</span>
            <h3>Profile</h3>
          </NavLink>

          <NavLink to="/project" className="nav_link">
            <span className="material-icons-outlined">account_tree</span>
            <h3>Project</h3>
          </NavLink>

          <NavLink to="/manage" className="nav_link">
            <span className="material-icons-outlined">view_list</span>
            <h3>Manage</h3>
          </NavLink>

          <NavLink to="/statistics" className="nav_link">
            <span className="material-icons-outlined">bar_chart</span>
            <h3>Statistics</h3>
          </NavLink>

          <NavLink to="/timeline" className="nav_link">
            <span className="material-icons-outlined">insert_invitation</span>

            <h3>Calendar</h3>

          </NavLink>

          <NavLink to="/groups" className="nav_link">
            <span className="material-icons-outlined">groups</span>
            <h3>Groups</h3>
          </NavLink>

          <NavLink to="/payment" className="nav_link">
            <span className="material-icons-outlined">currency_exchange</span>
            <h3>Payment</h3>
          </NavLink>

          <NavLink to="/login"
            onClick={() => {
              console.log('logging out...');
              localStorage.clear();
            }}
            className="nav_link"
          >
            <span className="material-icons-outlined">logout</span>
            <h3>Logout</h3>
          </NavLink>
        </div>
      </aside>
      <Protected><DataProvider><Outlet /></DataProvider></Protected>
    </div>
  );
}

export default Sidebar;