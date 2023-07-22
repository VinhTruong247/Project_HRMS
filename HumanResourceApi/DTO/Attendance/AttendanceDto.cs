using Microsoft.EntityFrameworkCore.Query.Internal;
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
        public TimeSpan? TimeIn { get; set; }
        [Required]
        public TimeSpan? TimeOut { get; set; }
        [Required]
        public TimeSpan? LateHours { get; set; }
        [Required]
        public TimeSpan? EarlyLeaveHours { get; set; }
        [Required]
        public TimeSpan? TotalHours { get; set; } 
        [Required]
        public bool? AttendanceStatus { get; set; }
        [Required]
        public string Notes { get; set; }
    }
}
