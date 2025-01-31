INSERT INTO Teachers
VALUES
('Ruthanne','Bamb', '84948 Mesta Junction', '3105500146', 6),
('Gerrard','Lowin', '370 Talisman Plaza', '3324874824', 2),
('Merrile','Lambdin', '81 Dahle Plaza', '4373065154', 5),
('Bert','Ivie', '2 Gateway Circle', '4409584510', 4)

INSERT INTO Subjects
VALUES
('Geometry', 12),
('Health', 10),
('Drama', 7),
('Sports', 9)


UPDATE StudentsSubjects
SET StudentsSubjects.Grade = 6
WHERE 
	StudentsSubjects.SubjectId BETWEEN 1 and 2
	and StudentsSubjects.Grade >= 5.50


DELETE FROM StudentsTeachers
WHERE TeacherId IN 
	(SELECT Id FROM Teachers WHERE CHARINDEX('72', Teachers.Phone) > 0)

DELETE FROM Teachers WHERE CHARINDEX('72', Teachers.Phone) > 0