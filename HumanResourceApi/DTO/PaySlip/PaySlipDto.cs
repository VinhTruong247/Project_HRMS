using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.PaySlip
{
    public class PaySlipDto
    {
        public string PayslipId { get; set; }
        public string EmployeeId { get; set; }
        public string PayPeriod { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? BaseSalary { get; set; }
        public TimeSpan? OtHours { get; set; }
        public string ContractId { get; set; }
        public TimeSpan? StandardWorkHours { get; set; }
        public TimeSpan? ActualWorkHours { get; set; }
        public decimal TaxIncome { get; set; }
        public decimal Tax { get; set; }
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
