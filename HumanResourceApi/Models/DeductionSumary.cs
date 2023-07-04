using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class DeductionSumary
    {
        public string DeductionId { get; set; }
        public string PayslipId { get; set; }
        public decimal? Amount { get; set; }
        public bool? Status { get; set; }

        public virtual Deduction Deduction { get; set; }
        public virtual PaySlip Payslip { get; set; }
    }
}
