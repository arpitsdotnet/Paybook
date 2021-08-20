CREATE PROCEDURE [dbo].[sps_Dashboard_GetPaymentCountByDays]
	@BusinessId INT,
	@Days INT = 7
AS
BEGIN
	SELECT COUNT(pay.Id) AS [Count] 
	FROM Payments pay
		INNER JOIN InvoicePayments ipay ON pay.Id = ipay.PaymentId
	WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND
		(pay.PaymentDate BETWEEN DATEADD(DAY, -(@Days), GETDATE()) AND GETDATE())
END
