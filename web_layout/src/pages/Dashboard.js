import React from 'react';
import TotalEmployee from './Dashboard/TotalEmployee';
import Status from './Dashboard/Status';
import OngoingStatics from './Dashboard/OngoingStatics';


function Dasboard() {
    return (
        <div className="news">
            <div className="row">
                <TotalEmployee />
                <Status />
                <OngoingStatics />
            </div>
        </div>
    );
}

export default Dasboard;
