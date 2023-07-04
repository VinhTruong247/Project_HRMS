import React from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import Year from './Year';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import 'react-calendar/dist/Calendar.css';
import '../css/calendar.css';
const localizer = momentLocalizer(moment);
localizer.formats.yearHeaderFormat = 'YYYY';

const Timeline = () => {
  return (
    <div>
      <h1>
        GeeksforGeeks is a Computer Science portal for geeks.
      </h1>
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
