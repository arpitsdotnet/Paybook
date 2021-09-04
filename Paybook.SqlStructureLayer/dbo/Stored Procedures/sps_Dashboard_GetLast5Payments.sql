CREATE PROCEDURE [dbo].[sps_Dashboard_GetLast5Payments]
	@BusinessId INT
AS
BEGIN
	SELECT TOP 5 pay.[Id],clpay.[ClientId],pay.[PaymentDate],pay.[IsSuccess],pay.[Method],pay.[Amount]
	FROM [dbo].[Payments] pay
		INNER JOIN [dbo].[ClientPayments] clpay ON pay.Id = clpay.PaymentId
	WHERE pay.[BusinessId] = @BusinessId AND pay.[IsActive] = 1
	ORDER BY pay.[PaymentDate] DESC
END
