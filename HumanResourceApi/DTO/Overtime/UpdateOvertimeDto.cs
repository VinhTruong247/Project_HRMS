using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Overtime
{
    public class UpdateOvertimeDto
    {
        [Required]
        public string Status { get; set; }
        [Required]
        public bool? IsDeleted { get; set; }
    }
}
