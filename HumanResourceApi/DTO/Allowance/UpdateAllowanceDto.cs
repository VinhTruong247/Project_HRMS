using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Allowance
{
    public class UpdateAllowanceDto
    {
        public string AllowanceName { get; set; }
        public string AllowanceType { get; set; }
        public decimal Amount { get; set; }
        public bool? Status { get; set; }
    }
}