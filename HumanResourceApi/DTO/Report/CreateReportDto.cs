using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Report
{
    public class CreateReportDto
    {
        [Required]
        public string ReportId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
