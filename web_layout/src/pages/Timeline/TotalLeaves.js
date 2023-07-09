import React from 'react'
import LeavesCounter from '../../component/LeavesCounter'
export default function TotalLeaves() {
  return (
    <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="employee">
                    <h3>Total Leaves</h3>
                    <h5>Base on active list</h5>
                </div>
                <hr />
                <div className="col-12">
                    <h2><LeavesCounter /></h2>
                    <p className="text-secondary mb-1">Total</p>
                </div>
            </div>
        </div>

  )
}
