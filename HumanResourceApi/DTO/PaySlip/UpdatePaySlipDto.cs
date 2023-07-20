using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.PaySlip
{
    public class UpdatePaySlipDto
    {
        public string PayPeriod { get; set; }
        public DateTime PaidDate { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
