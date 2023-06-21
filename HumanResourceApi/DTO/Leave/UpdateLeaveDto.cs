namespace HumanResourceApi.DTO.Leave
{
    public class UpdateLeaveDto
    {
        public string EmployeeId { get; set; }
        public string LeaveType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public double? LeaveHours { get; set; }
    }
}
