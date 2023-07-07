using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.EmployeeBenefit
{
    public class EmployeeBenefitDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string AllowanceId { get; set; }
        [Required]
        public string AllowancesId { get; set; }
        [Required]
        public bool? Status { get; set; }
    }
}
