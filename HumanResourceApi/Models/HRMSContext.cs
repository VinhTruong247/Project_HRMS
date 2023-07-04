using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HumanResourceApi.Models
{
    public partial class HRMSContext : DbContext
    {
        public HRMSContext()
        {
        }

        public HRMSContext(DbContextOptions<HRMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allowance> Allowances { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Deduction> Deductions { get; set; }
        public virtual DbSet<DeductionSumary> DeductionSumaries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentMemberList> DepartmentMemberLists { get; set; }
        public virtual DbSet<DetailTaxIncome> DetailTaxIncomes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
        public virtual DbSet<EmployeeContract> EmployeeContracts { get; set; }
        public virtual DbSet<EmployeeLoanLog> EmployeeLoanLogs { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<GrantedPermission> GrantedPermissions { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<Overtime> Overtimes { get; set; }
        public virtual DbSet<PaySlip> PaySlips { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillEmployee> SkillEmployees { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=HRMS;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allowance>(entity =>
            {
                entity.Property(e => e.AllowanceId)
                    .HasMaxLength(10)
                    .HasColumnName("allowance_id");

                entity.Property(e => e.AllowanceType)
                    .HasMaxLength(200)
                    .HasColumnName("allowance_type");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Attendance");

                entity.Property(e => e.AttendanceStatus).HasColumnName("attendance_status");

                entity.Property(e => e.Day)
                    .HasColumnType("date")
                    .HasColumnName("day");

                entity.Property(e => e.EarlyLeaveHours).HasColumnName("early_leave_hours");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.LateHours).HasColumnName("late_hours");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1)
                    .HasColumnName("notes");

                entity.Property(e => e.TimeIn).HasColumnName("time_in");

                entity.Property(e => e.TimeOut).HasColumnName("time_out");

                entity.Property(e => e.TotalHours).HasColumnName("total_hours");

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Attendanc__emplo__628FA481");
            });

            modelBuilder.Entity<Deduction>(entity =>
            {
                entity.ToTable("Deduction");

                entity.Property(e => e.DeductionId)
                    .HasMaxLength(10)
                    .HasColumnName("deduction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.DeductionType)
                    .HasMaxLength(1)
                    .HasColumnName("deduction_type");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<DeductionSumary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DeductionSumary");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.DeductionId)
                    .HasMaxLength(10)
                    .HasColumnName("deduction_id");

                entity.Property(e => e.PayslipId)
                    .HasMaxLength(10)
                    .HasColumnName("payslip_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Deduction)
                    .WithMany()
                    .HasForeignKey(d => d.DeductionId)
                    .HasConstraintName("FK__Deduction__deduc__75A278F5");

                entity.HasOne(d => d.Payslip)
                    .WithMany()
                    .HasForeignKey(d => d.PayslipId)
                    .HasConstraintName("FK__Deduction__paysl__76969D2E");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(200)
                    .HasColumnName("department_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<DepartmentMemberList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DepartmentMemberList");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .HasColumnName("department_id");

                entity.Property(e => e.EmpRole)
                    .HasMaxLength(20)
                    .HasColumnName("emp_role");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Departmen__depar__52593CB8");

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Departmen__emplo__5165187F");
            });

            modelBuilder.Entity<DetailTaxIncome>(entity =>
            {
                entity.ToTable("detail_tax_income");

                entity.Property(e => e.DetailTaxIncomeId)
                    .HasMaxLength(10)
                    .HasColumnName("detail_tax_income_id");

                entity.Property(e => e.MucChiuThue).HasColumnName("muc_chiu_thue");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.ThueSuat).HasColumnName("thue_suat");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.BankAccountName).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.EmployeeAddress)
                    .HasMaxLength(200)
                    .HasColumnName("employee_address");

                entity.Property(e => e.EmployeeImage)
                    .HasMaxLength(200)
                    .HasColumnName("employee_image");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.JobId)
                    .HasMaxLength(10)
                    .HasColumnName("job_id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Employee__depart__48CFD27E");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Employee__job_id__47DBAE45");
            });

            modelBuilder.Entity<EmployeeBenefit>(entity =>
            {
                entity.HasKey(e => e.AllowancesId)
                    .HasName("PK__Employee__BA57CC1BDE70313F");

                entity.ToTable("EmployeeBenefit");

                entity.Property(e => e.AllowancesId)
                    .HasMaxLength(10)
                    .HasColumnName("allowances_id");

                entity.Property(e => e.AllowanceId)
                    .HasMaxLength(10)
                    .HasColumnName("allowance_id");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Allowance)
                    .WithMany(p => p.EmployeeBenefits)
                    .HasForeignKey(d => d.AllowanceId)
                    .HasConstraintName("FK__EmployeeB__allow__6EF57B66");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeBenefits)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeB__emplo__6E01572D");
            });

            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK__Employee__F8D664239AF32162");

                entity.ToTable("EmployeeContract");

                entity.Property(e => e.ContractId)
                    .HasMaxLength(10)
                    .HasColumnName("contract_id");

                entity.Property(e => e.BaseSalary)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("base_salary");

                entity.Property(e => e.ContractFile)
                    .HasMaxLength(200)
                    .HasColumnName("contract_file");

                entity.Property(e => e.ContractType)
                    .HasMaxLength(50)
                    .HasColumnName("contract_type");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Job)
                    .HasMaxLength(200)
                    .HasColumnName("job");

                entity.Property(e => e.PercentDeduction).HasColumnName("percent_deduction");

                entity.Property(e => e.SalaryType)
                    .HasMaxLength(50)
                    .HasColumnName("salary_type");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeContracts)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeC__emplo__60A75C0F");
            });

            modelBuilder.Entity<EmployeeLoanLog>(entity =>
            {
                entity.HasKey(e => e.LoanId)
                    .HasName("PK__Employee__A1F79554E7FBCFDF");

                entity.ToTable("EmployeeLoanLog");

                entity.Property(e => e.LoanId)
                    .HasMaxLength(10)
                    .HasColumnName("loan_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.ApprovalStatus)
                    .HasMaxLength(1)
                    .HasColumnName("approval_status");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.InstallmentAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("installment_amount");

                entity.Property(e => e.InstallmentFrequency)
                    .HasMaxLength(1)
                    .HasColumnName("installment_frequency");

                entity.Property(e => e.LoanEndDate)
                    .HasColumnType("date")
                    .HasColumnName("loan_end_date");

                entity.Property(e => e.LoanProvider)
                    .HasMaxLength(1)
                    .HasColumnName("loan_provider");

                entity.Property(e => e.LoanStartDate)
                    .HasColumnType("date")
                    .HasColumnName("loan_start_date");

                entity.Property(e => e.LoanType)
                    .HasMaxLength(1)
                    .HasColumnName("loan_type");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLoanLogs)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeL__emplo__656C112C");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.ToTable("Experience");

                entity.Property(e => e.ExperienceId)
                    .HasMaxLength(10)
                    .HasColumnName("experience_id");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.NameProject)
                    .HasMaxLength(50)
                    .HasColumnName("name_project");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TeamSize).HasColumnName("team_size");

                entity.Property(e => e.TechStack)
                    .HasMaxLength(500)
                    .HasColumnName("tech_stack");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Experiences)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Experienc__emplo__4F7CD00D");
            });

            modelBuilder.Entity<GrantedPermission>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GrantedPermission");

                entity.Property(e => e.PermissionId)
                    .HasMaxLength(10)
                    .HasColumnName("permission_id");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(10)
                    .HasColumnName("role_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Permission)
                    .WithMany()
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK__GrantedPe__permi__3C69FB99");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__GrantedPe__role___3B75D760");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobId)
                    .HasMaxLength(10)
                    .HasColumnName("job_id");

                entity.Property(e => e.AllowanceId)
                    .HasMaxLength(10)
                    .HasColumnName("allowance_id");

                entity.Property(e => e.BaseSalaryPerHour)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("base_salary_per_hour");

                entity.Property(e => e.Bonus)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("bonus");

                entity.Property(e => e.JobDescription)
                    .HasMaxLength(500)
                    .HasColumnName("job_description");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(200)
                    .HasColumnName("job_title");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Allowance)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.AllowanceId)
                    .HasConstraintName("FK__Job__allowance_i__44FF419A");
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.ToTable("Leave");

                entity.Property(e => e.LeaveId)
                    .HasMaxLength(10)
                    .HasColumnName("leave_id");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.LeaveHours).HasColumnName("leave_hours");

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(50)
                    .HasColumnName("leave_type");

                entity.Property(e => e.Reason)
                    .HasMaxLength(50)
                    .HasColumnName("reason");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Leave__employee___68487DD7");
            });

            modelBuilder.Entity<Overtime>(entity =>
            {
                entity.ToTable("Overtime");

                entity.Property(e => e.OvertimeId)
                    .HasMaxLength(10)
                    .HasColumnName("overtime_id");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.OvertimeHours)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("overtime_hours");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Overtimes)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Overtime__employ__6B24EA82");
            });

            modelBuilder.Entity<PaySlip>(entity =>
            {
                entity.ToTable("PaySlip");

                entity.Property(e => e.PayslipId)
                    .HasMaxLength(10)
                    .HasColumnName("payslip_id");

                entity.Property(e => e.ActualWorkHours).HasColumnName("actual_work_hours");

                entity.Property(e => e.AllowancesId)
                    .HasMaxLength(10)
                    .HasColumnName("allowances_id");

                entity.Property(e => e.Approval)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("approval");

                entity.Property(e => e.BankAccountName).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.BaseSalary).HasColumnName("base_salary");

                entity.Property(e => e.Bonus).HasColumnName("bonus");

                entity.Property(e => e.ContractId)
                    .HasMaxLength(10)
                    .HasColumnName("contract_id");

                entity.Property(e => e.DeductionSum).HasColumnName("deduction_sum");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(1)
                    .HasColumnName("note");

                entity.Property(e => e.OtHours).HasColumnName("ot_hours");

                entity.Property(e => e.PaidDate)
                    .HasColumnType("date")
                    .HasColumnName("paid_date");

                entity.Property(e => e.PayPeriod)
                    .HasMaxLength(50)
                    .HasColumnName("pay_period");

                entity.Property(e => e.StarndardWorkHours).HasColumnName("starndard_work_hours");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.Property(e => e.TaxIncome).HasColumnName("tax_income");

                entity.Property(e => e.TotalSalary).HasColumnName("total_salary");

                entity.HasOne(d => d.Allowances)
                    .WithMany(p => p.PaySlips)
                    .HasForeignKey(d => d.AllowancesId)
                    .HasConstraintName("FK__PaySlip__allowan__73BA3083");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.PaySlips)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK__PaySlip__contrac__71D1E811");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PaySlips)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__PaySlip__employe__72C60C4A");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId)
                    .HasMaxLength(10)
                    .HasColumnName("permission_id");

                entity.Property(e => e.PermissionDes)
                    .HasMaxLength(500)
                    .HasColumnName("permission_des");

                entity.Property(e => e.PermissionDisplayName)
                    .HasMaxLength(200)
                    .HasColumnName("permission_displayName");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(10)
                    .HasColumnName("project_id");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .HasColumnName("department_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("project_name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Project__departm__59FA5E80");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(10)
                    .HasColumnName("report_id");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issue_date");

                entity.Property(e => e.Reason)
                    .HasMaxLength(255)
                    .HasColumnName("reason");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Report__employee__571DF1D5");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .HasMaxLength(10)
                    .HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.SkillId)
                    .HasMaxLength(10)
                    .HasColumnName("skill_id");

                entity.Property(e => e.SkillDescription)
                    .HasMaxLength(500)
                    .HasColumnName("skill_description");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(100)
                    .HasColumnName("skill_name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<SkillEmployee>(entity =>
            {
                entity.HasKey(e => e.UniqueId)
                    .HasName("PK__Skill_em__A2929130C50DA7B0");

                entity.ToTable("Skill_employee");

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(10)
                    .HasColumnName("unique_id");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.Level)
                    .HasMaxLength(50)
                    .HasColumnName("level");

                entity.Property(e => e.SkillId)
                    .HasMaxLength(10)
                    .HasColumnName("skill_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SkillEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Skill_emp__emplo__5CD6CB2B");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.SkillEmployees)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK__Skill_emp__skill__5DCAEF64");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .HasColumnName("user_id");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .HasColumnName("employee_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(10)
                    .HasColumnName("role_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Users__employee___4CA06362");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__role_id__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
