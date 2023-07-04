using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Attendance
{
    public class AttendanceDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public DateTime? Day { get; set; }
        [Required]
        public double? TimeIn { get; set; }
        [Required]
        public double? TimeOut { get; set; }
        [Required]
        public double? LateHours { get; set; }
        [Required]
        public double? EarlyLeaveHours { get; set; }
        [Required]
        public double? TotalHours { get; set; }
        [Required]
        public string AttendanceStatus { get; set; }
        [Required]
        public bool Notes { get; set; }
    }
}
