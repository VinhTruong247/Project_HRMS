using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Overtime
    {
        public int OvertimeId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? Day { get; set; }
        public decimal? OvertimeHours { get; set; }
        public string Status { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
