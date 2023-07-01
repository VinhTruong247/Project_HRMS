﻿use HRMS  
delete from Employee
delete from users
delete from Job
delete from Department
delete from Experience
delete from Allowances
delete from EmployeeBenefit 

INSERT INTO Roles (role_id, role_name, status)
VALUES 
  ('RL000001', 'ADMIN', 'active'),
  ('RL000002', 'HR_Manager', 'active'),
  ('RL000003', 'HR_Staff', 'active'),
  ('RL000004', 'Employee', 'active');

-- Insert 13 employees

INSERT INTO Employee (employee_id, first_name, last_name, employee_image, date_of_birth, employee_address, email, phone_number, BankAccountNumber, BankAccountName, BankName, job_id, department_id,status)
VALUES
  ('EP000001', N'Hồ', N'Nguyễn', 'ho_nguyen.jpg', '1990-05-15', '123 Main St, City, Country', 'ho.nguyen@example.com', '+1234567890', '123456789', N'Hồ Nguyễn', 'Bank XYZ', 'JB000001', 'DP000001','Active'),
  ('EP000002', N'Mai', N'Trần', 'mai_tran.jpg', '1992-09-20', '456 Elm St, City, Country', 'mai.tran@example.com', '+0987654321', '987654321', N'Mai Trần', 'Bank ABC','JB000001', 'DP000001','Active'),
  ('EP000003', N'Thắm', N'Lê', 'tham_le.jpg', '1988-07-10', '789 Oak St, City, Country', 'tham.le@example.com', '+1357924680', '246813579', N'Thắm Lê', 'Bank DEF','JB000001', 'DP000001','Active'),
  ('EP000005', N'Bình', N'Lê', 'binh_le.jpg', '1991-11-05', '654 Cedar St, City, Country', 'binh.le@example.com', '+0123456789', '123456789', N'Bình Lê', 'Bank ABC','JB000004', 'DP000001','Active'),
  ('EP000004', N'Nam', N'Trần', 'nam_tran.jpg', '1995-03-25', '321 Pine St, City, Country', 'nam.tran@example.com', '+5678901234', '987654321', N'Nam Trần', 'Bank XYZ','JB000002', 'DP000002','Active'),
  ('EP000006', N'Lan', N'Vũ', 'lan_vu.jpg', '1993-12-30', '987 Maple St, City, Country', 'lan.vu@example.com', '+9876543210', '246813579', 'Lan Vũ', N'Bank DEF','JB000005', 'DP000006','Active'),
  ('EP000007', N'Minh', N'Trần', 'minh_tran.jpg', '1989-08-17', '147 Birch St, City, Country', 'minh.tran@example.com', '+5432109876', '987654321', N'Minh Trần', 'Bank XYZ','JB000005', 'DP000006','Active'),
  ('EP000008', N'An', N'Lê', 'an_le.jpg', '1994-06-12', '258 Walnut St, City, Country', 'an.le@example.com', '+7654321098', '123456789', N'An Lê', 'Bank ABC','JB000012', 'DP000004','Active');


INSERT INTO Users (user_id,employee_id username, password, Email, role_id, status)
VALUES
  ('US000001','EP000001', N'Ho', N'a123!', 'ho.nguyen@example.com', 'RL000004', 1),
  ('US000002','EP000002', N'Mai', N'abc123', 'mai.tran@example.com', 'RL000004', 1),
  ('US000003','EP000003', N'Tham', N'Str0ngP@ssw0rd', 'tham.le@example.com', 'RL000004', 1),
  ('US000004','EP000004', N'Nam', N'password123', 'nam.tran@example.com', 'RL000004', 1),
  ('US000005','EP000005', N'Binh', N'123456789', 'binh.le@example.com', 'RL000004', 1),
  ('US000006','EP000006', N'Lan', N'P@ssw0rd789!', 'lan.vu@example.com', 'RL000002', 1),
  ('US000007','EP000007', N'Minh', N'abc123', 'minh.tran@example.com', 'RL000003', 1),
  ('US000008','EP000008', N'An', N'Str0ngP@ssw0rd', 'an.le@example.com', 'RL000001', 1);



