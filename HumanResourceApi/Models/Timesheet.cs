using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Timesheet
    {
        public string TimesheetId { get; set; }
        public string EmployeeId { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public DateTime? Day { get; set; }
        public bool? Status { get; set; }
        public TimeSpan? TotalWorkHours { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
