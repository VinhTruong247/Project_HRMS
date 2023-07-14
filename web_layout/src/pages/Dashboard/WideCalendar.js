import React from 'react'
import { ActivityCalendar } from 'activity-calendar-react'
export default function WideCalendar() {
    const colorCustomization = {
        activity0: '#dadada',
        activity1: '#cfb4ab',
        activity2: '#9A3434;',
        activity3: '#E20F0F',
        activity4: '#301210',
      }
      const sampleData = [
        {
          day: "2023-01-01",
          activity: 5
        },
        {
          day: "2023-01-02",
          activity: 1
        }
      ]
    return (
        <div className="col-lg-8">

                <div className="static">
                <div className='calendar'>
                    <h3>Calendar</h3>
                    <ActivityCalendar sampleData={sampleData} colorCustomization={colorCustomization} showMonth={true} />
                </div></div>
                <hr />

        </div>
    );
}
