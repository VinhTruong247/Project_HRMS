using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Attendance
    {
        public string EmployeeId { get; set; }
        public DateTime? Day { get; set; }
        public double? TimeIn { get; set; }
        public double? TimeOut { get; set; }
        public double? LateHours { get; set; }
        public double? EarlyLeaveHours { get; set; }
        public double? TotalHours { get; set; }
        public bool? AttendanceStatus { get; set; }
        public string Notes { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
