using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Report
{
    public class UpdateReportDto
    {
        [Required]
        public string Status { get; set; }
    }
}
