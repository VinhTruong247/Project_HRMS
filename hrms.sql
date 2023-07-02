ALTER TABLE Department
ADD status NVARCHAR(10) DEFAULT 'Active';

UPDATE Department
SET status = 'Active';