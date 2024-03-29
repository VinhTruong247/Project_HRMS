﻿namespace HumanResourceApi.DTO.Allowance
{
    public class ResponseAllowanceDto
    {
        public string AllowanceId { get; set; }
        public string AllowanceName { get; set; }
        public string AllowanceType { get; set; }
        public decimal Amount { get; set; }
        public bool? Status { get; set; }
    }
}
