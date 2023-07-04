using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.DeductionSummary
{
    public class DeductionSumaryDto
    {
        [Required]
        public string DeductionId { get; set; }
        [Required]
        public string PayslipId { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
