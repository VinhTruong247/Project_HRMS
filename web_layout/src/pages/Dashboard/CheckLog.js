import React, { useState } from 'react';
import Chart from 'react-apexcharts';

function CheckLog() {
    const [isCheckedIn, setIsCheckedIn] = useState(false);
    const [totalHours, setTotalHours] = useState(0);

    const handleCheckInOut = () => {
        setIsCheckedIn(prevCheckedIn => !prevCheckedIn);
    }

    const data = {
        labels: ['Hours Worked', 'Remaining Hours'],
        series: [5, 3],
    };

    const options = {
        labels: ['Hours Worked', 'Remaining Hours'],
        colors: ['#13C044', '#9A3434'],
    };

    return (
        <div className="col-lg-4" style={{ height: '430px' }}>
            <div className="col-lg-12">
                <div className="checklog">

                    <div className='row'>
                        <h2>Attendance</h2>
                    </div>

                    <div className='row'>
                        <div>
                            <Chart options={options} series={data.series} type="donut" />
                        </div>
                    </div>

                    <div className='row'>
                        <button onClick={handleCheckInOut}>
                            {isCheckedIn ? 'Check Out' : 'Check In'}
                        </button>
                    </div>

                    <div className='row' style={{ marginTop: '1.5rem' }}>
                        <p className={isCheckedIn ? 'checked-in' : 'checked-out'}>
                            You are currently {isCheckedIn ? 'checked in' : 'checked out'}.
                        </p>
                    </div>

                </div>
            </div>
        </div>
    );
}

export default CheckLog;
