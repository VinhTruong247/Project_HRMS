import React from 'react'
import Counter from '../../component/management/Counter/EmployeeCounter';

export default function TotalEmployee() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="employee">
                    <h3>Total Employee</h3>
                    <h5>Base on company list</h5>
                </div>
                <hr />
                <div className="col-12">
                    <h4><Counter /></h4>
                </div>
            </div>
        </div>
    );
}
