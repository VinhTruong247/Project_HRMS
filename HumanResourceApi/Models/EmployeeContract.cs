using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class EmployeeContract
    {
        public EmployeeContract()
        {
            PaySlips = new HashSet<PaySlip>();
        }

        public string ContractId { get; set; }
        public string EmployeeId { get; set; }
        public string ContractFile { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Job { get; set; }
        public decimal? BaseSalary { get; set; }
        public string Status { get; set; }
        public double? PercentDeduction { get; set; }
        public string SalaryType { get; set; }
        public string ContractType { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<PaySlip> PaySlips { get; set; }
    }
}
