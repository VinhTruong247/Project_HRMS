using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeBenefits = new HashSet<EmployeeBenefit>();
            EmployeeContracts = new HashSet<EmployeeContract>();
            EmployeeLoanLogs = new HashSet<EmployeeLoanLog>();
            Leaves = new HashSet<Leave>();
            Overtimes = new HashSet<Overtime>();
            PaySlips = new HashSet<PaySlip>();
            SkillEmployees = new HashSet<SkillEmployee>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeImage { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmployeeAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public int? ExperienceId { get; set; }
        public int? UserId { get; set; }
        public int? JobId { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Experience Experience { get; set; }
        public virtual Job Job { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; }
        public virtual ICollection<EmployeeContract> EmployeeContracts { get; set; }
        public virtual ICollection<EmployeeLoanLog> EmployeeLoanLogs { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
        public virtual ICollection<Overtime> Overtimes { get; set; }
        public virtual ICollection<PaySlip> PaySlips { get; set; }
        public virtual ICollection<SkillEmployee> SkillEmployees { get; set; }
    }
}
