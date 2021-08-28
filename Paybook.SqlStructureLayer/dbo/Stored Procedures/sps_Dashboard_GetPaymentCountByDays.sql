CREATE PROCEDURE [dbo].[sps_Dashboard_GetPaymentCountByDays]
	@BusinessId INT,
	@Days INT = 7
AS
BEGIN
	SELECT COUNT(pay.Id) AS [Count] 
	FROM Payments pay
		INNER JOIN InvoicePayments ipay ON pay.Id = ipay.PaymentId
		INNER JOIN Invoices inv ON ipay.InvoiceId = inv.Id
	WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND inv.IsActive = 1
END
