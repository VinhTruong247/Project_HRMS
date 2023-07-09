use HRMS
delete from Attendance
delete from Project
delete from EmployeeBenefit
delete from Experience
delete from Users
delete from Employee
delete from Department
delete from Job
delete from Allowances
delete from Roles

INSERT INTO Permission (permission_id, permission_des, permission_displayName, status)
VALUES
  ('PM000001', 'Can access employee information', 'Access Employee Information', 1),
  ('PM000002', 'Can manage employee records', 'Manage Employee Records', 1),
  ('PM000003', 'Can view salary details', 'View Salary Details', 1),
  ('PM000004', 'Can update employee attendance', 'Update Employee Attendance', 1),
  ('PM000005', 'Can generate reports', 'Generate Reports', 1);


INSERT INTO Roles (role_id, role_name, status)
VALUES 
  ('RL000001', 'ADMIN', 1),
  ('RL000002', 'HR_Manager', 1),
  ('RL000003', 'HR_Staff', 1),
  ('RL000004', 'Employee', 1);
-- Insert 10 allowances
INSERT INTO Allowances (allowance_id, allowance_type, amount,status)
VALUES
  ('AL000001', 'Transportation', 2000000.00, 1),
  ('AL000002', 'Housing', 10000000.00,1),
  ('AL000003', 'Meal', 4000000.00, 1),
  ('AL000004', 'Medical', 6000000.00, 1),
  ('AL000005', 'Phone', 1000000.00, 1),
  ('AL000006', 'Internet', 1500000.00, 1),
  ('AL000007', 'Education', 5000000.00, 1),
  ('AL000008', 'Travel', 3000000.00, 1),
  ('AL000009', 'Fitness', 2000000.00, 1),
  ('AL000010', 'Parking', 1000000.00, 1),
  ('AL000011', 'Software Development Allowance', 500000.00, 1),
  ('AL000012', 'Database Administration Allowance',  400000.00, 1),
  ('AL000013', 'Network Operations Allowance',  600000.00, 1),
  ('AL000014', 'IT Support Allowance',  300000.00, 1),
  ('AL000015', 'Quality Assurance Allowance', 400000.00, 1),
  ('AL000016', 'Human Resources Allowance',  200000.00, 1),
  ('AL000017', 'Cybersecurity Allowance',  500000.00, 1),
  ('AL000018', 'Business Analysis Allowance',  450000.00, 1);
-- Insert 13 jobs
INSERT INTO Job (job_id, job_title, job_description, start_date, status, base_salary_per_hour, bonus)
VALUES
  ('JB000001', 'Software Engineer', N'Trách nhiệm phát triển ứng dụng phần mềm', '2023-01-01', 1, 1000000.00, 100000000.00),
  ('JB000002', 'Data Analyst', N'Phân tích dữ liệu và cung cấp thông tin', '2023-02-01',1, 900000.00, 80000000.00),
  ('JB000003', 'Network Administrator', N'Quản lý và duy trì cơ sở hạ tầng mạng', '2023-03-01',1, 1100000.00, 120000000.00),
  ('JB000004', 'Web Developer', N'Trách nhiệm thiết kế và phát triển website', '2023-04-01',1, 800000.00, 60000000.00),
  ('JB000005', 'IT Project Manager', N'Lập kế hoạch, thực hiện và quản lý dự án IT', '2023-05-01',1, 1200000.00, 140000000.00),
  ('JB000006', 'Database Administrator', N'Quản lý và duy trì cơ sở dữ liệu', '2023-06-01',1, 1000000.00, 100000000.00),
  ('JB000007', 'Quality Assurance Engineer', N'Đảm bảo chất lượng phần mềm qua kiểm thử và phân tích', '2023-07-01', 1, 900000.00, 80000000.00),
  ('JB000008', 'System Analyst', N'Phân tích và cải thiện hệ thống thông tin của công ty', '2023-08-01', 1, 1100000.00, 120000000.00),
  ('JB000009', 'UI/UX Designer', N'Thiết kế giao diện người dùng và trải nghiệm người dùng', '2023-09-01', 1, 800000.00, 60000000.00),
  ('JB000010', 'IT Support Specialist', N'Cung cấp hỗ trợ kỹ thuật cho người dùng cuối', '2023-10-01', 1, 1000000.00, 100000000.00),
  ('JB000011', 'Business Analyst', N'Phân tích nhu cầu và xác định yêu cầu kinh doanh', '2023-11-01', 1, 950000.00, 90000000.00),
  ('JB000012', 'IT Consultant', N'Tư vấn và đề xuất giải pháp công nghệ thông tin', '2023-12-01', 1, 1050000.00, 110000000.00),
  ('JB000013', 'Cybersecurity Analyst', N'Phân tích và đảm bảo an ninh thông tin', '2024-01-01',1, 950000.00, 90000000.00);
  INSERT INTO Department (department_id, department_name, description ,status)
