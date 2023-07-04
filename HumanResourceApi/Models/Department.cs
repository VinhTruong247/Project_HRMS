using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            Projects = new HashSet<Project>();
        }

        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
