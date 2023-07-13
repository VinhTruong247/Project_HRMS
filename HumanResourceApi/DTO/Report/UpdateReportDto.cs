using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Report
{
    public class UpdateReportDto
    {
        [Required]
        public bool? Status { get; set; }
    }
}
