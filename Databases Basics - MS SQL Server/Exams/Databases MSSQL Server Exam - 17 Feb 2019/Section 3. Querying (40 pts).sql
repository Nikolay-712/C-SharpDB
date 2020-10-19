-- 05. Teen Students 

SELECT s.FirstName, s.LastName, s.Age FROM Students AS s
WHERE s.Age >= 12
ORDER BY s.FirstName, s.LastName,s.Age

--06. Cool Addresses 

SELECT CONCAT
	(s.FirstName, ' ',s.MiddleName, ' ' , s.LastName) as 'Full Name' , s.[Address] as 'Address' 
FROM Students AS s WHERE 
	CHARINDEX('road', s.Address) > 0 
	ORDER BY s.FirstName, s.LastName, s.Address

-- 07. 42 Phones

SELECT s.FirstName, s.Address, s.Phone 
FROM 
	Students AS s 
WHERE 
	s.Phone LIKE '42%' AND s.MiddleName is not NULL 
ORDER BY s.FirstName

--08. Students Teachers

SELECT s.FirstName,s.LastName , COUNT(s.Id)
	FROM Students AS s
	JOIN StudentsTeachers AS t ON s.Id = t.StudentId
GROUP BY s.FirstName, s.LastName

-- 09. Subjects with Students 

SELECT 
	CONCAT(t.FirstName, ' ', t.LastName) AS FullName, 
	CONCAT(s.Name,'-',s.Lessons ) AS Subjects, 
	COUNT(st.StudentId) AS Students
FROM 
	Teachers AS t 
	JOIN Subjects AS s ON t.SubjectId = s.Id
	JOIN StudentsTeachers AS st ON t.Id = st.TeacherId
GROUP BY 
	t.FirstName,t.LastName,
	s.Name,s.Lessons
ORDER BY Students DESC,	FullName, Subjects

-- 10. Students to Go

SELECT CONCAT(s.FirstName, ' ', s.LastName) AS 'Full Name'
FROM 
	Students AS s 
	LEFT JOIN StudentsExams AS se ON s.Id = se.StudentId
	WHERE se.ExamId is NULL
ORDER BY [Full Name]

-- 11. Busiest Teachers 

SELECT TOP 10
	t.FirstName , t.LastName,
	COUNT(st.StudentId) AS StudentsCount
FROM 
	Teachers AS t 
	JOIN Subjects AS s ON t.SubjectId = s.Id
	JOIN StudentsTeachers AS st ON t.Id = st.TeacherId
GROUP BY 
	t.FirstName,t.LastName
ORDER BY StudentsCount DESC, t.FirstName, t.LastName

--12. Top Students 

SELECT TOP 10 
	s.FirstName, s.LastName , STR(AVG(se.Grade),4, 2) AS Grade
	FROM Students AS s 
	JOIN StudentsExams AS se ON s.Id = se.StudentId
	GROUP BY s.FirstName, s.LastName
ORDER BY Grade DESC ,s.FirstName, s.LastName

-- 13. Second Highest Grade 

SELECT SortedStudents.FirstName, SortedStudents.LastName, SortedStudents.Grade
FROM 
(
	SELECT FirstName, LastName, Grade, ROW_NUMBER() 
	OVER (PARTITION BY FirstName, LastName 
	ORDER BY Grade DESC) AS RowNumber  
	FROM Students AS s JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
)	AS SortedStudents

 WHERE SortedStudents.RowNumber = 2
 ORDER BY FirstName, LastName

 -- 14. Not So In The Studying 

SELECT CONCAT(s.FirstName, ' ', COALESCE(MiddleName + ' ',''), s.LastName) AS 'Full Name'
FROM 
	Students AS s LEFT JOIN StudentsSubjects AS ss ON s.Id = ss.StudentId
WHERE 
	ss.SubjectId is NULL ORDER BY [Full Name]

--15. Top Student per Teacher 

--16. Average Grade per Subject 

SELECT su.[Name], AVG(ss.Grade) AS Grade
	FROM Subjects AS su 
	JOIN StudentsSubjects AS ss ON su.Id = ss.SubjectId 
	GROUP BY su.[Name] ,SubjectId
	ORDER BY SubjectId

--17. Exams Information

SELECT 
CASE
        WHEN MONTH(e.Date) >= 1 AND MONTH(e.Date) <=3  THEN 'Q1'
        WHEN MONTH(e.Date) >= 4 AND MONTH(e.Date) <=6 THEN 'Q2'
        WHEN MONTH(e.Date) >= 7 AND MONTH(e.Date) <=9 THEN 'Q3'
        WHEN MONTH(e.Date) >= 10 AND MONTH(e.Date) <=12 THEN 'Q4'
		WHEN e.Date is NULL THEN 'TBA'

END 'Quarter',s.[Name] AS [SubjectName],COUNT(se.StudentId) AS [StudentsCount]
FROM Exams AS e
	JOIN Subjects AS s ON s.Id = e.SubjectId 
	JOIN StudentsExams AS se ON se.ExamId = e.Id
	WHERE se.Grade >= 4
	GROUP BY e.[Date] ,s.[Name]
	ORDER BY [Quarter]
	
	
	
	



	
	