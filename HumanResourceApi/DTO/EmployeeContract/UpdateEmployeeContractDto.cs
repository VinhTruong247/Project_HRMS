using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.EmployeeContract
{
    public class UpdateEmployeeContractDto
    {
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
        public string Status { get; set; }
        [Required]
        public double? PercentDeduction { get; set; }
        [Required]
        public string SalaryType { get; set; }
        [Required]
        public string ContractType { get; set; }
    }
}
