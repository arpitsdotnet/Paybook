CREATE PROCEDURE [dbo].[sps_Dashboard_GetPaymentsLast10]
	@BusinessId int,
	@Days int = 7
AS
BEGIN
	SELECT TOP 10 SUM(ISNULL(Amount,0)) as [Amount], CONVERT(DATE, PaymentDate) AS [Date]
	FROM [Payments]
	WHERE BusinessId = @BusinessId AND IsActive = 1
	GROUP BY CONVERT(DATE, PaymentDate)
END 