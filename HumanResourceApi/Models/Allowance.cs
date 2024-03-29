﻿using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Allowance
    {
        public Allowance()
        {
            EmployeeBenefits = new HashSet<EmployeeBenefit>();
        }

        public string AllowanceId { get; set; }
        public string AllowanceName { get; set; }
        public string AllowanceType { get; set; }
        public decimal Amount { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; }
    }
}
