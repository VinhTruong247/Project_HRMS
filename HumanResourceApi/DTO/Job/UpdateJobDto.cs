namespace HumanResourceApi.DTO.Job
{
    public class UpdateJobDto
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public string Status { get; set; }
        public decimal? BaseSalaryPerHour { get; set; }
        public string AllowanceId { get; set; }
        public decimal? Bonus { get; set; }
    }
}
