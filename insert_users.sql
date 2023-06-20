use HRMS
INSERT INTO Roles(role_id, role_name)
VALUES 
(1, 'ADMIN'),
(2, 'HR_Manager'),
(3, 'HR_Staff'),
(4, 'Employee'); 

INSERT INTO Users (user_id, username, password, Email, role_id,status)
VALUES
  (1, N'Hồ', N'P@ssw0rd123!', 'ho.nguyen@example.com', 4, 'active'),
  (2, N'Mai', N'abc123', 'mai.tran@example.com', 4,'active'),
  (3, N'Thắm', N'Str0ngP@ssw0rd', 'tham.le@example.com', 4,'active'),
  (4, N'Nam', N'password123', 'nam.tran@example.com', 4,'active'),
  (5, N'Bình', N'123456789', 'binh.le@example.com', 4,'active'),
  (6, N'Lan', N'P@ssw0rd789!', 'lan.vu@example.com', 2,'active'),
  (7, N'Minh', N'abc123', 'minh.tran@example.com', 3,'active'),
  (8, N'An', N'Str0ngP@ssw0rd', 'an.le@example.com', 1,'active');

  delete from Employee
  delete from users
-- Insert 13 employees

INSERT INTO Employee (employee_id, first_name, last_name, employee_image, date_of_birth, employee_address, email, phone_number, BankAccountNumber, BankAccountName, BankName, experience_id, user_id, job_id, department_id,status)
VALUES
  (1, N'Hồ', N'Nguyễn', 'ho_nguyen.jpg', '1990-05-15', '123 Main St, City, Country', 'ho.nguyen@example.com', '+1234567890', '123456789', N'Hồ Nguyễn', 'Bank XYZ', 1, 1, 1, 1,'active'),
  (2, N'Mai', N'Trần', 'mai_tran.jpg', '1992-09-20', '456 Elm St, City, Country', 'mai.tran@example.com', '+0987654321', '987654321', N'Mai Trần', 'Bank ABC', 2, 2, 1, 1,'active'),
  (3, N'Thắm', N'Lê', 'tham_le.jpg', '1988-07-10', '789 Oak St, City, Country', 'tham.le@example.com', '+1357924680', '246813579', N'Thắm Lê', 'Bank DEF', 3, 3, 1, 1,'active'),
  (4, N'Bình', N'Lê', 'binh_le.jpg', '1991-11-05', '654 Cedar St, City, Country', 'binh.le@example.com', '+0123456789', '123456789', N'Bình Lê', 'Bank ABC', 4, 5, 4, 1,'active'),
  (5, N'Nam', N'Trần', 'nam_tran.jpg', '1995-03-25', '321 Pine St, City, Country', 'nam.tran@example.com', '+5678901234', '987654321', N'Nam Trần', 'Bank XYZ', 3, 4, 2, 2,'active'),
  (6, N'Lan', N'Vũ', 'lan_vu.jpg', '1993-12-30', '987 Maple St, City, Country', 'lan.vu@example.com', '+9876543210', '246813579', 'Lan Vũ', N'Bank DEF', 5, 6, 5, 6,'active'),
  (7, N'Minh', N'Trần', 'minh_tran.jpg', '1989-08-17', '147 Birch St, City, Country', 'minh.tran@example.com', '+5432109876', '987654321', N'Minh Trần', 'Bank XYZ', 5, 7, 5, 6,'active'),
  (8, N'An', N'Lê', 'an_le.jpg', '1994-06-12', '258 Walnut St, City, Country', 'an.le@example.com', '+7654321098', '123456789', N'An Lê', 'Bank ABC',12, 8, 12, 4,'active');

