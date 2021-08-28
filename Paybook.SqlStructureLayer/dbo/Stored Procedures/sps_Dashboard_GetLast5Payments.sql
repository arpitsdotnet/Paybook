CREATE PROCEDURE [dbo].[sps_Dashboard_GetLast5Payments]
	@BusinessId INT
AS
BEGIN
	SELECT TOP 5 [Id],[PaymentDate],[IsSuccess],[Method],[Amount]
	FROM [dbo].[Payments]
	WHERE [BusinessId] = @BusinessId AND [IsActive] = 1
	ORDER BY [PaymentDate] DESC
END
