using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class DepartmentMemberList
    {
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }
        public string EmpRole { get; set; }
        public bool? Status { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