VALUES
  ('DP000001', 'Software Development', 'Responsible for developing software applications and solutions.', 1),
  ('DP000002', 'Database Administration', 'Responsible for managing and maintaining databases.', 1),
  ('DP000003', 'Network Operations', 'Responsible for managing the company''s network infrastructure.', 1),
  ('DP000004', 'IT Support', 'Responsible for providing technical support to employees and resolving IT issues.', 1),
  ('DP000005', 'Quality Assurance', 'Responsible for testing and ensuring the quality of software and systems.', 1),
  ('DP000006', 'Human Resources', 'Responsible for managing HR processes and employee-related matters.', 1),
  ('DP000007', 'Cybersecurity', 'Responsible for protecting the company''s IT systems and data from security threats.', 1),
  ('DP000008', 'Business Analysis', 'Responsible for analyzing business requirements and recommending IT solutions.', 1);
-- Insert 13 employees
INSERT INTO Employee (employee_id, first_name, last_name, employee_image, date_of_birth, employee_address, email, phone_number, BankAccountNumber, BankAccountName, BankName, job_id, department_id,status)
VALUES
  ('EP000001', N'Hồ', N'Nguyễn', 'ho_nguyen.jpg', '1990-05-15', '123 Main St, City, Country', 'ho.nguyen@example.com', '+1234567890', '123456789', N'Hồ Nguyễn', 'Bank XYZ', 'JB000001', 'DP000001',1),
  ('EP000002', N'Mai', N'Trần', 'mai_tran.jpg', '1992-09-20', '456 Elm St, City, Country', 'mai.tran@example.com', '+0987654321', '987654321', N'Mai Trần', 'Bank ABC','JB000001', 'DP000001',1),
  ('EP000003', N'Thắm', N'Lê', 'tham_le.jpg', '1988-07-10', '789 Oak St, City, Country', 'tham.le@example.com', '+1357924680', '246813579', N'Thắm Lê', 'Bank DEF','JB000001', 'DP000001',1),
  ('EP000005', N'Bình', N'Lê', 'binh_le.jpg', '1991-11-05', '654 Cedar St, City, Country', 'binh.le@example.com', '+0123456789', '123456789', N'Bình Lê', 'Bank ABC','JB000004', 'DP000001',1),
  ('EP000004', N'Nam', N'Trần', 'nam_tran.jpg', '1995-03-25', '321 Pine St, City, Country', 'nam.tran@example.com', '+5678901234', '987654321', N'Nam Trần', 'Bank XYZ','JB000002', 'DP000002',1),
  ('EP000006', N'Lan', N'Vũ', 'lan_vu.jpg', '1993-12-30', '987 Maple St, City, Country', 'lan.vu@example.com', '+9876543210', '246813579', 'Lan Vũ', N'Bank DEF','JB000005', 'DP000006',1),
  ('EP000007', N'Minh', N'Trần', 'minh_tran.jpg', '1989-08-17', '147 Birch St, City, Country', 'minh.tran@example.com', '+5432109876', '987654321', N'Minh Trần', 'Bank XYZ','JB000005', 'DP000006',1),
  ('EP000008', N'An', N'Lê', 'an_le.jpg', '1994-06-12', '258 Walnut St, City, Country', 'an.le@example.com', '+7654321098', '123456789', N'An Lê', 'Bank ABC','JB000012', 'DP000004',1);
