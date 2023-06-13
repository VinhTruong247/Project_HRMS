use HRMS
INSERT INTO Roles (role_id, role_name)
VALUES 
(1, 'ADMIN'),
(2, 'HR_Manager'),
(3, 'HR_Staff'),
(4, 'Employee');
INSERT INTO Users (user_id, username, password, Email, role_id)
VALUES
  (1, N'Hồ', N'P@ssw0rd123!', 'ho.nguyen@example.com', 1),
  (2, N'Mai', N'abc123', 'mai.tran@example.com', 4),
  (3, N'Thắm', N'Str0ngP@ssw0rd', 'tham.le@example.com', 4),
  (4, N'Nam', N'password123', 'nam.tran@example.com', 4),
  (5, N'Bình', N'123456789', 'binh.le@example.com', 4),
  (6, N'Lan', N'P@ssw0rd789!', 'lan.vu@example.com', 2),
  (7, N'Minh', N'abc123', 'minh.tran@example.com', 3),
  (8, N'An', N'Str0ngP@ssw0rd', 'an.le@example.com', 3),
  (9, N'Quốc', N'987654321', 'quoc.tran@example.com', 2),
  (10, N'Hà', N'password123', 'ha.le@example.com', 3),
  (11, N'Đạt', N'P@ssw0rd456!', 'dat.tran@example.com', 4),
  (12, N'Hương', N'abc123', 'huong.le@example.com', 1),
  (13, N'Long', N'Str0ngP@ssw0rd', 'long.tran@example.com', 4);

  select * from  Allowances
  select * from Employee
  DELETE FROM Employee WHERE Employee.employee_id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);

-- Insert 13 employees
INSERT INTO Employee (employee_id, first_name, last_name, employee_image, date_of_birth, employee_address, email, phone_number, BankAccountNumber, BankAccountName, BankName, experience_id, user_id, job_id, department_id)
VALUES
  (1, N'Hò', N'Nguyễn', 'ho_nguyen.jpg', '1990-05-15', N'123 Đường Mai, Thành phố, Quốc gia', 'ho.nguyen@example.com', '1234567890', 123456780, N'Hồ Nguyễn', 'Vietcombank', 1, 1, 1, 1),
  (2, N'Mai', N'Trần', 'mai_tran.jpg', '1992-09-23', N'456 Đường Lan, Thành phố, Quốc gia', 'mai.tran@example.com', '9876543210', 987654321, N'Mai Trần', 'Vietcombank', 2, 2, 2, 2),
  (3, N'Thắm', N'Lê', 'tham_le.jpg', '1985-12-10', N'789 Đường Cây Sấu, Thành phố, Quốc gia', 'tham.le@example.com', '3456789012', 345678902, N'Thắm Lê', 'Vietcombank', 3, 3, 3, 3),
  (4, N'Nam', N'Trần', 'nam_tran.jpg', '1991-06-28', N'567 Đường Phong Lan, Thành phố, Quốc gia', 'nam.tran@example.com', '7890123456', 789012343, N'Nam Trần', 'Vietcombank', 4, 4, 4, 4),
  (5, N'Bình', N'Lê', 'binh_le.jpg', '1993-03-05', N'890 Đường Cây Điệp, Thành phố, Quốc gia', 'binh.le@example.com', '2345678901', 234567895, N'Bình Lê', 'Vietcombank', 5, 5, 5, 5),
  (6, N'Lan', N'Vũ', 'lan_vu.jpg', '1994-07-19', N'345 Đường Đào, Thành phố, Quốc gia', 'lan.vu@example.com', '6789012345', 678901232, N'Lan Vũ', 'Vietcombank', 6, 6, 6, 6),
  (7, N'Minh', N'Trần', 'minh_tran.jpg', '1988-02-14', N'678 Đường Ôn, Thành phố, Quốc gia', 'minh.tran@example.com', '9012345678', 901234566, N'Minh Trần', 'Vietcombank', 7, 7, 7, 7),
  (8, N'An', N'Lê', 'an_le.jpg', '1990-11-30', N'123 Đường Dứa, Thành phố, Quốc gia', 'an.le@example.com', '1234509876', 123450987, N'An Lê', 'Vietcombank', 8, 8, 8, 8),
  (9, N'Quốc', N'Trần', 'quoc_tran.jpg', '1993-08-18', N'456 Đường Xoài, Thành phố, Quốc gia', 'quoc.tran@example.com', '9876543210', 987654322, N'Quốc Trần', 'Vietcombank', 9, 9, 9, 9),
  (10, N'Hà', N'Lê', 'ha_le.jpg', '1991-04-02', N'789 Đường Chuối, Thành phố, Quốc gia', 'ha.le@example.com', '2345678901', 234567896, N'Hà Lê', 'Vietcombank', 10, 10, 10, 10),
  (11, N'Đạt', N'Trần', 'dat_tran.jpg', '1987-10-12', N'567 Đường Cam, Thành phố, Quốc gia', 'dat.tran@example.com', '6789012345', 678901233, N'Đạt Trần', 'Vietcombank', 11, 11, 11, 3),
  (12, N'Hương', N'Lê', 'huong_le.jpg', '1995-01-25', N'890 Đường Anh Đào, Thành phố, Quốc gia', 'huong.le@example.com', '9012345678', 901234568, N'Hương Lê', 'Vietcombank', 12, 12, 12, 7),
  (13, N'Long', N'Trần', 'long_tran.jpg', '1989-07-08', N'345 Đường Nho, Thành phố, Quốc gia', 'long.tran@example.com', '3456789012', 345678903, N'Long Trần', 'Vietcombank', 13, 13, 13, 10);



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
  (10, 'Parking', 1000000.00);


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
  (6, 'Project Management', 'Responsible for managing IT projects and ensuring their successful delivery.'),
  (7, 'Cybersecurity', 'Responsible for protecting the company''s IT systems and data from security threats.'),
  (8, 'Business Analysis', 'Responsible for analyzing business requirements and recommending IT solutions.'),
  (9, 'Data Science', 'Responsible for analyzing and deriving insights from data for business decision-making.'),
  (10, 'IT Infrastructure', 'Responsible for managing the company''s IT infrastructure and hardware components.');