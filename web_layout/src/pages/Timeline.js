import React from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import Year from './Year';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import 'react-calendar/dist/Calendar.css';
import '../css/style.css';
import Status from './Dashboard/Status';
import TotalLeaves from './Timeline/TotalLeaves';
import DepartmentCounter from '../component/management/Counter/DepartmentCounter';
import TotalEmployee from './Dashboard/TotalEmployee';


const localizer = momentLocalizer(moment);
localizer.formats.yearHeaderFormat = 'YYYY';
const Timeline = () => {
  return (
    <div className='overflowy'>
      <h1>
        TimeLine page
      </h1>
      <div className="news">
      <div className="row">
                <TotalLeaves />
                <Status />
                <TotalEmployee />
            </div>
        </div>

      <Calendar
        localizer={localizer}
        events={[]}
        toolbar={true}
        views={{
          day: true,
          week: true,
          month: true,
          year: Year
        }}
        messages={{ year: 'Year' }}
      />
    </div>
  );
};
export default Timeline;
