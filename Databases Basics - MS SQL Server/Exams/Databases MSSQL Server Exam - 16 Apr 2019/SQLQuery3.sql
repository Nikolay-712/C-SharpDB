--05. Trips 

SELECT f.Origin, f.Destination 
FROM Flights AS f ORDER BY f.Origin, f.Destination

--06. The "Tr" Planes 

SELECT * FROM Planes AS p 
WHERE CHARINDEX('Tr',p.Name) > 0 
ORDER BY 
p.Id, 
p.[Name], 
p.Seats,
p.[Range]

--07. Flight Profits

SELECT f.Id, SUM(t.Price) AS Price
FROM Flights AS f  JOIN Tickets AS t ON f.Id = t.FlightId
GROUP BY f.Id
ORDER BY Price DESC, f.Id

--08. Passanger and Prices 

SELECT TOP(10) p.FirstName, p.LastName, t.Price 
FROM Passengers AS p 
	JOIN Tickets AS t ON p.Id = t.PassengerId 
	GROUP BY p.FirstName, p.LastName,t.Price
ORDER BY 
	t.Price DESC,
	p.FirstName,
	p.LastName

--09. Top Luggages

SELECT lt.Type , COUNT(LuggageTypeId) AS MostUsedLuggage
FROM Luggages AS l JOIN LuggageTypes AS lt ON l.LuggageTypeId = lt.Id 
GROUP BY  lt.[Type]
ORDER BY MostUsedLuggage DESC, lt.[Type]

-- 10. Passanger Trips

SELECT CONCAT(p.FirstName, ' ', p.LastName) AS [Full Name], f.Origin, f.Destination
FROM Passengers AS p 
	JOIN Tickets AS t ON p.Id = t.PassengerId
	JOIN Flights AS f ON t.FlightId = f.Id
ORDER BY [Full Name], f.Origin,f.Destination

--11. Non Adventures People 

SELECT p.FirstName, p.LastName, p.Age 
FROM Passengers AS p 
	LEFT JOIN Tickets AS t ON p.Id = t.PassengerId 
	WHERE t.FlightId IS NULL
ORDER BY
	p.Age DESC,
	p.FirstName,
	p.LastName

--12. Lost Luggages 

SELECT p.PassportId, p.[Address] 
FROM Passengers AS p 
	LEFT JOIN Luggages AS l ON p.Id = l.PassengerId 
	WHERE l.Id IS NULL 
ORDER BY 
	p.PassportId,
	p.[Address]

--13. Count of Trips 

SELECT p.FirstName, p.LastName, COUNT(t.Id) AS [Total Trips]
FROM Passengers AS p  
	LEFT JOIN Tickets AS t ON p.Id = t.PassengerId
	GROUP BY p.FirstName, p.LastName
ORDER BY 
	[Total Trips] DESC,
	p.FirstName,
	p.LastName

-- 14. Full Info 

SELECT 
	CONCAT(p.FirstName, ' ', p.LastName) AS [Full Name],
	pl.[Name] AS [Plane Name],
	CONCAT(f.Origin,' - ', f.Destination) AS [Trip],
	lt.[Type] AS [Luggage Type]
FROM Passengers AS p  
	JOIN Tickets AS t ON p.Id = t.PassengerId
	JOIN Flights AS f ON t.FlightId = f.Id
	JOIN Planes AS pl ON f.PlaneId = pl.Id
	JOIN Luggages AS l ON t.LuggageId = l.Id
	JOIN LuggageTypes AS lt ON l.LuggageTypeId = lt.Id
ORDER BY 
	[Full Name],
	[Plane Name],
	f.Origin,
	f.Destination,
	lt.[Type]

--15. Most Expesnive Trips

SELECT p.FirstName, p.LastName,f.Destination,Price 
	FROM(SELECT * ,RANK() OVER(ORDER BY Price DESC) AS [row] FROM Tickets) AS t 
JOIN Passengers AS p ON t.PassengerId = p.Id
JOIN Flights AS f ON t.FlightId = f.Id
ORDER BY t.Price DESC, p.FirstName, p.LastName,f.Destination

--16. Destinations Info

SELECT f.Destination,COUNT(t.Id) AS [FilesCount]
FROM Flights AS f 
	LEFT JOIN Tickets AS t ON t.FlightId = f.Id 
	GROUP BY f.Destination
ORDER BY [FilesCount] DESC , f.Destination

--17. PSP 

SELECT p.Name ,p.Seats, COUNT(t.Id) AS [Passengers Count] 
FROM Planes AS p
	LEFT JOIN Flights AS f ON f.PlaneId = p.Id
	LEFT JOIN Tickets AS t ON t.FlightId = f.Id
	GROUP BY p.Name,p.Seats
ORDER BY 
	[Passengers Count] DESC, 
	p.[Name],
	p.Seats

