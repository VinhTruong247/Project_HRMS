using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.EmployeeBenefit
{
    public class UpdateEmployeeBenefitDto
    {
        [Required]
        public string AllowanceId { get; set; }
        [Required]
        public bool? Status { get; set; }
    }
}
