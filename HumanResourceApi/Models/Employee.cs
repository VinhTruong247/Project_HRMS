using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
            DailySalaries = new HashSet<DailySalary>();
            EmployeeBenefits = new HashSet<EmployeeBenefit>();
            EmployeeContracts = new HashSet<EmployeeContract>();
            Experiences = new HashSet<Experience>();
            Leaves = new HashSet<Leave>();
            Overtimes = new HashSet<Overtime>();
            PaySlips = new HashSet<PaySlip>();
            Reports = new HashSet<Report>();
            SkillEmployees = new HashSet<SkillEmployee>();
            Timesheets = new HashSet<Timesheet>();
            Users = new HashSet<User>();
        }

        public string EmployeeId { get; set; }
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
        public int? Dependents { get; set; }
        public string JobId { get; set; }
        public string DepartmentId { get; set; }
        public bool? Status { get; set; }

        public virtual Department Department { get; set; }
        public virtual Job Job { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<DailySalary> DailySalaries { get; set; }
        public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; }
        public virtual ICollection<EmployeeContract> EmployeeContracts { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
        public virtual ICollection<Overtime> Overtimes { get; set; }
        public virtual ICollection<PaySlip> PaySlips { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SkillEmployee> SkillEmployees { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
