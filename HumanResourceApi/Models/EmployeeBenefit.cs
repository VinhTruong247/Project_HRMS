using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class EmployeeBenefit
    {
        public string EmployeeId { get; set; }
        public string AllowanceId { get; set; }
        public string EmployeebenefitId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Status { get; set; }

        public virtual Allowance Allowance { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
