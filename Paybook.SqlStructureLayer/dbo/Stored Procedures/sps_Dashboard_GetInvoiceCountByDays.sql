CREATE PROCEDURE [dbo].[sps_Dashboard_GetInvoiceCountByDays]
	@BusinessId INT,
	@Days INT = 7
AS
BEGIN
	SELECT COUNT(ID) AS [Count] 
	FROM Invoices
	WHERE BusinessId = @BusinessId AND IsActive = 1 AND
		(InvoiceDate BETWEEN DATEADD(DAY, -(@Days), GETDATE()) AND GETDATE())
END
