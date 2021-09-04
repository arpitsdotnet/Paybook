CREATE PROCEDURE [dbo].[sps_Clients_GetBalanceTotalById]
	@BusinessId INT,
	@Id	INT
AS
BEGIN	

	SELECT Balance
	FROM [dbo].[ClientBalances]
	WHERE ClientId = @Id;

END