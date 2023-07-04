using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Report
    {
        public string ReportId { get; set; }
        public string EmployeeId { get; set; }
        public string Reason { get; set; }
        public string Content { get; set; }
        public DateTime? IssueDate { get; set; }
        public bool? Status { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
