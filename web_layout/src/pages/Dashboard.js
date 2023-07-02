import React from 'react';
import Counter from '../component/Counter';

function TotalEmployee() {
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

function Status() {
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

function OngoingStatics() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="static">
                    <h3>Statics</h3>
                    <h5>Ongoing Statics</h5>
                </div>
                <hr />
            </div>
        </div>
    );
}

function Dasboard() {
    return (
        <div className="news">
            <div className="row">
                <TotalEmployee />
                <Status />
                <OngoingStatics />
            </div>
        </div>
    );
}

export default Dasboard;