import React from 'react';
import Counter from '../component/Counter';
import TotalEmployee from './Dashboard/TotalEmployee';
import Status from './Dashboard/Status';
import OngoingStatics from './Dashboard/OngoingStatics';
import TotalActivity from './Dashboard/TotalActivity';
import Overall from './Dashboard/Overall';
import WorkComplete from './Dashboard/WorkComplete';
import WideCalendar from './Dashboard/WideCalendar';



function Dasboard() {
    return (
        <div className="news">
            <div className="row">
                <TotalEmployee />
                <Status />
                <OngoingStatics />
            </div>
            <div className="row">
                <Overall/>
                <WorkComplete/>
                <TotalActivity/>
            </div>
            <div className="row">
                <WideCalendar/>

            </div>
        </div>
    );
}

export default Dasboard;
