--Create Database HRMS
DROP DATABASE IF EXISTS HRMS;
CREATE DATABASE HRMS;
USE HRMS;

-- Create Department table
CREATE TABLE Department (
  department_id NVARCHAR(10) PRIMARY KEY,
  department_name NVARCHAR(200),
  description NVARCHAR(500)
);

-- Create Roles table
CREATE TABLE Roles (
  role_id NVARCHAR(10) PRIMARY KEY,
  role_name NVARCHAR(50),
  status NVARCHAR(10)
);

-- Create Permission table
CREATE TABLE Permission (
  permission_id NVARCHAR(10) PRIMARY KEY,
  permission_des NVARCHAR(500),
  permission_displayName NVARCHAR(200),
  status NVARCHAR(10)
  
);

-- Create Users table
CREATE TABLE Users (
  user_id NVARCHAR(10) PRIMARY KEY,
  username NVARCHAR(50),
  password NVARCHAR(50),
  Email NVARCHAR(100),
  role_id NVARCHAR(10),
  status NVARCHAR,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);

-- Create GrantedPermission table
CREATE TABLE GrantedPermission (
  role_id NVARCHAR(10),
  permission_id NVARCHAR(10),
  status NVARCHAR(10),
  FOREIGN KEY (role_id) REFERENCES Roles(role_id),
  FOREIGN KEY (permission_id) REFERENCES Permission(permission_id)
);

-- Create detail_tax_income table
CREATE TABLE detail_tax_income (
  detail_tax_income_id NVARCHAR(10) PRIMARY KEY,
  muc_chiu_thue FLOAT,
  thue_suat FLOAT,
status NVARCHAR(10)
);

-- Create Skill table
CREATE TABLE Skill (
  skill_id NVARCHAR(10) PRIMARY KEY,
  skill_name NVARCHAR(100),
  skill_description NVARCHAR(500),
  status NVARCHAR(10)
);

-- Create Deduction table
CREATE TABLE Deduction (
  deduction_id NVARCHAR(10) PRIMARY KEY,
  deduction_type NVARCHAR,
  amount DECIMAL(18, 2),
  status NVARCHAR(10)
);

-- Create Allowances table
CREATE TABLE Allowances (
  allowance_id NVARCHAR(10) PRIMARY KEY,
  allowance_type NVARCHAR(200),
  amount DECIMAL(18, 2),
  status NVARCHAR(10)
);

-- Create Job table
CREATE TABLE Job (
  job_id NVARCHAR(10) PRIMARY KEY,
  job_title NVARCHAR(200),
  job_description NVARCHAR(500),
  start_date DATE,
  status NVARCHAR(200),
  base_salary_per_hour DECIMAL(18, 2),
  allowance_id NVARCHAR(10),
  bonus DECIMAL(18, 2),
  FOREIGN KEY (allowance_id) REFERENCES Allowances(allowance_id)
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
  user_id NVARCHAR(10),
  job_id NVARCHAR(10),
  department_id NVARCHAR(10),
  status NVARCHAR (10),
  FOREIGN KEY (user_id) REFERENCES Users(user_id),
  FOREIGN KEY (job_id) REFERENCES Job(job_id),
  FOREIGN KEY (department_id) REFERENCES Department(department_id)
);
-- Create Experience table
CREATE TABLE Experience (
  experience_id NVARCHAR(10) PRIMARY KEY,
  name_project NVARCHAR(50),
  team_size INT,
  start_date DATE,
  end_date DATE,
  tech_stack NVARCHAR(500),
  status NVARCHAR(10),
  employee_id NVARCHAR(10),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
--Create DepartmentMemberList table
CREATE TABLE DepartmentMemberList (
  department_id NVARCHAR(10),
  employee_id NVARCHAR(10),
  emp_role NVARCHAR(20),
  status NVARCHAR(10),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (department_id) REFERENCES Department (department_id)
);
--CREATE TABLE Report
CREATE TABLE Report (
  report_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  reason NVARCHAR(255),
  content TEXT,
  issue_date DATE,
  status VARCHAR(50),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
--CREATE TABLE Project
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
  status NVARCHAR(10),
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
  job NVARCHAR(200),
  base_salary DECIMAL(18, 2),
  status NVARCHAR(200),
  percent_deduction FLOAT,
  salary_type NVARCHAR(50),
  contract_type NVARCHAR(50),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
-- Create Attendance table
CREATE TABLE Attendance (
  employee_id NVARCHAR(10),
  day DATE,
  time_in FLOAT,
  time_out FLOAT,
  late_hours FLOAT,
  early_leave_hours FLOAT,
  total_hours FLOAT,
  attendance_status NVARCHAR,
  notes NVARCHAR,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
-- Create EmployeeLoanLog table
CREATE TABLE EmployeeLoanLog (
  loan_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  loan_type NVARCHAR,
  amount DECIMAL(18, 2),
  installment_amount DECIMAL(18, 2),
  installment_frequency NVARCHAR,
  loan_start_date DATE,
  loan_end_date DATE,
  loan_provider NVARCHAR,
  approval_status NVARCHAR,
  status NVARCHAR(10),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
-- Create Leave table
CREATE TABLE Leave (
  leave_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  leave_type NVARCHAR(50),
  start_date DATE,
  end_date DATE,
  reason NVARCHAR(50),
  status NVARCHAR(50),
  leave_hours FLOAT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);

-- Create Overtime table
CREATE TABLE Overtime (
  overtime_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  Day DATE,
  overtime_hours DECIMAL(5, 2),
  status NVARCHAR(50),
  isDeleted BIT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);
-- Create EmployeeBenefit table
CREATE TABLE EmployeeBenefit (
  employee_id NVARCHAR(10),
  allowance_id NVARCHAR(10),
  allowances_id NVARCHAR(10) PRIMARY KEY,
  status NVARCHAR,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (allowance_id) REFERENCES Allowances(allowance_id)
);
-- Create PaySlip table
CREATE TABLE PaySlip (
  payslip_id NVARCHAR(10) PRIMARY KEY,
  employee_id NVARCHAR(10),
  pay_period NVARCHAR(50),
  paid_date DATE,
  base_salary FLOAT,
  ot_hours FLOAT,
  allowances_id NVARCHAR(10),
  contract_id NVARCHAR(10),
  starndard_work_hours FLOAT,
  actual_work_hours FLOAT,
  tax_income FLOAT,
  bonus FLOAT,
  deduction_sum FLOAT,
  total_salary FLOAT,
  note NVARCHAR,
  BankAccountNumber INT,
  BankAccountName NVARCHAR(50),
  BankName NVARCHAR(50),
  approval VARCHAR,
  status VARCHAR,
  FOREIGN KEY (contract_id) REFERENCES EmployeeContract(contract_id),
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (allowances_id) REFERENCES EmployeeBenefit(allowances_id)
);

-- Create DeductionSumary table
CREATE TABLE DeductionSumary (
  deduction_id NVARCHAR(10),
  payslip_id NVARCHAR(10),
  amount DECIMAL(18, 2),
  status NVARCHAR(10),
  FOREIGN KEY (deduction_id) REFERENCES Deduction(deduction_id),
  FOREIGN KEY (payslip_id) REFERENCES PaySlip(payslip_id)
);






