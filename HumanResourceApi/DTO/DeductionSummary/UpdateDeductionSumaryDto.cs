namespace HumanResourceApi.DTO.DeductionSummary
{
    public class UpdateDeductionSumaryDto
    {
        public string PayslipId { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }
}