INSERT INTO Users (user_id,employee_id , username, password, Email, role_id, status)
VALUES
  ('US000001','EP000001', N'Ho', N'a123!', 'ho.nguyen@example.com', 'RL000004', 1),
  ('US000002','EP000002', N'Mai', N'abc123', 'mai.tran@example.com', 'RL000004', 1),
  ('US000003','EP000003', N'Tham', N'Str0ngP@ssw0rd', 'tham.le@example.com', 'RL000004', 1),
  ('US000004','EP000004', N'Nam', N'password123', 'nam.tran@example.com', 'RL000004', 1),
  ('US000005','EP000005', N'Binh', N'123456789', 'binh.le@example.com', 'RL000004', 1),
  ('US000006','EP000006', N'Lan', N'P@ssw0rd789!', 'lan.vu@example.com', 'RL000002', 1),
  ('US000007','EP000007', N'Minh', N'abc123', 'minh.tran@example.com', 'RL000003', 1),
  ('US000008','EP000008', N'An', N'Str0ngP@ssw0rd', 'an.le@example.com', 'RL000001', 1);

INSERT INTO Experience (experience_id, employee_id, name_project, team_size, start_date, end_date, tech_stack, status)
VALUES
  ('EX000001','EP000001', 'Website Redesign', 6, '2021-11-15', '2022-03-31', 'HTML, CSS, JavaScript', 1),
  ('EX000002','EP000002', 'Mobile App Development', 8, '2022-03-01', '2022-08-31', 'React Native, Firebase, TypeScript', 1),
  ('EX000003','EP000003', 'Data Analytics Platform', 5, '2022-09-01', '2023-02-28', 'Python, SQL, Tableau', 1),
  ('EX000004','EP000004', 'E-commerce Website Development', 8, '2022-02-15', '2022-07-31', 'PHP, Laravel, MySQL', 1),
  ('EX000005','EP000005', 'Mobile App Development', 6, '2021-10-01', '2022-02-28', 'Swift, iOS, Firebase',1),
  ('EX000006','EP000006', 'Data Analysis and Visualization', 4, '2022-03-15', '2022-06-30', 'Python, Pandas, Matplotlib',1),
  ('EX000007','EP000007', 'UI/UX Design for Web Applications', 3, '2022-06-01', '2022-08-31', 'Sketch, Adobe XD, InVision',1),
  ('EX000008','EP000008', 'Blockchain Implementation', 5, '2021-12-01', '2022-04-30', 'Solidity, Ethereum, Truffle',1),
  ('EX000009','EP000001', 'Cloud Infrastructure Deployment', 10, '2022-01-15', '2022-09-30', 'AWS, Azure, Terraform',1),
  ('EX000010','EP000002', 'Machine Learning Model Development', 7, '2022-05-01', '2022-11-30', 'Python, scikit-learn, TensorFlow',1),
  ('EX000011','EP000003', 'CRM System Integration', 4, '2021-09-01', '2022-01-31', 'Java, Spring Boot, Salesforce',1),
  ('EX000012','EP000004', 'Chatbot Development', 3, '2022-03-01', '2022-05-31', 'Python, NLTK, Dialogflow',1),
  ('EX000013','EP000005', 'Game Development', 6, '2021-11-01', '2022-07-31', 'Unity, C#, 3D Modeling',1);

INSERT INTO EmployeeBenefit (employeebenefit_id, employee_id, allowance_id,status)
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

