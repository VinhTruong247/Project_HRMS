using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Overtime
    {
        public string OvertimeId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan? OvertimeHours { get; set; }
        public string Status { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
