using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.PaySlip
{
    public class PaySlipDto
    {
        [Required]
        public string PayslipId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string PayPeriod { get; set; }
        [Required]
        public DateTime? PaidDate { get; set; }
        [Required]
        public double? BaseSalary { get; set; }
        [Required]
        public TimeSpan? OtHours { get; set; }
        [Required]
        public string AllowancesId { get; set; }
        [Required]
        public string ContractId { get; set; }
        [Required]
        public double? StarndardWorkHours { get; set; }
        [Required]
        public double? ActualWorkHours { get; set; }
        [Required]
        public double? TaxIncome { get; set; }
        [Required]
        public double? Bonus { get; set; }
        [Required]
        public double? DeductionSum { get; set; }
        [Required]
        public double? TotalSalary { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public int? BankAccountNumber { get; set; }
        [Required]
        public string BankAccountName { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string Approval { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
