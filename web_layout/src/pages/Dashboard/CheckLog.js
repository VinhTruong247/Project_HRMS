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
        colors: ['#84E0BE', '#9A3434'],
    };

    return (
        <div className="check col-lg-4">
            <div className="col-lg-12">
                <div className="checklog">

                    <div className='row'>
                        <h2>Attendance</h2>
                    </div>

                    <div className='row'>
                        <div>
<<<<<<< Updated upstream
                            <Chart options={options} series={data.series} type="donut" />
=======
                            <Chart options={options} series={dataPie.series} type="donut" style={{ maxHeight: '300px' }} />
>>>>>>> Stashed changes
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
