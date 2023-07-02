namespace HumanResourceApi.DTO.Deduction
{
    public class DeductionDto
    {
        public string DeductionId { get; set; }
        public string DeductionType { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }
}
