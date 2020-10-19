--01. Create Table Logs 

CREATE TABLE Logs
(
  LogID INT NOT NULL IDENTITY,
  AccountID INT FOREIGN KEY REFERENCES Accounts(Id),
  OldSum MONEY,
  NewSum MONEY,
)

GO

CREATE TRIGGER tr_ChangeBalance ON Accounts
AFTER UPDATE
AS BEGIN
	INSERT INTO 
	Logs(AccountID,OldSum,NewSum)
	SELECT 
	i.Id, d.Balance, i.Balance 
	FROM inserted AS i INNER JOIN deleted AS d ON i.Id = d.Id
END

--02. Create Table Emails 

CREATE TABLE NotificationEmails
(
 Id INT NOT NULL IDENTITY,
 Recipient INT FOREIGN KEY REFERENCES Accounts(Id),
 [Subject] VARCHAR(50),
 Body TEXT
)

GO

CREATE TRIGGER tr_EmailsNotificationsAfterInsert
ON Logs AFTER INSERT 
AS BEGIN
	INSERT INTO NotificationEmails(Recipient,[Subject],Body)
	SELECT i.AccountID, 
	CONCAT('Balance change for account: ', i.AccountId),
	CONCAT('On ',GETDATE(),' your balance was changed from ',i.NewSum,' to ',i.OldSum)
	FROM inserted AS i
END

--03. Deposit Money 
GO

CREATE PROC usp_DepositMoney @accountId INT, @moneyAmount DECIMAL(18,2) AS
SELECT * FROM Accounts AS a JOIN AccountHolders AS ah ON ah.Id = a.Id

SELECT * from AccountHolders

SELECT * FROM Accounts