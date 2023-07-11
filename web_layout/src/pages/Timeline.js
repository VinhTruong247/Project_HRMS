import React from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import Year from './Year';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import 'react-calendar/dist/Calendar.css';
import '../css/style.css';
import Status from './Dashboard/Status';
import OngoingStatics from './Dashboard/OngoingStatics';
import TotalLeaves from './Timeline/TotalLeaves';


const localizer = momentLocalizer(moment);
localizer.formats.yearHeaderFormat = 'YYYY';
const Timeline = () => {
  return (
    <div className='timeline'>
      <h1>
        TimeLine page
      </h1>
      <div className="news">
      <div className="row">
                <TotalLeaves />
                <Status />
                <OngoingStatics />
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