import React from 'react'
import ReportCounter from '../../component/management/Counter/ReportCounter';

export default function Status() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="employee">
                    <h3>Report Status</h3>
                    <h5>Request Approval Status</h5>
                </div>
                <hr />
                <div className="col-12">
                    <h4><ReportCounter /></h4>
                </div>
            </div>
        </div>
    );
}
