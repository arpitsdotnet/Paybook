CREATE PROCEDURE [dbo].[sps_Dashboard_GetPaymentCountAndAmountByDays]
	@BusinessId int,
	@Days int = 7
AS
BEGIN
	SELECT COUNT(pay.Id)as [Count],ISNULL(SUM(Amount),0) as [Amount], CONVERT(DATE,PaymentDate) AS [Date]
	FROM [Payments] pay
	WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
		(PaymentDate BETWEEN DATEADD(DAY, -(@Days), GETDATE()) AND GETDATE())
	GROUP BY CONVERT(DATE, PaymentDate)
	ORDER BY CONVERT(DATE, PaymentDate) DESC;
END