-- Insert 13 jobs
INSERT INTO Job (job_id, job_title, job_description, start_date, status, base_salary_per_hour, allowance_id, bonus)
VALUES
  (1, 'Software Engineer', N'Trách nhiệm phát triển ứng dụng phần mềm', '2023-01-01', 'Active', 1000000.00, 1, 100000000.00),
  (2, 'Data Analyst', N'Phân tích dữ liệu và cung cấp thông tin', '2023-02-01', 'Active', 900000.00, 2, 80000000.00),
  (3, 'Network Administrator', N'Quản lý và duy trì cơ sở hạ tầng mạng', '2023-03-01', 'Active', 1100000.00, 3, 120000000.00),
  (4, 'Web Developer', N'Trách nhiệm thiết kế và phát triển website', '2023-04-01', 'Active', 800000.00, 4, 60000000.00),
  (5, 'IT Project Manager', N'Lập kế hoạch, thực hiện và quản lý dự án IT', '2023-05-01', 'Active', 1200000.00, 5, 140000000.00),
  (6, 'Database Administrator', N'Quản lý và duy trì cơ sở dữ liệu', '2023-06-01', 'Active', 1000000.00, 6, 100000000.00),
  (7, 'Quality Assurance Engineer', N'Đảm bảo chất lượng phần mềm qua kiểm thử và phân tích', '2023-07-01', 'Active', 900000.00, 7, 80000000.00),
  (8, 'System Analyst', N'Phân tích và cải thiện hệ thống thông tin của công ty', '2023-08-01', 'Active', 1100000.00, 8, 120000000.00),
  (9, 'UI/UX Designer', N'Thiết kế giao diện người dùng và trải nghiệm người dùng', '2023-09-01', 'Active', 800000.00, 9, 60000000.00),
  (10, 'IT Support Specialist', N'Cung cấp hỗ trợ kỹ thuật cho người dùng cuối', '2023-10-01', 'Active', 1000000.00, 10, 100000000.00),
  (11, 'Business Analyst', N'Phân tích nhu cầu và xác định yêu cầu kinh doanh', '2023-11-01', 'Active', 950000.00, 7, 90000000.00),
  (12, 'IT Consultant', N'Tư vấn và đề xuất giải pháp công nghệ thông tin', '2023-12-01', 'Active', 1050000.00, 2, 110000000.00),
  (13, 'Cybersecurity Analyst', N'Phân tích và đảm bảo an ninh thông tin', '2024-01-01', 'Active', 950000.00, 6, 90000000.00);

  INSERT INTO Department (department_id, department_name, description)
VALUES
  (1, 'Software Development', 'Responsible for developing software applications and solutions.'),
  (2, 'Database Administration', 'Responsible for managing and maintaining databases.'),
  (3, 'Network Operations', 'Responsible for managing the company''s network infrastructure.'),
  (4, 'IT Support', 'Responsible for providing technical support to employees and resolving IT issues.'),
  (5, 'Quality Assurance', 'Responsible for testing and ensuring the quality of software and systems.'),
  (6, 'Human Resources', 'Responsible for managing HR processes and employee-related matters.'),
  (7, 'Cybersecurity', 'Responsible for protecting the company''s IT systems and data from security threats.'),
  (8, 'Business Analysis', 'Responsible for analyzing business requirements and recommending IT solutions.');

  INSERT INTO Experience (experience_id, name_project, team_size, start_date, end_date, tech_stack)
