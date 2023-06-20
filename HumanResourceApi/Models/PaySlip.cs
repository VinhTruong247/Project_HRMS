using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class PaySlip
    {
        public string PayslipId { get; set; }
        public string EmployeeId { get; set; }
        public string PayPeriod { get; set; }
        public DateTime? PaidDate { get; set; }
        public double? BaseSalary { get; set; }
        public double? OtHours { get; set; }
        public string AllowancesId { get; set; }
        public string ContractId { get; set; }
        public double? StarndardWorkHours { get; set; }
        public double? ActualWorkHours { get; set; }
        public double? TaxIncome { get; set; }
        public double? Bonus { get; set; }
        public double? DeductionSum { get; set; }
        public double? TotalSalary { get; set; }
        public string Note { get; set; }
        public int? BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string Approval { get; set; }
        public string Status { get; set; }

        public virtual EmployeeBenefit Allowances { get; set; }
        public virtual EmployeeContract Contract { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
