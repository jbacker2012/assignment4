--Part 1
SELECT COLUMN_name, data_type  
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE       
TABLE_NAME = 'jobs'

--Part 2
SELECT Name 
FROM techjobs.employers 
WHERE
Location = 'st. Louis city'

--Part 3
SELECT Name, Description
FROM techjobs.skills 
WHERE Name IS NOT NULL
ORDER BY Name ASC;