VALUES
  (1, 'Website Redesign', 6, '2021-11-15', '2022-03-31', 'HTML, CSS, JavaScript'),
  (2, 'Mobile App Development', 8, '2022-03-01', '2022-08-31', 'React Native, Firebase, TypeScript'),
  (3, 'Data Analytics Platform', 5, '2022-09-01', '2023-02-28', 'Python, SQL, Tableau'),
  (4, 'E-commerce Website Development', 8, '2022-02-15', '2022-07-31', 'PHP, Laravel, MySQL'),
  (5, 'Mobile App Development', 6, '2021-10-01', '2022-02-28', 'Swift, iOS, Firebase'),
  (6, 'Data Analysis and Visualization', 4, '2022-03-15', '2022-06-30', 'Python, Pandas, Matplotlib'),
  (7, 'UI/UX Design for Web Applications', 3, '2022-06-01', '2022-08-31', 'Sketch, Adobe XD, InVision'),
  (8, 'Blockchain Implementation', 5, '2021-12-01', '2022-04-30', 'Solidity, Ethereum, Truffle'),
  (9, 'Cloud Infrastructure Deployment', 10, '2022-01-15', '2022-09-30', 'AWS, Azure, Terraform'),
  (10, 'Machine Learning Model Development', 7, '2022-05-01', '2022-11-30', 'Python, scikit-learn, TensorFlow'),
  (11, 'CRM System Integration', 4, '2021-09-01', '2022-01-31', 'Java, Spring Boot, Salesforce'),
  (12, 'Chatbot Development', 3, '2022-03-01', '2022-05-31', 'Python, NLTK, Dialogflow'),
  (13, 'Game Development', 6, '2021-11-01', '2022-07-31', 'Unity, C#, 3D Modeling');
 
-- Insert 10 allowances
INSERT INTO Allowances (allowance_id, allowance_type, amount)
VALUES
  (1, 'Transportation', 2000000.00),
  (2, 'Housing', 10000000.00),
  (3, 'Meal', 4000000.00),
  (4, 'Medical', 6000000.00),
  (5, 'Phone', 1000000.00),
  (6, 'Internet', 1500000.00),
  (7, 'Education', 5000000.00),
  (8, 'Travel', 3000000.00),
  (9, 'Fitness', 2000000.00),
  (10, 'Parking', 1000000.00),
  (11, 'Software Development Allowance', 500000.00),
  (12, 'Database Administration Allowance',  400000.00),
  (13, 'Network Operations Allowance',  600000.00),
  (14, 'IT Support Allowance',  300000.00),
  (15, 'Quality Assurance Allowance', 400000.00),
  (16, 'Human Resources Allowance',  200000.00),
  (17, 'Cybersecurity Allowance',  500000.00),
  (18, 'Business Analysis Allowance',  450000.00);

INSERT INTO EmployeeBenefit (allowances_id, employee_id, allowance_id)
VALUES
  (1, 1, 1),   -- Employee 1 has Transportation allowance
  (2, 1, 2),   -- Employee 1 has Housing allowance
  (3, 1, 3),   -- Employee 1 has Meal allowance
  (4, 2, 4),   -- Employee 2 has Medical allowance
  (5, 2, 5),   -- Employee 2 has Phone allowance
  (6, 3, 6),   -- Employee 3 has Internet allowance
  (7, 4, 7),   -- Employee 4 has Education allowance
  (8, 5, 8),   -- Employee 5 has Travel allowance
  (9, 5, 9),   -- Employee 5 has Fitness allowance
  (10, 5, 10), -- Employee 5 has Parking allowance
  (11, 6, 1),  -- Employee 6 has Transportation allowance
  (12, 6, 4),  -- Employee 6 has Medical allowance
  (13, 7, 6),  -- Employee 7 has Internet allowance
  (14, 7, 7),  -- Employee 7 has Education allowance
  (15, 7, 9),  -- Employee 7 has Fitness allowance
  (16, 7, 10), -- Employee 7 has Parking allowance
  (17, 8, 17), -- Employee 8 has no allowance
  (18, 1, 11),  -- Employee 1 has Software Development Allowance
  (19, 2, 14),  -- Employee 2 has IT Support Allowance
  (20, 3, 13),  -- Employee 3 has Network Operations Allowance
  (21, 4, 12),  -- Employee 4 has Database Administration Allowance
  (22, 5, 15),  -- Employee 5 has Quality Assurance Allowance
  (23, 6, 16),  -- Employee 6 has Human Resources Allowance
  (24, 7, 17),  -- Employee 7 has Cybersecurity Allowance
  (25, 8, 8); -- Employee 8 has no allowance
