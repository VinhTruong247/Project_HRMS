namespace HumanResourceApi.DTO.DeductionSummary
{
    public class DeductionSumaryDto
    {
        public string DeductionId { get; set; }
        public string PayslipId { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }
}