-- Inserting Attendance table
INSERT INTO Attendance (attendance_id,employee_id, day, time_in, time_out, late_hours, early_leave_hours, total_hours, attendance_status, notes)
VALUES
  ('AT000001','EP000001', '2023-07-01', '08:00:00', '17:00:00', '00:00:00', '00:00:00', '09:00:00', 1, 'Regular working hours'),
  ('AT000002','EP000002', '2023-07-01', '08:15:00', '17:15:00', '00:15:00', '00:00:00', '09:00:00', 1, 'Arrived slightly late'),
  ('AT000003','EP000003', '2023-07-01', '08:30:00', '17:30:00', '00:30:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000004','EP000004', '2023-07-01', '08:45:00', '17:45:00', '00:45:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000005','EP000005', '2023-07-02', '09:00:00', '18:00:00', '01:00:00', '00:00:00', '09:00:00', 1, 'Regular working hours'),
  ('AT000006','EP000006', '2023-07-02', '09:15:00', '18:15:00', '01:15:00', '00:00:00', '09:00:00', 1, 'Arrived slightly late'),
  ('AT000007','EP000007', '2023-07-02', '09:30:00', '18:30:00', '01:30:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000008','EP000008', '2023-07-02', '09:45:00', '18:45:00', '01:45:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000009','EP000001', '2023-07-03', '10:00:00', '19:00:00', '02:00:00', '00:00:00', '09:00:00', 1, 'Regular working hours'),
  ('AT000010','EP000002', '2023-07-03', '10:15:00', '19:15:00', '02:15:00', '00:00:00', '09:00:00', 1, 'Arrived slightly late'),
  ('AT000011','EP000003', '2023-07-03', '10:30:00', '19:30:00', '02:30:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000012','EP000004', '2023-07-03', '10:45:00', '19:45:00', '02:45:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000013','EP000005', '2023-07-04', '11:00:00', '20:00:00', '03:00:00', '00:00:00', '09:00:00', 1, 'Regular working hours'),
  ('AT000014','EP000006', '2023-07-04', '11:15:00', '20:15:00', '03:15:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000015','EP000007', '2023-07-04', '11:30:00', '20:30:00', '03:30:00', '00:00:00', '09:00:00', 1, 'Arrived late'),
  ('AT000016','EP000008', '2023-07-04', '11:45:00', '20:45:00', '03:45:00', '00:00:00', '09:00:00', 1, 'Arrived late');
<<<<<<< Updated upstream

-- Inserting Project table
INSERT INTO Project (project_id, project_name, department_id, start_date, end_date, status)
VALUES
  ('PJ000001', 'Software Application Development', 'DP000001', '2020-07-01', '2023-08-31', 'In Progress'),
  ('PJ000002', 'Database Migration Project', 'DP000002', '2020-07-01', '2020-09-30', 'Completed'),
  ('PJ000003', 'Network Infrastructure Upgrade', 'DP000003', '2020-07-01', '2020-08-31', 'On Hold'),
  ('PJ000004', 'IT Helpdesk Enhancement', 'DP000004', '2020-07-01', '2023-08-31', 'In Progress'),
  ('PJ000005', 'Quality Assurance Automation', 'DP000005', '2020-07-01', '2023-09-30', 'In Progress'),
  ('PJ000006', 'Employee Onboarding System', 'DP000006', '2020-07-01', '2020-08-31', 'On Hold'),
  ('PJ000007', 'Cybersecurity Audit and Remediation', 'DP000007', '2020-07-01', '2020-09-30', 'Completed'),
  ('PJ000008', 'Business Process Optimization', 'DP000008', '2020-07-01', '2023-08-31', 'In Progress'),
  ('PJ000009', 'Mobile App Development', 'DP000001', '2020-07-01', '2020-09-30', 'Completed'),
  ('PJ000010', 'Data Analytics Platform', 'DP000002', '2020-07-01', '2020-08-31', 'Completed'),
  ('PJ000011', 'Network Security Enhancement', 'DP000003', '2020-07-01', '2020-09-30', 'Cancelled'),
  ('PJ000012', 'IT Service Desk Upgrade', 'DP000004', '2020-07-01', '2020-08-31', 'Completed');

  INSERT INTO Skill (skill_id, skill_name, skill_description, status)
