namespace HumanResourceApi.DTO.DailySalary
{
    public class ResponseDailySalary
    {
        public string DailysalaryId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? TotalHours { get; set; }
        public decimal? SalaryPerHour { get; set; }
        public decimal? BaseSalary { get; set; }
        public decimal? OtHours { get; set; }
        public string OtType { get; set; }
        public decimal? OtSalary { get; set; }
        public decimal? DailyAllowance { get; set; }
        public decimal? TotalSalary { get; set; }
    }
}
