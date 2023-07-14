using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Allowance
{
    public class AllowanceDto
    {
        [Required]
        public string AllowanceId { get; set; }
        [Required]
        public string AllowanceType { get; set; }
        [Required]
        public decimal? AmountPerDay { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
