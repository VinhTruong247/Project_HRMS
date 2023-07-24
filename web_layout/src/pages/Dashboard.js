import React from 'react';
import TotalEmployee from './Dashboard/TotalEmployee';
import Status from './Dashboard/Status';
import DepartmentCounter from '../component/management/Counter/DepartmentCounter';
import CheckLog from './Dashboard/CheckLog';
import Overall from './Dashboard/Overall';
import WorkComplete from './Dashboard/WorkComplete';
import WideCalendar from './Dashboard/WideCalendar';
import { BrowserRouter as Router, Route, Link, Outlet, NavLink } from 'react-router-dom';
import useData from "../hooks/useData";
import TotalDepartment from './Dashboard/TotalDepartment';


function Dasboard() {
    const data = useData()
    return (

    <div className='dashboardpage'>
        <nav className='dashboardnav'>
        <NavLink to="profile">{data.firstName} {data.lastName}</NavLink>
        </nav>

        <div className="news">
            <div className="row">
                <TotalEmployee />
                <Status />
                <TotalDepartment />
            </div>
            <div className="row">
                <Overall/>
                <WorkComplete/>
                <CheckLog/>
            </div>
            <div className="row">
                <WideCalendar/>
            </div>
        </div>
    </div>

    );
}

export default Dasboard;
