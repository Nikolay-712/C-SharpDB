--11. User Total Commits 

CREATE FUNCTION udf_UserTotalCommits(@username VARCHAR(MAX))
RETURNS INT
AS BEGIN
	
	RETURN
	(	SELECT COUNT(c.Id) 
		FROM Users AS u 
			JOIN Commits AS c ON u.Id = c.ContributorId
		GROUP BY u.Username HAVING u.Username = @username
	)
END

GO

SELECT dbo.udf_UserTotalCommits('UnderSinduxrein')

GO

--12. Find by Extensions 

CREATE PROC usp_FindByExtension @extension VARCHAR(MAX)
AS SELECT 
	f.Id,f.[Name],CONCAT(f.Size ,'KB') AS [Size]
	FROM Files AS f 
	WHERE CHARINDEX(@extension,f.[Name]) > 0
	ORDER BY f.Id, f.Name,f.Size DESC

	EXEC usp_FindByExtension 'txt'