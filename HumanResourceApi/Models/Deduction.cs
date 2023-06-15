using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Deduction
    {
        public int DeductionId { get; set; }
        public string DeductionType { get; set; }
        public decimal? Amount { get; set; }
    }
}
