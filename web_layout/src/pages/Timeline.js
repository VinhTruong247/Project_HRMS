import React from 'react';
import Attendance from './Manage/Attendance';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import 'react-calendar/dist/Calendar.css';
import '../css/style.css';
import Status from './Dashboard/Status';
import TotalLeaves from './Timeline/TotalLeaves';
import TotalEmployee from './Dashboard/TotalEmployee';

const Timeline = () => {
  return (
    <div className='overflowy'>
      <h1>
        Timesheets page
      </h1>
      <div className="news">
        <div className="row">
          <TotalLeaves />
          <Status />
          <TotalEmployee />
        </div>
      </div>
      <Attendance />
    </div>
  );
};
export default Timeline;
