namespace HumanResourceApi.DTO.Allowance
{
    public class UpdateAllowanceDto
    {
        public string AllowanceType { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }
}