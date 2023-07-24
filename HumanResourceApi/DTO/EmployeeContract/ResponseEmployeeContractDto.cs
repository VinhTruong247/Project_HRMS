using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.EmployeeContract
{
    public class ResponseEmployeeContractDto
    {
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string ContractFile { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string Job { get; set; }
        [Required]
        public decimal? BaseSalary { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public double? PercentDeduction { get; set; }
        [Required]
        public string SalaryType { get; set; }
        [Required]
        public string ContractType { get; set; }
    }
}
