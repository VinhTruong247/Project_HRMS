DROP DATABASE IF EXISTS HRMS;
CREATE DATABASE HRMS;
USE HRMS;

-- Create Department table
CREATE TABLE Department (
  department_id NVARCHAR(10) PRIMARY KEY,
  department_name NVARCHAR(200),
  description NVARCHAR(500),
  status BIT
);

-- Create Roles table
CREATE TABLE Roles (
  role_id NVARCHAR(10) PRIMARY KEY,
  role_name NVARCHAR(50),
  status BIT
);

-- Create Permission table
CREATE TABLE Permission (
  permission_id NVARCHAR(10) PRIMARY KEY,
  permission_des NVARCHAR(500),
  permission_displayName NVARCHAR(200),
  status BIT
);

-- Create GrantedPermission table
CREATE TABLE GrantedPermission (
  role_id NVARCHAR(10),
  permission_id NVARCHAR(10),
  status BIT,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id),
  FOREIGN KEY (permission_id) REFERENCES Permission(permission_id)
);

-- Create Skill table
CREATE TABLE Skill (
  skill_id NVARCHAR(10) PRIMARY KEY,
  skill_name NVARCHAR(100),
  skill_description NVARCHAR(500),
  status BIT
);

-- Create Allowances table
CREATE TABLE Allowances (
  allowance_id NVARCHAR(10) PRIMARY KEY,
  allowance_name NVARCHAR(200),
  allowance_type NVARCHAR(200),
  amount DECIMAL(18, 2),
  status BIT
);


-- Create Job table
CREATE TABLE Job (
  job_id NVARCHAR(10) PRIMARY KEY,
  job_title NVARCHAR(200),
  job_description NVARCHAR(500),
  status BIT,
  base_salary_per_hour DECIMAL(18, 2),
  bonus DECIMAL(18, 2),
);

-- Create Employee table
CREATE TABLE Employee (
  employee_id NVARCHAR(10) PRIMARY KEY,
  first_name NVARCHAR(50),
  last_name NVARCHAR(50),
  employee_image NVARCHAR(200),
  date_of_birth DATE,
  employee_address NVARCHAR(200),
  email NVARCHAR(100),
  phone_number VARCHAR(20),
  BankAccountNumber INT,
  BankAccountName NVARCHAR(50),
  BankName NVARCHAR(50),
  dependents INT,
  job_id NVARCHAR(10),
  department_id NVARCHAR(10),
  status BIT,
  FOREIGN KEY (job_id) REFERENCES Job(job_id),
  FOREIGN KEY (department_id) REFERENCES Department(department_id)
);

-- Create Users table
CREATE TABLE Users (
  user_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  username NVARCHAR(50),
  password NVARCHAR(50),
  Email NVARCHAR(100),
  role_id NVARCHAR(10),
  status BIT,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

-- Create Experience table
CREATE TABLE Experience (
  experience_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  name_project NVARCHAR(50),
  team_size INT,
  start_date DATE,
  end_date DATE,
  tech_stack NVARCHAR(500),
  status BIT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

-- Create DepartmentMemberList table
CREATE TABLE DepartmentMemberList (
  department_id NVARCHAR(10),
  employee_id NVARCHAR(10),
  emp_role NVARCHAR(50),
  status BIT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (department_id) REFERENCES Department(department_id)
);

-- Create detail_tax_income table
CREATE TABLE detail_tax_income (
  detail_tax_income_id NVARCHAR(10) PRIMARY KEY,
  muc_chiu_thue FLOAT,
  thue_suat FLOAT,
  status BIT
);

-- Create Report table
CREATE TABLE Report (
  report_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  reason NVARCHAR(255),
  content TEXT,
  issue_date DATE,
  status NVARCHAR(10),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

-- Create Project table
CREATE TABLE Project (
  project_id NVARCHAR(10) PRIMARY KEY,
  project_name VARCHAR(255),
  department_id NVARCHAR(10),
  start_date DATE,
  end_date DATE,
  status VARCHAR(50),
  FOREIGN KEY (department_id) REFERENCES Department(department_id)
);

-- Create Skill_employee table
CREATE TABLE Skill_employee (
  unique_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  level NVARCHAR(50),
  skill_id NVARCHAR(10),
  status BIT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (skill_id) REFERENCES Skill(skill_id)
);

-- Create EmployeeContract table
CREATE TABLE EmployeeContract (
  contract_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  contract_file NVARCHAR(200),
  start_date DATE,
  end_date DATE,
  job_id NVARCHAR(10),
  base_salary DECIMAL(18, 2),
  status BIT,
  percent_deduction FLOAT,
  salary_type NVARCHAR(50),
  contract_type NVARCHAR(50),
  FOREIGN KEY (job_id) REFERENCES Job(job_id),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

-- Create Attendance table
CREATE TABLE Attendance (
  attendance_id NVARCHAR(10) primary key,
  employee_id NVARCHAR(10),
  day DATE,
  time_in TIME,
  time_out TIME,
  late_hours TIME,
  early_leave_hours TIME,
  total_hours TIME,
  attendance_status BIT,
  notes NVARCHAR(50),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
--Create Timesheet table
create table Timesheet(
	 timesheet_id NVARCHAR(10) PRIMARY KEY,
	 employee_id NVARCHAR(10),
	 time_in TIME,
	 time_out TIME,
	 day DATE,
	 status BIT,
	 totalWorkHours time,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
)

-- Create Leave table
CREATE TABLE Leave (
  leave_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  leave_type NVARCHAR(50),
  start_date DATE,
  end_date DATE,
  reason NVARCHAR(50),
  status BIT,
  leave_hours DECIMAL,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

-- Create Overtime table
CREATE TABLE Overtime (
  overtime_id NVARCHAR(10) PRIMARY KEY,
  overtime_type NVARCHAR(50),
  employee_id NVARCHAR(10),
  Day DATE,
  overtime_hours TIME,
  status NVARCHAR(50),
  isDeleted BIT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

-- Create EmployeeBenefit table
CREATE TABLE EmployeeBenefit (
  employee_id NVARCHAR(10),
  allowance_id NVARCHAR(10),
  employeebenefit_id NVARCHAR(10) PRIMARY KEY,
  start_date DATE,
  end_date DATE,
  status BIT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (allowance_id) REFERENCES Allowances(allowance_id)
);

-- Create PaySlip table
CREATE TABLE PaySlip (
  payslip_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  pay_period NVARCHAR(50),
  paid_date DATE,
  base_salary DECIMAL(18, 2),
  ot_hours DECIMAL,
  contract_id NVARCHAR(10),
  standard_work_hours DECIMAL,
  actual_work_hours DECIMAL,
  tax_income DECIMAL(18, 2),
  total_salary DECIMAL(18, 2),
  note NVARCHAR(255),
  BankAccountNumber INT,
  BankAccountName NVARCHAR(50),
  BankName NVARCHAR(50),
  status NVARCHAR(10),
  FOREIGN KEY (contract_id) REFERENCES EmployeeContract(contract_id),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
);

--Create DailySalary table
Create table DailySalary (
	dailysalary_id NVARCHAR(10) PRIMARY KEY,
	employee_id NVARCHAR(10),
	date DATE,
	total_hours DECIMAL(18,2),
	salary_per_hour DECIMAL(18,2),
	total_salary DECIMAL(18, 2),
	ot_hours DECIMAL(18,2),
	ot_type NVARCHAR(50),
	ot_salary DECIMAL(18,2),
	FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

