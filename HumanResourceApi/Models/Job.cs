using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Job
    {
        public Job()
        {
            EmployeeContracts = new HashSet<EmployeeContract>();
            Employees = new HashSet<Employee>();
        }

        public string JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public bool? Status { get; set; }
        public decimal? BaseSalaryPerHour { get; set; }
        public decimal? Bonus { get; set; }

        public virtual ICollection<EmployeeContract> EmployeeContracts { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
