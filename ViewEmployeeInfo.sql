SELECT
  e.employee_id,
  e.first_name,
  e.last_name,
  e.employee_image,
  e.date_of_birth,
  e.employee_address,
  e.email,
  e.phone_number,
  e.BankAccountNumber,
  e.BankAccountName,
  e.BankName,
  e.user_id,
  u.role_id,
  r.role_name,
  d.department_name,
  j.job_title,
  ex.name_project,
  ex.team_size,
  ex.start_date,
  ex.end_date,
  ex.tech_stack
FROM
  Employee e
  JOIN Department d ON e.department_id = d.department_id
  JOIN Job j ON e.job_id = j.job_id
  JOIN Experience ex ON e.experience_id = ex.experience_id
  JOIN Users u ON e.user_id = u.user_id
  JOIN Roles r ON u.role_id = r.role_id;
