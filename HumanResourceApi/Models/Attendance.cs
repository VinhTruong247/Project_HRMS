using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Attendance
    {
        public string AttendanceId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? Day { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public TimeSpan? LateHours { get; set; }
        public TimeSpan? EarlyLeaveHours { get; set; }
        public TimeSpan? TotalHours { get; set; }
        public bool? AttendanceStatus { get; set; }
        public string Notes { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
