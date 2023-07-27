using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Overtime
{
    public class OvertimeDto
    {
        [Required]
        public string OvertimeId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string OvertimeType { get; set; }
        [Required]
        public DateTime Day { get; set; }
        [Required]
        public TimeSpan? OvertimeHours { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public bool? IsDeleted { get; set; }
    }
}
