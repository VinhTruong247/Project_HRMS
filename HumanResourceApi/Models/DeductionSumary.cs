using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class DeductionSumary
    {
        public int? DeductionId { get; set; }
        public int? PayslipId { get; set; }
        public decimal? Amount { get; set; }

        public virtual Deduction Deduction { get; set; }
        public virtual PaySlip Payslip { get; set; }
    }
}
