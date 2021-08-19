CREATE PROCEDURE [dbo].[sps_Dashboard_GetClientCountByDays]
	@BusinessId INT,
	@Days INT = 7
AS
BEGIN 
	SELECT COUNT(ID) as Count,CONVERT(DATE, CreateDate) as [Date]
	FROM Clients
	WHERE BusinessId = @BusinessId AND IsActive = 1 AND
	 (CreateDate BETWEEN  DATEADD(DAY, -(@Days), GETDATE()) AND GETDATE())
	GROUP BY CONVERT(DATE, CreateDate);
END
