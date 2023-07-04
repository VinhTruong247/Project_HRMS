using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Allowance
{
    public class UpdateAllowanceDto
    {
        [Required]
        public string AllowanceType { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}