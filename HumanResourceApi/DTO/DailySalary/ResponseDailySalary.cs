﻿namespace HumanResourceApi.DTO.DailySalary
{
    public class ResponseDailySalary
    {
        public string DailysalaryId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TotalHours { get; set; }
        public decimal? SalaryPerHour { get; set; }
        public decimal? TotalSalary { get; set; }
        public TimeSpan OtHours { get; set; }
        public string OtType { get; set; }
        public decimal? OtSalary { get; set; }
        public decimal? DailyAllowance { get; set; }
        public decimal? DailySalary { get; set; }
        public ICollection<DailyAllowance> DailyAllowanceList { get; set; }
        
    }

    public class DailyAllowance
    {
        public string AllowanceId { get; set; }
        public string AllowanceName { get; set; }
        public string AllowanceType { get; set; }
        public decimal Amount { get; set; }
    }
}
