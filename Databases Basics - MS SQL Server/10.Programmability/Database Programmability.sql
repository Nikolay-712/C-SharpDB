--01. Employees with Salary Above 35000

CREATE PROC
	usp_GetEmployeesSalaryAbove35000
AS SELECT 
	e.FirstName, e.LastName FROM Employees AS e
WHERE e.Salary > 35000

EXEC usp_GetEmployeesSalaryAbove35000

GO
--02. Employees with Salary Above Number 

CREATE PROC
	usp_GetEmployeesSalaryAboveNumber @salary DECIMAL(18,4)		
AS SELECT 
	e.FirstName,e.LastName FROM Employees AS e
WHERE e.Salary >= @salary

EXEC usp_GetEmployeesSalaryAboveNumber 50000

GO

--03. Town Names Starting With

CREATE PROC
	usp_GetTownsStartingWith @firstSymbol VARCHAR(MAX) 
AS SELECT 
	t.Name FROM Towns AS t
WHERE  SUBSTRING(t.Name,1,LEN(@firstSymbol)) = @firstSymbol

EXEC usp_GetTownsStartingWith be
GO

--04. Employees from Town

CREATE PROC 
	usp_GetEmployeesFromTown @townName VARCHAR(MAX)
AS SELECT 
	e.FirstName,e.LastName FROM Employees AS e 
	JOIN Addresses AS a ON e.AddressID = a.AddressID
	JOIN Towns AS t ON a.TownID = t.TownID
WHERE t.Name = @townName

EXEC usp_GetEmployeesFromTown Sofia

GO

--05. Salary Level Function 

CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4)) 
RETURNS VARCHAR(7)
AS BEGIN

    DECLARE @salaryLevel VARCHAR(7)

	IF(@salary < 30000) SET @salaryLevel = 'Low'
	IF(@salary BETWEEN 30000 and 50000) SET @salaryLevel = 'Average'
	IF(@salary > 50000) SET @salaryLevel = 'High'

	RETURN @salaryLevel
END
GO

SELECT e.Salary, dbo.ufn_GetSalaryLevel(e.Salary) FROM Employees AS e

-- 06. Employees by Salary Level
GO

CREATE PROC usp_EmployeesBySalaryLevel @SalaryLevel VARCHAR(10)
AS SELECT 
	 e.FirstName, e.LastName 
     FROM Employees AS e
WHERE
	 dbo.ufn_GetSalaryLevel(e.Salary) = @SalaryLevel

EXEC usp_EmployeesBySalaryLevel Low
GO

--07. Define Function

CREATE  FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX))
RETURNS BIT
AS BEGIN
	
	DECLARE @index INT
	DECLARE @symbol NVARCHAR(MAX)

	SET @index = LEN(@word)
	
	WHILE(@index > 0)
	BEGIN

		SET @symbol = SUBSTRING(@word,@index,1)

		IF(CHARINDEX(@symbol, @setOfLetters)) = 0
		BEGIN
			RETURN 0;
		END

		SET @index = @index - 1

		
	END
	RETURN 1;

END
GO


SELECT dbo.ufn_IsWordComprised('pppp','Sofia')
GO

--08. Delete Employees and Departments

-- 09. Find Full Name 

CREATE PROC 
	usp_GetHoldersFullName
AS SELECT 
	  ah.FirstName
	 ,ah.LastName 
FROM AccountHolders AS ah

EXEC usp_GetHoldersFullName

GO

-- 10. People with Balance Higher Than 

CREATE PROC usp_GetHoldersWithBalanceHigherThan @balance DECIMAL(18,4)
AS SELECT 
	 ah.FirstName, ah.LastName
FROM 
	AccountHolders AS ah JOIN Accounts AS a ON ah.Id = a.AccountHolderId 
GROUP BY 
	ah.FirstName,
	ah.LastName
HAVING SUM(a.Balance) > @balance
ORDER BY ah.FirstName,ah.LastName

EXEC usp_GetHoldersWithBalanceHigherThan 50000

GO

--11. Future Value Function

CREATE FUNCTION
	ufn_CalculateFutureValue(@initialSum DECIMAL(18,4),@interestRate FLOAT ,@years INT)
RETURNS DECIMAL(18,4)
BEGIN
	DECLARE	@totalSum DECIMAL(18,4)

	SET @totalSum = @initialSum * POWER(1 + @interestRate, @years)

	RETURN @totalSum
END
GO
 SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)

 --12. Calculating Interest
 GO

 CREATE PROC 
	usp_CalculateFutureValueForAccount @accountID INT, @rate FLOAT
AS SELECT 
	@accountID as 'Account Id',
	ah.FirstName AS 'First Name',
	ah.LastName AS 'Last Name',
	a.Balance as 'Current Balance',
	dbo.ufn_CalculateFutureValue(a.Balance, @rate, 5) as 'Balance in 5 years'
FROM 
	AccountHolders AS ah JOIN Accounts AS a ON ah.Id = a.Id
WHERE 
	@accountID = a.Id
GO

EXEC dbo.usp_CalculateFutureValueForAccount 1, 0.1

--13. *Cash in User Games Odd Rows
