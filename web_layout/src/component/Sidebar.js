import '../css/style.css';
import { NavLink, Outlet } from 'react-router-dom';
import React from 'react';

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

          <NavLink to="/calendar" className="nav_link">
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

          <NavLink to="/login" className="nav_link">
            <span className="material-icons-outlined">logout</span>
            <h3>Logout</h3>
          </NavLink>
        </div>

        {/* <div className="sidebar">
          <a href="index.html" className="nav_link">
            <span className="material-icons-outlined">dashboard</span>
            <h3>Dashboard</h3>
          </a>
          <a href="profile.html" className="nav_link">
            <span className="material-icons-outlined">person_outline</span>
            <h3>Profile</h3>
          </a>
          <a href="project.html" className="nav_link">
            <span className="material-icons-outlined">account_tree</span>
            <h3>Project</h3>
          </a>
          <a href="manage.html" className="nav_link">
            <span className="material-icons-outlined">view_list</span>
            <h3>Manage</h3>
          </a>
          <a href="statistics.html" className="nav_link">
            <span className="material-icons-outlined">bar_chart</span>
            <h3>Statistics</h3>
          </a>
          <a href="calendar.html" className="nav_link">
            <span className="material-icons-outlined">insert_invitation</span>
            <h3>Calendar</h3>
          </a>
          <a href="groups.html" className="nav_link">
            <span className="material-icons-outlined">groups</span>
            <h3>Groups</h3>
          </a>
          <a href="payment.html" className="nav_link">
            <span className="material-icons-outlined">currency_exchange</span>
            <h3>Payment</h3>
          </a>
          <a href="login.html">
            <span className="material-icons-outlined">logout</span>
            <h3>Logout</h3>
          </a>
        </div> */}
      </aside>
      <Outlet />
    </div>
  );
}

export default Sidebar;