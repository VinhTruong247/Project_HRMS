using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.EmployeeBenefit
{
    public class ResponseEmployeeBenefitDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string AllowanceId { get; set; }
        [Required]
        public string EmployeebenefitId { get; set; }
        [Required]
        public bool? Status { get; set; }
    }
}