-- Insert 13 jobs
INSERT INTO Job (job_id, job_title, job_description, start_date, status, base_salary_per_hour, allowance_id, bonus)
VALUES
  ('JB000001', 'Software Engineer', N'Trách nhiệm phát triển ứng dụng phần mềm', '2023-01-01', 'Active', 1000000.00, 'AL000001', 100000000.00),
  ('JB000002', 'Data Analyst', N'Phân tích dữ liệu và cung cấp thông tin', '2023-02-01', 'Active', 900000.00, 'AL000002', 80000000.00),
  ('JB000003', 'Network Administrator', N'Quản lý và duy trì cơ sở hạ tầng mạng', '2023-03-01', 'Active', 1100000.00, 'AL000003', 120000000.00),
  ('JB000004', 'Web Developer', N'Trách nhiệm thiết kế và phát triển website', '2023-04-01', 'Active', 800000.00, 'AL000004', 60000000.00),
  ('JB000005', 'IT Project Manager', N'Lập kế hoạch, thực hiện và quản lý dự án IT', '2023-05-01', 'Active', 1200000.00, 'AL000005', 140000000.00),
  ('JB000006', 'Database Administrator', N'Quản lý và duy trì cơ sở dữ liệu', '2023-06-01', 'Active', 1000000.00, 'AL000006', 100000000.00),
  ('JB000007', 'Quality Assurance Engineer', N'Đảm bảo chất lượng phần mềm qua kiểm thử và phân tích', '2023-07-01', 'Active', 900000.00, 'AL000007', 80000000.00),
  ('JB000008', 'System Analyst', N'Phân tích và cải thiện hệ thống thông tin của công ty', '2023-08-01', 'Active', 1100000.00, 'AL000008', 120000000.00),
  ('JB000009', 'UI/UX Designer', N'Thiết kế giao diện người dùng và trải nghiệm người dùng', '2023-09-01', 'Active', 800000.00, 'AL000009', 60000000.00),
  ('JB000010', 'IT Support Specialist', N'Cung cấp hỗ trợ kỹ thuật cho người dùng cuối', '2023-10-01', 'Active', 1000000.00, 'AL000010', 100000000.00),
  ('JB000011', 'Business Analyst', N'Phân tích nhu cầu và xác định yêu cầu kinh doanh', '2023-11-01', 'Active', 950000.00, 'AL000007', 90000000.00),
  ('JB000012', 'IT Consultant', N'Tư vấn và đề xuất giải pháp công nghệ thông tin', '2023-12-01', 'Active', 1050000.00, 'AL000002', 110000000.00),
  ('JB000013', 'Cybersecurity Analyst', N'Phân tích và đảm bảo an ninh thông tin', '2024-01-01', 'Active', 950000.00, 'AL000006', 90000000.00);

  INSERT INTO Department (department_id, department_name, description ,status)
VALUES
  ('DP000001', 'Software Development', 'Responsible for developing software applications and solutions.', 'Active'),
  ('DP000002', 'Database Administration', 'Responsible for managing and maintaining databases.', 'Active'),
  ('DP000003', 'Network Operations', 'Responsible for managing the company''s network infrastructure.', 'Active'),
  ('DP000004', 'IT Support', 'Responsible for providing technical support to employees and resolving IT issues.', 'Active'),
  ('DP000005', 'Quality Assurance', 'Responsible for testing and ensuring the quality of software and systems.', 'Active'),
  ('DP000006', 'Human Resources', 'Responsible for managing HR processes and employee-related matters.', 'Active'),
  ('DP000007', 'Cybersecurity', 'Responsible for protecting the company''s IT systems and data from security threats.', 'Active'),
  ('DP000008', 'Business Analysis', 'Responsible for analyzing business requirements and recommending IT solutions.', 'Active');

  INSERT INTO Experience (experience_id, employee_id, name_project, team_size, start_date, end_date, tech_stack)
VALUES
  ('EX000001','EP000001', 'Website Redesign', 6, '2021-11-15', '2022-03-31', 'HTML, CSS, JavaScript'),
  ('EX000002','EP000002', 'Mobile App Development', 8, '2022-03-01', '2022-08-31', 'React Native, Firebase, TypeScript'),
  ('EX000003','EP000003', 'Data Analytics Platform', 5, '2022-09-01', '2023-02-28', 'Python, SQL, Tableau'),
  ('EX000004','EP000004', 'E-commerce Website Development', 8, '2022-02-15', '2022-07-31', 'PHP, Laravel, MySQL'),
  ('EX000005','EP000005', 'Mobile App Development', 6, '2021-10-01', '2022-02-28', 'Swift, iOS, Firebase'),
  ('EX000006','EP000006', 'Data Analysis and Visualization', 4, '2022-03-15', '2022-06-30', 'Python, Pandas, Matplotlib'),
  ('EX000007','EP000007', 'UI/UX Design for Web Applications', 3, '2022-06-01', '2022-08-31', 'Sketch, Adobe XD, InVision'),
  ('EX000008','EP000008', 'Blockchain Implementation', 5, '2021-12-01', '2022-04-30', 'Solidity, Ethereum, Truffle'),
  ('EX000009','EP000001', 'Cloud Infrastructure Deployment', 10, '2022-01-15', '2022-09-30', 'AWS, Azure, Terraform'),
  ('EX000010','EP000002', 'Machine Learning Model Development', 7, '2022-05-01', '2022-11-30', 'Python, scikit-learn, TensorFlow'),
  ('EX000011','EP000003', 'CRM System Integration', 4, '2021-09-01', '2022-01-31', 'Java, Spring Boot, Salesforce'),
  ('EX000012','EP000004', 'Chatbot Development', 3, '2022-03-01', '2022-05-31', 'Python, NLTK, Dialogflow'),
  ('EX000013','EP000005', 'Game Development', 6, '2021-11-01', '2022-07-31', 'Unity, C#, 3D Modeling');
 