VALUES
  ('SK000001', 'Java', 'Java programming language', 1),
  ('SK000002', 'Python', 'Python programming language', 1),
  ('SK000003', 'JavaScript', 'JavaScript programming language', 1),
  ('SK000004', 'SQL', 'Structured Query Language', 1),
  ('SK000005', 'HTML', 'Hypertext Markup Language', 1),
  ('SK000006', 'CSS', 'Cascading Style Sheets', 1),
  ('SK000007', 'React', 'React JavaScript library', 1),
  ('SK000008', 'Angular', 'Angular JavaScript framework', 1),
  ('SK000009', 'Node.js', 'Node.js runtime environment', 1),
  ('SK000010', 'PHP', 'PHP programming language', 1),
  ('SK000011', 'Laravel', 'Laravel PHP framework', 1),
  ('SK000012', 'MySQL', 'MySQL relational database management system', 1),
  ('SK000013', 'MongoDB', 'MongoDB document-oriented database', 1),
  ('SK000014', 'AWS', 'Amazon Web Services', 1),
  ('SK000015', 'Azure', 'Microsoft Azure cloud computing service', 1),
  ('SK000016', 'Docker', 'Docker containerization platform', 1),
  ('SK000017', 'Kubernetes', 'Kubernetes container orchestration platform', 1),
  ('SK000018', 'Git', 'Version control system', 1),
  ('SK000019', 'Jenkins', 'Jenkins automation server', 1),
  ('SK000020', 'Agile', 'Agile software development methodology', 1),
  ('SK000021', 'Ruby', 'Ruby programming language', 1),
  ('SK000022', 'Go', 'Go programming language', 1),
  ('SK000023', 'Swift', 'Swift programming language', 1),
  ('SK000024', 'Docker', 'Docker containerization platform', 1),
  ('SK000025', 'Kubernetes', 'Kubernetes container orchestration platform', 1),
  ('SK000026', 'Git', 'Version control system', 1),
  ('SK000027', 'Jenkins', 'Jenkins automation server', 1),
  ('SK000028', 'Agile', 'Agile software development methodology', 1),
  ('SK000029', 'Scrum', 'Scrum framework for agile development', 1),
  ('SK000030', 'DevOps', 'Software development methodology combining development and operations', 1);

-- Inserting Project table
INSERT INTO Project (project_id, project_name, department_id, start_date, end_date, status)
VALUES
  ('PJ000001', 'Software Application Development', 'DP000001', '2020-07-01', '2023-08-31', 'In Progress'),
  ('PJ000002', 'Database Migration Project', 'DP000002', '2020-07-01', '2020-09-30', 'Completed'),
  ('PJ000003', 'Network Infrastructure Upgrade', 'DP000003', '2020-07-01', '2020-08-31', 'On Hold'),
  ('PJ000004', 'IT Helpdesk Enhancement', 'DP000004', '2020-07-01', '2023-08-31', 'In Progress'),
  ('PJ000005', 'Quality Assurance Automation', 'DP000005', '2020-07-01', '2023-09-30', 'In Progress'),
  ('PJ000006', 'Employee Onboarding System', 'DP000006', '2020-07-01', '2020-08-31', 'On Hold'),
  ('PJ000007', 'Cybersecurity Audit and Remediation', 'DP000007', '2020-07-01', '2020-09-30', 'Completed'),
  ('PJ000008', 'Business Process Optimization', 'DP000008', '2020-07-01', '2023-08-31', 'In Progress'),
  ('PJ000009', 'Mobile App Development', 'DP000001', '2020-07-01', '2020-09-30', 'Completed'),
  ('PJ000010', 'Data Analytics Platform', 'DP000002', '2020-07-01', '2020-08-31', 'Completed'),
  ('PJ000011', 'Network Security Enhancement', 'DP000003', '2020-07-01', '2020-09-30', 'Cancelled'),
  ('PJ000012', 'IT Service Desk Upgrade', 'DP000004', '2020-07-01', '2020-08-31', 'Completed');

  INSERT INTO Skill (skill_id, skill_name, skill_description, status)
VALUES
  ('SK000001', 'Java', 'Java programming language', 1),
  ('SK000002', 'Python', 'Python programming language', 1),
  ('SK000003', 'JavaScript', 'JavaScript programming language', 1),
  ('SK000004', 'SQL', 'Structured Query Language', 1),
  ('SK000005', 'HTML', 'Hypertext Markup Language', 1),
  ('SK000006', 'CSS', 'Cascading Style Sheets', 1),
  ('SK000007', 'React', 'React JavaScript library', 1),
  ('SK000008', 'Angular', 'Angular JavaScript framework', 1),
  ('SK000009', 'Node.js', 'Node.js runtime environment', 1),
  ('SK000010', 'PHP', 'PHP programming language', 1),
  ('SK000011', 'Laravel', 'Laravel PHP framework', 1),
  ('SK000012', 'MySQL', 'MySQL relational database management system', 1),
  ('SK000013', 'MongoDB', 'MongoDB document-oriented database', 1),
  ('SK000014', 'AWS', 'Amazon Web Services', 1),
  ('SK000015', 'Azure', 'Microsoft Azure cloud computing service', 1),
  ('SK000016', 'Docker', 'Docker containerization platform', 1),
  ('SK000017', 'Kubernetes', 'Kubernetes container orchestration platform', 1),
  ('SK000018', 'Git', 'Version control system', 1),
  ('SK000019', 'Jenkins', 'Jenkins automation server', 1),
  ('SK000020', 'Agile', 'Agile software development methodology', 1),
  ('SK000021', 'Ruby', 'Ruby programming language', 1),
  ('SK000022', 'Go', 'Go programming language', 1),
  ('SK000023', 'Swift', 'Swift programming language', 1),
  ('SK000024', 'Docker', 'Docker containerization platform', 1),
  ('SK000025', 'Kubernetes', 'Kubernetes container orchestration platform', 1),
  ('SK000026', 'Git', 'Version control system', 1),
  ('SK000027', 'Jenkins', 'Jenkins automation server', 1),
  ('SK000028', 'Agile', 'Agile software development methodology', 1),
  ('SK000029', 'Scrum', 'Scrum framework for agile development', 1),
  ('SK000030', 'DevOps', 'Software development methodology combining development and operations', 1);

  -- Inserting GrantedPermission table
