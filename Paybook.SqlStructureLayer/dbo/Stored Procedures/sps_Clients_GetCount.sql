CREATE PROCEDURE [dbo].[sps_Clients_GetCount]
	@BusinessId INT,
	@Count INT OUT
AS
BEGIN
	SELECT @Count = COUNT(Id)
	FROM Clients
	WHERE BusinessId = @BusinessId AND IsActive = 1;
END
