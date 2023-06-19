using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class DepartmentMemberList
    {
        public int? DepartmentId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmpRole { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
