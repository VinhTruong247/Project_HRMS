namespace HumanResourceApi.DTO.Allowance
{
    public class AllowanceDto
    {
        public string AllowanceId { get; set; }
        public string AllowanceType { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }
}
