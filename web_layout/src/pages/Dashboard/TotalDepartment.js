import React from 'react'
import DepartmentCounter from '../../component/management/Counter/DepartmentCounter';

export default function TotalDepartment() {
    return (
        <div className="col-lg-4">
            <div className="col-lg-12">
                <div className="department">
                    <div className='row'>
                        <div className="col-lg-6">
                            <h3>Total Department</h3>
                            <h5>Base on company list</h5>
                        </div>
                        <div className="col-lg-6"><a href="manage/department"><button>View More</button></a></div>

                    </div>
                </div>
                <hr />
                <div className="col-12">
                    <h4><DepartmentCounter /></h4>
                </div>
            </div>
        </div>
    );
}
