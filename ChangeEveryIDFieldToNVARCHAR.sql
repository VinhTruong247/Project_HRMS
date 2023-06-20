-- Alter Department table
ALTER TABLE Department
ALTER COLUMN department_id NVARCHAR(50);

-- Alter DepartmentMemberList table
ALTER TABLE DepartmentMemberList
ALTER COLUMN department_id NVARCHAR(50);

-- Alter Roles table
ALTER TABLE Roles
ALTER COLUMN role_id NVARCHAR(50);

-- Alter Permission table
ALTER TABLE Permission
ALTER COLUMN permission_id NVARCHAR(50);

-- Alter Users table
ALTER TABLE Users
ALTER COLUMN user_id NVARCHAR(50);

-- Alter GrantedPermission table
ALTER TABLE GrantedPermission
ALTER COLUMN role_id NVARCHAR(50);

-- Alter GrantedPermission table
ALTER TABLE GrantedPermission
ALTER COLUMN permission_id NVARCHAR(50);

-- Alter detail_tax_income table
ALTER TABLE detail_tax_income
ALTER COLUMN detail_tax_income_id NVARCHAR(50);

-- Alter Experience table
ALTER TABLE Experience
ALTER COLUMN experience_id NVARCHAR(50);

-- Alter Skill table
ALTER TABLE Skill
ALTER COLUMN skill_id NVARCHAR(50);

-- Alter Deduction table
ALTER TABLE Deduction
ALTER COLUMN deduction_id NVARCHAR(50);

-- Alter Allowances table
ALTER TABLE Allowances
ALTER COLUMN allowance_id NVARCHAR(50);

-- Alter Job table
ALTER TABLE Job
ALTER COLUMN job_id NVARCHAR(50);

-- Alter Employee table
ALTER TABLE Employee
ALTER COLUMN employee_id NVARCHAR(50);

-- Alter Skill_employee table
ALTER TABLE Skill_employee
ALTER COLUMN unique_id NVARCHAR(50);

-- Alter EmployeeContract table
ALTER TABLE EmployeeContract
ALTER COLUMN contract_id NVARCHAR(50);

-- Alter Leave table
ALTER TABLE Leave
ALTER COLUMN leave_id NVARCHAR(50);

-- Alter Overtime table
ALTER TABLE Overtime
ALTER COLUMN overtime_id NVARCHAR(50);

-- Alter PaySlip table
ALTER TABLE PaySlip
ALTER COLUMN payslip_id NVARCHAR(50);

-- Alter EmployeeLoanLog table
ALTER TABLE EmployeeLoanLog
ALTER COLUMN loan_id NVARCHAR(50);

-- Alter DeductionSumary table
ALTER TABLE DeductionSumary
ALTER COLUMN deduction_id NVARCHAR(50);

-- Alter DeductionSumary table
ALTER TABLE DeductionSumary
ALTER COLUMN payslip_id NVARCHAR(50);

-- Alter Attendance table
ALTER TABLE Attendance
ALTER COLUMN employee_id NVARCHAR(50);

-- Alter EmployeeBenefit table
ALTER TABLE EmployeeBenefit
ALTER COLUMN employee_id NVARCHAR(50);

-- Alter EmployeeBenefit table
ALTER TABLE EmployeeBenefit
ALTER COLUMN allowances_id NVARCHAR(50);

-- Alter Report table
ALTER TABLE Report
ALTER COLUMN report_id NVARCHAR(50);

-- Alter Project table
ALTER TABLE Project
ALTER COLUMN project_id NVARCHAR(50);
