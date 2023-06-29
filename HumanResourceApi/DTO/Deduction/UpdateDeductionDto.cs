namespace HumanResourceApi.DTO.Deduction
{
    public class UpdateDeductionDto
    {
        public string DeductionType { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }
}
