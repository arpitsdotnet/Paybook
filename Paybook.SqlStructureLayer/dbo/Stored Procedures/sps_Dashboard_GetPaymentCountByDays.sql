CREATE PROCEDURE [dbo].[sps_Dashboard_GetPaymentCountByDays]
	@BusinessId INT,
	@Days INT = 7
AS
BEGIN
	SELECT COUNT(pay.Id) AS [Count] 
	FROM Payments pay
	WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND
		(PaymentDate BETWEEN DATEADD(DAY, -(@Days), GETDATE()) AND GETDATE())
END
