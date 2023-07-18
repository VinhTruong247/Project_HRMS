using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class DailySalary
    {
        public string DailysalaryId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? TotalHours { get; set; }
        public decimal? SalaryPerHour { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? OtHours { get; set; }
        public string OtType { get; set; }
        public decimal? OtSalary { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
