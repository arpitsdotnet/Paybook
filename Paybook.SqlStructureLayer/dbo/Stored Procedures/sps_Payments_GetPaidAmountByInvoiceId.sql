CREATE PROCEDURE [dbo].[sps_Payments_GetPaidAmountByInvoiceId]
	@BusinessId INT,
	@InvoiceId INT
AS
BEGIN
	SELECT ISNULL(SUM(pay.[Amount]),0) AS [PaidTotal]
	FROM [dbo].[Payments] pay 
		INNER JOIN [dbo].[InvoicePayments] ipay ON pay.Id = ipay.PaymentId 
	WHERE pay.BusinessId = @BusinessId AND ipay.InvoiceId = @InvoiceId
END