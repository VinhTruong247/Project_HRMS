import React from 'react'
import moment from 'moment';
import Year from '../Year';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import 'react-calendar/dist/Calendar.css';
export default function Overall() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="static">
                    <h3>Overall</h3>
                    <p messages={{ year: 'Year' }}> </p>
                </div>
                <hr />
            </div>
        </div>
    );
}