INSERT INTO GrantedPermission (role_id, permission_id, status)
VALUES
  ('RL000001', 'PM000001', 1),  -- ADMIN has access to all permissions
  ('RL000002', 'PM000002', 1),  -- HR_Manager has access to HR permissions
  ('RL000002', 'PM000003', 1),
  ('RL000002', 'PM000004', 1),
  ('RL000003', 'PM000003', 1),  -- HR_Staff has access to HR permissions
  ('RL000003', 'PM000004', 1),
  ('RL000004', 'PM000005', 1);  -- Employee has access to basic permissions
  
-- Inserting detail_tax_income table
INSERT INTO detail_tax_income (detail_tax_income_id, muc_chiu_thue, thue_suat, status)
VALUES
  ('DTI000009', 0, 5, 1),
  ('DTI000010', 5000000, 10, 1),
  ('DTI000011', 10000000, 15, 1),
  ('DTI000012', 18000000, 20, 1),
  ('DTI000013', 32000000, 25, 1);


  INSERT INTO Report (report_id, employee_id, reason, content, issue_date, status)
VALUES
  ('RP000001', 'EP000001', 'Request for leave', 'I would like to request a leave of absence for personal reasons.', '2023-07-05', 1),
  ('RP000002', 'EP000002', 'Expense reimbursement', 'I have attached the receipts for the business expenses I incurred during the trip.', '2023-07-06', 1);


  INSERT INTO DepartmentMemberList (department_id, employee_id, emp_role, status)
VALUES
  ('DP000001', 'EP000001', 'Team Lead', 1),
  ('DP000001', 'EP000002', 'Senior Developer', 1),
  ('DP000001', 'EP000003', 'Developer', 1),
  ('DP000001', 'EP000004', 'Developer', 1),
  ('DP000002', 'EP000005', 'Database Administrator', 1),
  ('DP000002', 'EP000006', 'Database Administrator', 1),
  ('DP000002', 'EP000007', 'Database Administrator', 1),
  ('DP000003', 'EP000008', 'Network Administrator', 1);

  INSERT INTO EmployeeContract (contract_id, employee_id, contract_file, start_date, end_date, job, base_salary, status, percent_deduction, salary_type, contract_type)
