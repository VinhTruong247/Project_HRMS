using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Project
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string DepartmentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }

        public virtual Department Department { get; set; }
    }
}
