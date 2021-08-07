CREATE PROCEDURE [dbo].[sps_Dashboard_GetInvoiceCountByDays]
	@BusinessId int,
	@Days int = 7
AS
BEGIN
	SELECT COUNT(Id) AS [Count], SUM(ISNULL([Total],0)) AS InvoiceAmount, CONVERT(DATE, InvoiceDate) AS InvoiceDate
	FROM Invoices
	WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
		(InvoiceDate BETWEEN DATEADD(DAY, -(@Days), GETDATE()) AND GETDATE())
	GROUP BY CONVERT(DATE, InvoiceDate)
	ORDER BY InvoiceDate DESC;
END