VALUES
  ('CN000001', 'EP000001', 'contract_file1.pdf', '2022-01-01', '2022-12-31', 'Software Engineer', 1000000.00, 1, 0.05, 'Monthly', 'Full-time'),
  ('CN000002', 'EP000002', 'contract_file2.pdf', '2022-03-15', '2022-12-31', 'Software Engineer', 1000000.00, 1, 0.05, 'Monthly', 'Full-time'),
  ('CN000003', 'EP000003', 'contract_file3.pdf', '2022-02-10', '2022-12-31', 'Software Engineer', 1000000.00, 1, 0.05, 'Monthly', 'Full-time'),
  ('CN000004', 'EP000005', 'contract_file4.pdf', '2022-04-20', '2022-12-31', 'Web Developer', 800000.00, 1, 0.05, 'Monthly', 'Full-time'),
  ('CN000005', 'EP000004', 'contract_file5.pdf', '2022-05-01', '2022-12-31', 'Data Analyst', 900000.00, 1, 0.05, 'Monthly', 'Full-time'),
  ('CN000006', 'EP000006', 'contract_file6.pdf', '2022-06-15', '2022-12-31', 'IT Project Manager', 1200000.00, 1, 0.05, 'Monthly', 'Full-time'),
  ('CN000007', 'EP000007', 'contract_file7.pdf', '2022-07-01', '2022-12-31', 'IT Project Manager', 1200000.00, 1, 0.05, 'Monthly', 'Full-time'),
  ('CN000008', 'EP000008', 'contract_file8.pdf', '2022-08-20', '2022-12-31', 'UI/UX Designer', 800000.00, 1, 0.05, 'Monthly', 'Full-time');

  INSERT INTO PaySlip (payslip_id, employee_id, pay_period, paid_date, base_salary, ot_hours, contract_id, standard_work_hours, actual_work_hours, tax_income, bonus, total_salary, note, BankAccountNumber, BankAccountName, BankName, approval, status)
VALUES
  ('PS000001', 'EP000001', 'June 2023', '2023-07-01', 1000000.00, '20:00:00', 'CN000001', '08:00:00', '08:30:00', 200000.00, 500000.00, 1500000.00, 'Received a bonus for outstanding performance.', 123456789, N'Hồ Nguyễn', 'Bank XYZ', 'Approved', 'Paid'),
  ('PS000002', 'EP000002', 'June 2023', '2023-07-01', 1000000.00, '15:30:00', 'CN000002', '08:00:00', '07:45:00', 150000.00, 300000.00, 1350000.00, 'Overtime hours were reduced due to completion of a project ahead of schedule.', 987654321, N'Mai Trần', 'Bank ABC', 'Approved', 'Paid'),
  ('PS000003', 'EP000003', 'June 2023', '2023-07-01', 1000000.00, '08:30:00', 'CN000003', '08:00:00', '08:45:00', 250000.00, 700000.00, 2250000.00, 'Received a tax refund for overpayment in the previous month.', 246813579, N'Thắm Lê', 'Bank DEF', 'Approved', 'Paid'),
  ('PS000004', 'EP000005', 'June 2023', '2023-07-01', 800000.00, '10:00:00', 'CN000004', '08:00:00', '08:15:00', 100000.00, 200000.00, 900000.00, 'Base salary was adjusted due to a promotion.', 123456789, N'Bình Lê', 'Bank ABC', 'Approved', 'Paid'),
  ('PS000005', 'EP000004', 'June 2023', '2023-07-01', 900000.00, '18:00:00', 'CN000005', '08:00:00', '08:30:00', 180000.00, 400000.00, 1220000.00, 'Earned a performance bonus for achieving sales targets.', 987654321, N'Nam Trần', 'Bank XYZ', 'Approved', 'Paid'),
  ('PS000006', 'EP000006', 'June 2023', '2023-07-01', 1200000.00, '22:30:00', 'CN000006', '08:00:00', '08:45:00', 225000.00, 800000.00, 2175000.00, 'Received a pay raise as part of the annual salary review.', 246813579, N'Lan Vũ', 'Bank DEF', 'Approved', 'Paid'),
  ('PS000007', 'EP000007', 'June 2023', '2023-07-01', 1200000.00, '17:30:00', 'CN000007', '08:00:00', '08:30:00', 175000.00, 350000.00, 1225000.00, 'Base salary adjusted due to a change in job responsibilities.', 987654321, N'Minh Trần', 'Bank XYZ', 'Approved', 'Paid'),
  ('PS000008', 'EP000008', 'June 2023', '2023-07-01', 800000.00, '12:00:00', 'CN000008', '08:00:00', '08:15:00', 120000.00, 250000.00, 970000.00, 'Received a one-time performance bonus for completing a challenging project.', 123456789, N'An Lê', 'Bank ABC', 'Approved', 'Paid');

select * from EmployeeBenefit
select * from Experience
select * from Users
select * from Employee
select * from Department
select * from Job
select * from Allowances
select * from Roles
select * from Attendance
select * from Project
select * from Skill
select * from GrantedPermission
select * from detail_tax_income
select * from Report
select * from DepartmentMemberList
select * from EmployeeContract
select * from PaySlip