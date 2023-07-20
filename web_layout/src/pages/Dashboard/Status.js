import React from 'react'
import ReportCounter from '../../component/management/Counter/ReportCounter';

export default function Status() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="employee">
<<<<<<< Updated upstream
                    <h3>Report Status</h3>
                    <h5>Request Approval Status</h5>
=======
                    <div className='row'>
                        <div className="col-lg-6">
                            <h3>Report Status</h3>
                            <h5>Request Approval Status</h5>
                        </div>
                        <div className="col-lg-6"><a href="manage/report"><button>View More</button></a></div>
                    </div>
>>>>>>> Stashed changes
                </div>
                <hr />
                <div className="col-12">
                    <h4><ReportCounter /></h4>
                </div>
            </div>
        </div>
    );
}
