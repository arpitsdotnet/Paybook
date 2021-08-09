CREATE PROCEDURE [dbo].[sps_Dashboard_GetPaymentCountAndAmountByDays]
	@BusinessId int,
	@Days int = 7
AS
BEGIN
	SELECT COUNT(Id)as [Count],SUM(ISNULL(Amount,0)) as [Amount],PaymentDate AS [Date]
	FROM [Payments]
	WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
		(PaymentDate BETWEEN DATEADD(DAY, -(@Days), GETDATE()) AND GETDATE())
	GROUP BY PaymentDate
	ORDER BY PaymentDate DESC
END