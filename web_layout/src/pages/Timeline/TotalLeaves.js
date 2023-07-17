import React from 'react'
import LeaveCounter from '../../component/LeavesCounter';


export default function TotalLeave() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="leave">
                    <div className='row'>
                        <div className="col-lg-6">
                            <h3>Total Leave</h3>
                            <h5>Base on active list</h5>
                        </div>
                        <div className="col-lg-6"><button>View More</button></div>
                    </div>
                </div>
                <hr />
                <div className="col-12">
                    <h4><LeaveCounter /></h4>
                </div>
            </div>
        </div>
    );
}
