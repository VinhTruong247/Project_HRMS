import React from 'react'

export default function Status() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="status">
                    <h3>Status</h3>
                    <h5>Request Approval Status</h5>
                </div>
                <hr />
                <div className="row">
                    <div className="col-lg-4">
                        <h2>35</h2>
                        <p className="text-secondary mb-1">Total</p>
                    </div>
                    <div className="col-lg-4">
                        <h2>5</h2>
                        <p className="text-secondary mb-1">Pending</p>
                    </div>
                    <div className="col-lg-4">
                        <h2>30</h2>
                        <p className="text-secondary mb-1">Approved</p>
                    </div>
                </div>
            </div>
        </div>
    );
}
