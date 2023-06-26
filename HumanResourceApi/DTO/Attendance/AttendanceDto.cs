namespace HumanResourceApi.DTO.Attendance
{
    public class AttendanceDto
    {
        public string EmployeeId { get; set; }
        public DateTime? Day { get; set; }
        public double? TimeIn { get; set; }
        public double? TimeOut { get; set; }
        public double? LateHours { get; set; }
        public double? EarlyLeaveHours { get; set; }
        public double? TotalHours { get; set; }
        public string AttendanceStatus { get; set; }
        public string Notes { get; set; }
    }
}
