using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Deduction
{
    public class UpdateDeductionDto
    {
        [Required]
        public string DeductionType { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
