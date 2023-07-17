import React from 'react'
import EmployeeCounter from '../../component/management/Counter/EmployeeCounter';

export default function TotalEmployee() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="employee">
                    <div className='row'>
                        <div className="col-lg-6">
                            <h3>Total Employee</h3>
                            <h5>Base on company list</h5>
                        </div>
                        <div className="col-lg-6"><button>View More</button></div>
                    </div>
                </div>
                <hr />
                <div className="col-12">
                    <h4><EmployeeCounter /></h4>
                </div>
            </div>
        </div>
    );
}
