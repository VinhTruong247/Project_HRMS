using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Report
{
    public class ReportDto
    {
        [Required]
        public string ReportId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime? IssueDate { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
