using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Attendance
{
    public class UpdateAttendanceDto
    {
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
        public string Notes { get; set; }
    }
}
