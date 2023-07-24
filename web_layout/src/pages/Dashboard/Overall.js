import React from 'react'
import moment from 'moment';
import Year from '../Year';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import 'react-calendar/dist/Calendar.css';
import EmployeeCounter from '../../component/management/Counter/EmployeeCounter';
import DepartmentCounter from '../../component/management/Counter/DepartmentCounter';
import JobCounter from '../../component/management/Counter/JobCounter';
export default function Overall() {
  return (
    <div className="col-lg-8">
        <div className="col-12">
          <div className="static">
            <h3>Overall</h3>
            <hr />
            <p messages={{ year: 'Year' }}> </p>
            <h5>Departments</h5>
            <DepartmentCounter />
            <hr />
            <h5>Employess</h5>
            <EmployeeCounter />
            <hr />
            <h5>Jobs</h5>
            <JobCounter />

          </div>

        </div>
      </div>
  );
}
