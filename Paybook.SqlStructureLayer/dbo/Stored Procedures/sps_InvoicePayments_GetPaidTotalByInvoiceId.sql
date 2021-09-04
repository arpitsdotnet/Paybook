CREATE PROCEDURE [dbo].[sps_InvoicePayments_GetPaidTotalByInvoiceId]
	@BusinessId INT,
	@InvoiceId INT
AS
BEGIN
	SELECT ISNULL(SUM(ipay.[PayAmount]),0) AS [PaidTotal]
	FROM [dbo].[InvoicePayments] ipay 
		INNER JOIN [dbo].[Invoices] inv ON ipay.InvoiceId  = inv.Id 
	WHERE ipay.BusinessId = @BusinessId AND ipay.InvoiceId = @InvoiceId AND ipay.IsActive = 1
END