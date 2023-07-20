import React from 'react'
import EmployeeCounter from '../../component/management/Counter/EmployeeCounter';


export default function TotalEmployee() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="employee">
<<<<<<< Updated upstream
                    <h3>Total Employee</h3>
                    <h5>Base on company list</h5>
=======
                    <div className='row'>
                        <div className="col-lg-6">
                            <h3>Total Employee</h3>
                            <h5>Base on company list</h5>
                        </div>
                        <div className="col-lg-6"><a href="manage/employee"><button>View More</button></a></div>
                    </div>
>>>>>>> Stashed changes
                </div>
                <hr />
                <div className="col-12">

                    <h4><EmployeeCounter /></h4>

                </div>
            </div>
        </div>
    );
}
