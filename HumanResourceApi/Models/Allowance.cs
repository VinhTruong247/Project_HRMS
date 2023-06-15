using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Allowance
    {
        public Allowance()
        {
            EmployeeBenefits = new HashSet<EmployeeBenefit>();
            Jobs = new HashSet<Job>();
        }

        public int AllowanceId { get; set; }
        public string AllowanceType { get; set; }
        public decimal? Amount { get; set; }

        public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
