using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.DeductionSummary
{
    public class UpdateDeductionSumaryDto
    {
        [Required]
        public string PayslipId { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