-- Insert 10 allowances
INSERT INTO Allowances (allowance_id, allowance_type, amount,status)
VALUES
  ('AL000001', 'Transportation', 2000000.00, 'Active'),
  ('AL000002', 'Housing', 10000000.00, 'Active'),
  ('AL000003', 'Meal', 4000000.00, 'Active'),
  ('AL000004', 'Medical', 6000000.00, 'Active'),
  ('AL000005', 'Phone', 1000000.00, 'Active'),
  ('AL000006', 'Internet', 1500000.00, 'Active'),
  ('AL000007', 'Education', 5000000.00, 'Active'),
  ('AL000008', 'Travel', 3000000.00, 'Active'),
  ('AL000009', 'Fitness', 2000000.00, 'Active'),
  ('AL000010', 'Parking', 1000000.00, 'Active'),
  ('AL000011', 'Software Development Allowance', 500000.00, 'Active'),
  ('AL000012', 'Database Administration Allowance',  400000.00, 'Active'),
  ('AL000013', 'Network Operations Allowance',  600000.00, 'Active'),
  ('AL000014', 'IT Support Allowance',  300000.00, 'Active'),
  ('AL000015', 'Quality Assurance Allowance', 400000.00, 'Active'),
  ('AL000016', 'Human Resources Allowance',  200000.00, 'Active'),
  ('AL000017', 'Cybersecurity Allowance',  500000.00, 'Active'),
  ('AL000018', 'Business Analysis Allowance',  450000.00, 'Active');

INSERT INTO EmployeeBenefit (allowances_id, employee_id, allowance_id,status)
VALUES
  ('EB000001', 'EP000001', 'AL000001', 1),   -- Employee 1 has Transportation allowance
  ('EB000002', 'EP000001', 'AL000002', 1),   -- Employee 1 has Housing allowance
  ('EB000003', 'EP000001', 'AL000003', 1),   -- Employee 1 has Meal allowance
  ('EB000004', 'EP000002', 'AL000004', 1),   -- Employee 2 has Medical allowance
  ('EB000005', 'EP000002', 'AL000005', 1),   -- Employee 2 has Phone allowance
  ('EB000006', 'EP000003', 'AL000006', 1),   -- Employee 3 has Internet allowance
  ('EB000007', 'EP000004', 'AL000007', 1),   -- Employee 4 has Education allowance
  ('EB000008', 'EP000005', 'AL000008', 1),   -- Employee 5 has Travel allowance
  ('EB000009', 'EP000005', 'AL000009', 1),   -- Employee 5 has Fitness allowance
  ('EB000010', 'EP000005', 'AL000010', 1), -- Employee 5 has Parking allowance
  ('EB000011', 'EP000006', 'AL000001', 1),  -- Employee 6 has Transportation allowance
  ('EB000012', 'EP000006', 'AL000004', 1),  -- Employee 6 has Medical allowance
  ('EB000013', 'EP000007', 'AL000006', 1),  -- Employee 7 has Internet allowance
  ('EB000014', 'EP000007', 'AL000007', 1),  -- Employee 7 has Education allowance
  ('EB000015', 'EP000007', 'AL000009', 1),  -- Employee 7 has Fitness allowance
  ('EB000016', 'EP000007', 'AL000010', 1), -- Employee 7 has Parking allowance
  ('EB000017', 'EP000008', 'AL000017', 1), -- Employee 8 has no allowance
  ('EB000018', 'EP000001', 'AL000011', 1),  -- Employee 1 has Software Development Allowance
  ('EB000019', 'EP000002', 'AL000014', 1),  -- Employee 2 has IT Support Allowance
  ('EB000020', 'EP000003', 'AL000013', 1),  -- Employee 3 has Network Operations Allowance
  ('EB000021', 'EP000004', 'AL000012', 1),  -- Employee 4 has Database Administration Allowance
  ('EB000022', 'EP000005', 'AL000015', 1),  -- Employee 5 has Quality Assurance Allowance
  ('EB000023', 'EP000006', 'AL000016', 1),  -- Employee 6 has Human Resources Allowance
  ('EB000024', 'EP000007', 'AL000017', 1),  -- Employee 7 has Cybersecurity Allowance
  ('EB000025', 'EP000008', 'AL000008', 1); -- Employee 8 has no allowance
