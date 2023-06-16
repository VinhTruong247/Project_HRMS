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
                    .ValueGeneratedNever()
                    .HasColumnName("allowance_id");

                entity.Property(e => e.AllowanceType)
                    .HasMaxLength(200)
                    .HasColumnName("allowance_type");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Attendance");

                entity.Property(e => e.AttendanceStatus)
                    .HasMaxLength(1)
                    .HasColumnName("attendance_status");

                entity.Property(e => e.Day)
                    .HasColumnType("date")
                    .HasColumnName("day");

                entity.Property(e => e.EarlyLeaveHours).HasColumnName("early_leave_hours");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

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
                    .HasConstraintName("FK__Attendanc__emplo__1DB06A4F");
            });

            modelBuilder.Entity<Deduction>(entity =>
            {
                entity.ToTable("Deduction");

                entity.Property(e => e.DeductionId)
                    .ValueGeneratedNever()
                    .HasColumnName("deduction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.DeductionType)
                    .HasMaxLength(1)
                    .HasColumnName("deduction_type");
            });

            modelBuilder.Entity<DeductionSumary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DeductionSumary");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.DeductionId).HasColumnName("deduction_id");

                entity.Property(e => e.PayslipId).HasColumnName("payslip_id");

                entity.HasOne(d => d.Deduction)
                    .WithMany()
                    .HasForeignKey(d => d.DeductionId)
                    .HasConstraintName("FK__Deduction__deduc__1AD3FDA4");

                entity.HasOne(d => d.Payslip)
                    .WithMany()
                    .HasForeignKey(d => d.PayslipId)
                    .HasConstraintName("FK__Deduction__paysl__1BC821DD");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(200)
                    .HasColumnName("department_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<DepartmentMemberList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DepartmentMemberList");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.EmpRole)
                    .HasMaxLength(20)
                    .HasColumnName("emp_role");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Departmen__depar__5441852A");

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Departmen__emplo__534D60F1");
            });

            modelBuilder.Entity<DetailTaxIncome>(entity =>
            {
                entity.ToTable("detail_tax_income");

                entity.Property(e => e.DetailTaxIncomeId)
                    .ValueGeneratedNever()
                    .HasColumnName("detail_tax_income_id");

                entity.Property(e => e.MucChiuThue).HasColumnName("muc_chiu_thue");

                entity.Property(e => e.ThueSuat).HasColumnName("thue_suat");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("employee_id");

                entity.Property(e => e.BankAccountName).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.EmployeeAddress)
                    .HasMaxLength(200)
                    .HasColumnName("employee_address");

                entity.Property(e => e.EmployeeImage)
                    .HasMaxLength(200)
                    .HasColumnName("employee_image");

                entity.Property(e => e.ExperienceId).HasColumnName("experience_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Employee__depart__5165187F");

                entity.HasOne(d => d.Experience)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ExperienceId)
                    .HasConstraintName("FK__Employee__experi__4F7CD00D");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Employee__job_id__5070F446");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Employee__user_i__4E88ABD4");
            });

            modelBuilder.Entity<EmployeeBenefit>(entity =>
            {
                entity.HasKey(e => e.AllowancesId)
                    .HasName("PK__Employee__BA57CC1B584A1CAD");

                entity.ToTable("EmployeeBenefit");

                entity.Property(e => e.AllowancesId)
                    .ValueGeneratedNever()
                    .HasColumnName("allowances_id");

                entity.Property(e => e.AllowanceId).HasColumnName("allowance_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.HasOne(d => d.Allowance)
                    .WithMany(p => p.EmployeeBenefits)
                    .HasForeignKey(d => d.AllowanceId)
                    .HasConstraintName("FK__EmployeeB__allow__114A936A");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeBenefits)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeB__emplo__10566F31");
            });

            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK__Employee__F8D6642344482958");

                entity.ToTable("EmployeeContract");

                entity.Property(e => e.ContractId)
                    .ValueGeneratedNever()
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

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeContracts)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeC__emplo__68487DD7");
            });

            modelBuilder.Entity<EmployeeLoanLog>(entity =>
            {
                entity.HasKey(e => e.LoanId)
                    .HasName("PK__Employee__A1F79554C8ED4271");

                entity.ToTable("EmployeeLoanLog");

                entity.Property(e => e.LoanId)
                    .ValueGeneratedNever()
                    .HasColumnName("loan_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.ApprovalStatus)
                    .HasMaxLength(1)
                    .HasColumnName("approval_status");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

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

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLoanLogs)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeL__emplo__7E37BEF6");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.ToTable("Experience");

                entity.Property(e => e.ExperienceId)
                    .ValueGeneratedNever()
                    .HasColumnName("experience_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.NameProject)
                    .HasMaxLength(50)
                    .HasColumnName("name_project");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.TeamSize).HasColumnName("team_size");

                entity.Property(e => e.TechStack)
                    .HasMaxLength(500)
                    .HasColumnName("tech_stack");
            });

            modelBuilder.Entity<GrantedPermission>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GrantedPermission");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Permission)
                    .WithMany()
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK__GrantedPe__permi__3F466844");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__GrantedPe__role___3E52440B");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobId)
                    .ValueGeneratedNever()
                    .HasColumnName("job_id");

                entity.Property(e => e.AllowanceId).HasColumnName("allowance_id");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .HasColumnName("status");

                entity.HasOne(d => d.Allowance)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.AllowanceId)
                    .HasConstraintName("FK__Job__allowance_i__4BAC3F29");
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.ToTable("Leave");

                entity.Property(e => e.LeaveId)
                    .ValueGeneratedNever()
                    .HasColumnName("leave_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Leave__employee___6B24EA82");
            });

            modelBuilder.Entity<Overtime>(entity =>
            {
                entity.ToTable("Overtime");

                entity.Property(e => e.OvertimeId)
                    .ValueGeneratedNever()
                    .HasColumnName("overtime_id");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

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
                    .HasConstraintName("FK__Overtime__employ__6E01572D");
            });

            modelBuilder.Entity<PaySlip>(entity =>
            {
                entity.ToTable("PaySlip");

                entity.Property(e => e.PayslipId)
                    .ValueGeneratedNever()
                    .HasColumnName("payslip_id");

                entity.Property(e => e.ActualWorkHours).HasColumnName("actual_work_hours");

                entity.Property(e => e.AllowancesId).HasColumnName("allowances_id");

                entity.Property(e => e.Approval)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("approval");

                entity.Property(e => e.BankAccountName).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.BaseSalary).HasColumnName("base_salary");

                entity.Property(e => e.Bonus).HasColumnName("bonus");

                entity.Property(e => e.ContractId).HasColumnName("contract_id");

                entity.Property(e => e.DeductionSum).HasColumnName("deduction_sum");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

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
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TaxIncome).HasColumnName("tax_income");

                entity.Property(e => e.TotalSalary).HasColumnName("total_salary");

                entity.HasOne(d => d.Allowances)
                    .WithMany(p => p.PaySlips)
                    .HasForeignKey(d => d.AllowancesId)
                    .HasConstraintName("FK__PaySlip__allowan__160F4887");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.PaySlips)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK__PaySlip__contrac__14270015");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PaySlips)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__PaySlip__employe__151B244E");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId)
                    .ValueGeneratedNever()
                    .HasColumnName("permission_id");

                entity.Property(e => e.PermissionDes)
                    .HasMaxLength(500)
                    .HasColumnName("permission_des");

                entity.Property(e => e.PermissionDisplayName)
                    .HasMaxLength(200)
                    .HasColumnName("permission_displayName");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.SkillId)
                    .ValueGeneratedNever()
                    .HasColumnName("skill_id");

                entity.Property(e => e.SkillDescription)
                    .HasMaxLength(500)
                    .HasColumnName("skill_description");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(100)
                    .HasColumnName("skill_name");
            });

            modelBuilder.Entity<SkillEmployee>(entity =>
            {
                entity.HasKey(e => e.UniqueId)
                    .HasName("PK__Skill_em__A2929130776C476C");

                entity.ToTable("Skill_employee");

                entity.Property(e => e.UniqueId)
                    .ValueGeneratedNever()
                    .HasColumnName("unique_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Level)
                    .HasMaxLength(50)
                    .HasColumnName("level");

                entity.Property(e => e.SkillId).HasColumnName("skill_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SkillEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Skill_emp__emplo__6477ECF3");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.SkillEmployees)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK__Skill_emp__skill__656C112C");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__role_id__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
