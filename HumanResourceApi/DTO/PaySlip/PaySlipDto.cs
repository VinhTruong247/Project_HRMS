using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.PaySlip
{
    public class PaySlipDto
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
        public decimal TaxIncome { get; set; }
        public decimal Tax { get; set; }
        public decimal Allowance { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? TotalSalary { get; set; }
        public string Note { get; set; }
        public int? BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string Approval { get; set; }
        public string Status { get; set; }
    }
}
