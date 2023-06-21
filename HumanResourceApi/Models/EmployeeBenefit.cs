using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class EmployeeBenefit
    {
        public EmployeeBenefit()
        {
            PaySlips = new HashSet<PaySlip>();
        }

        public string EmployeeId { get; set; }
        public string AllowanceId { get; set; }
        public string AllowancesId { get; set; }
        public string Status { get; set; }

        public virtual Allowance Allowance { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<PaySlip> PaySlips { get; set; }
    }
}
