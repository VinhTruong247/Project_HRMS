using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Experience
    {
        public Experience()
        {
            Employees = new HashSet<Employee>();
        }

        public int ExperienceId { get; set; }
        public string NameProject { get; set; }
        public int? TeamSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TechStack { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
