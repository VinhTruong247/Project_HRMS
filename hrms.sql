--Create Database HRMS
DROP DATABASE IF EXISTS HRMS;
CREATE DATABASE HRMS;
USE HRMS;

-- Create Department table
CREATE TABLE Department (
  department_id INT PRIMARY KEY,
  department_name NVARCHAR(200),
  description NVARCHAR(500)
);
-- Create Roles table
CREATE TABLE Roles (
  role_id INT PRIMARY KEY,
  role_name NVARCHAR(50)
);

-- Create Permission table
CREATE TABLE Permission (
  permission_id INT PRIMARY KEY,
  permission_des NVARCHAR(500),
  permission_displayName NVARCHAR(200)
);

-- Create Users table
CREATE TABLE Users (
  user_id INT PRIMARY KEY,
  username NVARCHAR(50),
  password NVARCHAR(50),
  Email NVARCHAR(100),
  role_id INT,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);

-- Create GrantedPermission table
CREATE TABLE GrantedPermission (
  role_id INT,
  permission_id INT,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id),
  FOREIGN KEY (permission_id) REFERENCES Permission(permission_id)
);
-- Create detail_tax_income table
CREATE TABLE detail_tax_income (
  detail_tax_income_id INT PRIMARY KEY,
  muc_chiu_thue FLOAT,
  thue_suat FLOAT
);
-- Create Experience table
CREATE TABLE Experience (
  experience_id INT PRIMARY KEY,
  name_project NVARCHAR(50),
  team_size INT,
  start_date DATE,
  end_date DATE,
  tech_stack NVARCHAR(500)
);
-- Create Skill table
CREATE TABLE Skill (
  skill_id INT PRIMARY KEY,
  skill_name NVARCHAR(100),
  skill_description NVARCHAR(500)
);
-- Create Deduction table
CREATE TABLE Deduction (
  deduction_id INT PRIMARY KEY,
  deduction_type NVARCHAR,
  amount DECIMAL(18, 2)
);

-- Create Allowances table
CREATE TABLE Allowances (
  allowance_id INT PRIMARY KEY,
  allowance_type NVARCHAR(200),
  amount DECIMAL(18, 2)
);
-- Create Skill_employee table
CREATE TABLE Skill_employee (
  unique_id INT PRIMARY KEY,
  employee_id INT,
  level NVARCHAR(50),
  skill_id INT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (skill_id) REFERENCES Skill(skill_id)
);

-- Create Job table
CREATE TABLE Job (
  job_id INT PRIMARY KEY,
  job_title NVARCHAR(200),
  job_description NVARCHAR(500),
  start_date DATE,
  --end_date DATE,
  status NVARCHAR(200),
  base_salary_per_hour DECIMAL(18, 2),
  allowance_id INT,
  bonus DECIMAL(18, 2),
  FOREIGN KEY (allowance_id) REFERENCES Allowances(allowance_id)
);
-- Create Employee table
CREATE TABLE Employee (
  employee_id INT PRIMARY KEY,
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
  experience_id INT,
  user_id INT,
  job_id INT,
  department_id INT,
  FOREIGN KEY (user_id) REFERENCES Users(user_id),
  FOREIGN KEY (experience_id) REFERENCES Experience(experience_id),
  FOREIGN KEY (job_id) REFERENCES Job(job_id),
  FOREIGN KEY (department_id) REFERENCES Department(department_id)
);

-- Create EmployeeContract table
CREATE TABLE EmployeeContract (
  contract_id INT PRIMARY KEY,
  employee_id INT,
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

-- Create Leave table
CREATE TABLE Leave (
  leave_id INT PRIMARY KEY,
  employee_id INT,
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
  overtime_id INT PRIMARY KEY,
  employee_id INT,
  Day DATE,
  overtime_hours DECIMAL(5, 2),
  status NVARCHAR(50),
  isDeleted BIT,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);



-- Create PaySlip table
CREATE TABLE PaySlip (
  payslip_id INT PRIMARY KEY,
  employee_id INT,
  pay_period NVARCHAR(50),
  paid_date DATE,
  base_salary FLOAT,
  ot_hours FLOAT,
  allowances_id INT,
  contract_id INT,
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




-- Create EmployeeLoanLog table
CREATE TABLE EmployeeLoanLog (
  loan_id INT PRIMARY KEY,
  employee_id INT,
  loan_type NVARCHAR,
  amount DECIMAL(18, 2),
  installment_amount DECIMAL(18, 2),
  installment_frequency NVARCHAR,
  loan_start_date DATE,
  loan_end_date DATE,
  loan_provider NVARCHAR,
  approval_status NVARCHAR,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id)
);


-- Create DeductionSumary table
CREATE TABLE DeductionSumary (
  deduction_id INT,
  payslip_id INT,
  amount DECIMAL(18, 2),
  FOREIGN KEY (deduction_id) REFERENCES Deduction(deduction_id),
  FOREIGN KEY (payslip_id) REFERENCES PaySlip(payslip_id)
);



-- Create Attendance table
CREATE TABLE Attendance (
  employee_id INT,
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

-- Create EmployeeBenefit table
CREATE TABLE EmployeeBenefit (
  employee_id INT,
  allowance_id INT,
  allowances_id INT PRIMARY KEY,
  FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  FOREIGN KEY (allowance_id) REFERENCES Allowances(allowance_id)
);
