using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Deduction
{
    public class DeductionDto
    {
        [Required]
        public string DeductionId { get; set; }
        [Required]
        public string DeductionType { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
