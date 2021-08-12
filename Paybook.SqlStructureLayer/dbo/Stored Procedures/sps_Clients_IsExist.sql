CREATE PROCEDURE [dbo].[sps_Clients_IsExist]
	@IsExist BIT = 0 OUT,
	@CreateBy NVARCHAR(256),
	@Name NVARCHAR(50)
AS
BEGIN
	IF EXISTS (SELECT Id FROM Clients WHERE CreateBy = @CreateBy AND [Name] = @Name)
	BEGIN
		SELECT @IsExist = 1;
	END
	ELSE
	BEGIN
		SELECT @IsExist = 0;
	END
END
