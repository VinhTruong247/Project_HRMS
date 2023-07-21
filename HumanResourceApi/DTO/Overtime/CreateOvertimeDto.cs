using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Overtime
{
    public class CreateOvertimeDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public DateTime Day { get; set; }
        [Required]
        public TimeSpan? OvertimeHours { get; set; }
    }
}
