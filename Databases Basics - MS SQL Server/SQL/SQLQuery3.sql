--05. Commits 

SELECT 
	com.Id, 
	com.[Message],
	com.RepositoryId,
	com.ContributorId 
FROM Commits AS com 
ORDER BY 
	com.Id,
	com.[Message],
	com.RepositoryId,
	com.ContributorId

--06. Heavy HTML

SELECT fil.Id,fil.[Name],fil.Size 
FROM 
	Files AS fil
WHERE 
	fil.Size > 1000 AND CHARINDEX('html',fil.[Name]) > 0
ORDER BY 
	fil.Size DESC,
	fil.Id, 
	fil.[Name]

--07. Issues and Users

SELECT iss.Id,CONCAT(us.Username, ' : ', iss.Title) AS [IssueAssignee]
FROM Issues AS iss 
	JOIN Users AS us ON iss.AssigneeId = us.Id 
ORDER BY 
	iss.Id DESC,
	IssueAssignee

--08. Non-Directory Files 

SELECT f.Id, f.[Name], CONCAT(f.Size,'KB')
FROM Files AS f 
WHERE NOT EXISTS (SELECT sf.ParentId FROM Files sf WHERE sf.ParentId = f.Id )
ORDER BY 
	f.Id, 
	f.[Name], 
	f.Size DESC

--09. Most Contributed Repositories

SELECT *
FROM Repositories AS r 
	JOIN Commits AS c ON c.RepositoryId = r.Id
	JOIN Users AS u ON u.Id = c.ContributorId
	
--10. User and Files 

SELECT u.Username,AVG(f.Size) AS [Size]
FROM Users AS u 
	 JOIN Commits AS c ON u.Id = c.ContributorId 
	 JOIN Files AS f ON f.CommitId = c.Id 
GROUP BY u.Username
ORDER BY Size DESC, u.Username 

 

