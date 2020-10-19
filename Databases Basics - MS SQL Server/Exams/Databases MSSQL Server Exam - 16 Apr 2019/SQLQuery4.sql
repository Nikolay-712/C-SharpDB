--18. Vacation

CREATE FUNCTION udf_CalculateTickets(@origin VARCHAR(30), @destination VARCHAR(30), @peopleCount INT)
RETURNS VARCHAR(MAX)
AS BEGIN	

   DECLARE @tripId INT = (SELECT f.Id FROM Flights AS f
							JOIN Tickets AS t ON t.FlightId = f.Id 
							WHERE Destination = @destination AND Origin = @origin)

	IF(@peopleCount <= 0) RETURN'Invalid people count!'
	IF( @tripId IS NULL) RETURN 'Invalid flight!'

	DECLARE @ticketPrice DECIMAL(15,2) = (SELECT t.Price FROM Flights AS f 
											JOIN Tickets AS t ON t.FlightId = f.Id 
											WHERE 
											f.Origin = @origin AND 
											f.Destination = @destination) 

	DECLARE @result DECIMAL(15, 2) = @ticketPrice * @peoplecount;
	RETURN 'Total price ' + CAST(@result as VARCHAR(30));

END

GO
SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', 33)
SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', -1)
SELECT dbo.udf_CalculateTickets('Invalid','Rancabolang', 33)

--19. Wrong Data
GO

CREATE PROC usp_CancelFlights
AS
UPDATE Flights
SET DepartureTime = NULL, ArrivalTime = NULL
WHERE ArrivalTime > DepartureTime


--20. Deleted Planes 

CREATE TABLE DeletedPlanes
(
	Id INT,
	Name VARCHAR(30),
	Seats INT,
	Range INT
)
GO

CREATE TRIGGER tr_DeletedPlanes ON Planes 
AFTER DELETE AS
  INSERT INTO DeletedPlanes (Id, Name, Seats, Range) 
      (SELECT Id, Name, Seats, Range FROM deleted)

DELETE Tickets
WHERE FlightId IN (SELECT Id FROM Flights WHERE PlaneId = 8)

DELETE FROM Flights
WHERE PlaneId = 8

DELETE FROM Planes
WHERE Id = 8