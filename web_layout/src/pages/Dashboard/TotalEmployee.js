import React from 'react'
import Counter from '../../component/Counter';

export default function TotalEmployee() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="employee">
                    <h3>Total Employee</h3>
                    <h5>Base on active list</h5>
                </div>
                <hr />
                <div className="col-12">
                    <h2><Counter /></h2>
                    <p className="text-secondary mb-1">Total</p>
                </div>
            </div>
        </div>
    );
}
