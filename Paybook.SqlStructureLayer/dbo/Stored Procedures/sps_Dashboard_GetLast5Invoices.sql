CREATE PROCEDURE [dbo].[sps_Dashboard_GetLast5Invoices]
	@BusinessId INT
AS
BEGIN
	SELECT TOP 5 [Id],[InvoiceNumber],[InvoiceDate],[ClientId],[StatusId],[Total]
	FROM [dbo].[Invoices]
	WHERE [BusinessId] = @BusinessId AND [IsActive] = 1
	ORDER BY [InvoiceDate] DESC
END
