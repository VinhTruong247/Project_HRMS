using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class PaySlip
    {
        public string PayslipId { get; set; }
        public string EmployeeId { get; set; }
        public string PayPeriod { get; set; }
        public DateTime PaidDate { get; set; }
        public decimal? BaseSalary { get; set; }
        public decimal? OtHours { get; set; }
        public string ContractId { get; set; }
        public decimal? StandardWorkHours { get; set; }
        public decimal? ActualWorkHours { get; set; }
        public decimal? TaxIncome { get; set; }
        public decimal? TotalSalary { get; set; }
        public string Note { get; set; }
        public int? BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string Status { get; set; }

        public virtual EmployeeContract Contract { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
