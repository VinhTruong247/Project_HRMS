using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class DetailTaxIncome
    {
        public string DetailTaxIncomeId { get; set; }
        public double? MucChiuThue { get; set; }
        public double? ThueSuat { get; set; }
        public string Status { get; set; }
    }
}
