import React, { useState, useEffect } from 'react';
import Chart from 'react-apexcharts';
import useData from '../../hooks/useData';

function CheckLog() {
    const data = useData()
    const [totalHour, setHour] = useState([]);
    const [isCheckedIn, setIsCheckedIn] = useState(localStorage.getItem('isCheckedIn') === 'true');
    const [totalHours, setTotalHours] = useState(0);
    const token = JSON.parse(localStorage.getItem('jwtToken'));

    useEffect(() => {
        localStorage.setItem('isCheckedIn', isCheckedIn);
    }, [isCheckedIn]);

    useEffect(() => {
        const interval = setInterval(() => {
            const now = new Date();
            if (now.getHours() === 12 || now.getHours() === 17) {
                performCheckOut()
                    .then(() => {
                        setIsCheckedIn(false);
                        console.log('Automatic check-out at', now, 'successful');
                    })
                    .catch(error => {
                        console.error('Error performing automatic check-out:', error);
                    });
            }
        }, 60000); // check every minute

        return () => clearInterval(interval);
    }, []);

    useEffect(() => {
        fetch(`https://gearheadhrmsdb.azurewebsites.net/api/Timesheet/get/timesheet/${data.employeeId}`, {
            method: "GET",
            headers: {
                'Content-Type': "application/json",
                'Authorization': `Bearer ${token.token}`,
            },
        })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error("Api response was not ok.");
                }
            })
            .then((hours) => {
                setHour(hours);
            })
            .catch((error) => {
                console.error("There was a problem with the fetch operation:", error);
            });
    }, []);

    const handleCheckInOut = () => {
        const now = new Date();
        if (now.getHours() >= 8 && now.getHours() < 17) {
            if (isCheckedIn) {
                performCheckOut()
                    .then(() => {
                        setIsCheckedIn(false);
                        console.log('Checked out successfully');
                    })
                    .catch(error => {
                        console.error('Error checking out:', error);
                    });
            } else {
                performCheckIn()
                    .then(() => {
                        setIsCheckedIn(true);
                        console.log('Checked in successfully');
                    })
                    .catch(error => {
                        console.error('Error checking in:', error);
                    });
            }
        } else {
            console.log('Check-in not allowed between 5pm and 8am.');
        }
    };

    // const handleCheckInOut = () => {
    //     if (isCheckedIn) {
    //         performCheckOut()
    //             .then(() => {
    //                 setIsCheckedIn(false);
    //                 console.log('Checked out successfully');
    //             })
    //             .catch(error => {
    //                 console.error('Error checking out:', error);
    //             });
    //     } else {
    //         performCheckIn()
    //             .then(() => {
    //                 setIsCheckedIn(true);
    //                 console.log('Checked in successfully');
    //             })
    //             .catch(error => {
    //                 console.error('Error checking in:', error);
    //             });
    //     }
    // };

    const performCheckIn = () => {
        return fetch(`https://gearheadhrmsdb.azurewebsites.net/api/Attendance/create/${data.employeeId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token.token}`
            },
            body: JSON.stringify({}),
        }).then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Checkin API response was not ok.');
            }
        });
    };

    const performCheckOut = () => {
        return fetch(`https://gearheadhrmsdb.azurewebsites.net/api/Attendance/update/attendance/${data.employeeId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token.token}`
            },
            body: JSON.stringify({}),
        }).then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Checkout API response was not ok.');
            }
        });
    };


    const dataPie = {
        labels: ['Hours Worked', 'Remaining Hours'],
        series: [8, 3],
    };

    const options = {
        labels: ['Hours Worked', 'Remaining Hours'],
        colors: ['#84E0BE', '#9A3434'],
    };

    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="checklog">
                    <div className="row">
                        <h2>Attendance</h2>
                    </div>

                    <div className="row">
                        <div>
                            <Chart options={options} series={dataPie.series} type="donut" />
                        </div>
                    </div>

                    <div className="row">
                        <button onClick={handleCheckInOut}>
                            {isCheckedIn ? 'Check Out' : 'Check In'}
                        </button>
                    </div>

                    <div className="row" style={{ marginTop: '1.5rem' }}>
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