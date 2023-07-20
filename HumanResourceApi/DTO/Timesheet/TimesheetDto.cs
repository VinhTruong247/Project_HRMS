using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Timesheet
{
    public class TimesheetDto
    {
        [Required]
        public string TimesheetId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public TimeSpan? TimeIn { get; set; }
        [Required]
        public TimeSpan? TimeOut { get; set; }
        [Required]
        public DateTime? Day { get; set; }
        [Required]
        public bool? Status { get; set; }
        [Required]
        public TimeSpan? TotalWorkHours { get; set; }
    }
}
