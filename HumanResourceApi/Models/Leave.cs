using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Leave
    {
        public string LeaveId { get; set; }
        public string EmployeeId { get; set; }
        public string LeaveType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public bool? Status { get; set; }
        public double? LeaveHours { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
