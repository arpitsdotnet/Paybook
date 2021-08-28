CREATE PROCEDURE [dbo].[sps_Dashboard_GetInvoiceCountByDays]
	@BusinessId INT,
	@Days INT = 7
AS
BEGIN
	SELECT COUNT(Id) AS [Count] 
	FROM Invoices
	WHERE BusinessId = @BusinessId AND IsActive = 1
END
