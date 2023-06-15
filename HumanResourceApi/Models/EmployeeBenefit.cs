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

        public int? EmployeeId { get; set; }
        public int? AllowanceId { get; set; }
        public int AllowancesId { get; set; }

        public virtual Allowance Allowance { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<PaySlip> PaySlips { get; set; }
    }
}
