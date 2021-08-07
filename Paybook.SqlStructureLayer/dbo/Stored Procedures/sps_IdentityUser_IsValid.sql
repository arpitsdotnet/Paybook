CREATE PROCEDURE [dbo].[sps_IdentityUser_IsValid]
	@Username VARCHAR(50),
	@PasswordHash VARCHAR(50)
AS
BEGIN
	IF EXISTS(SELECT Id FROM IdentityUsers WHERE Username=@UserName and [PasswordHash]=@PasswordHash)
	BEGIN
		SELECT 'UserExist' AS [Message]
	END
	ELSE
	BEGIN
		SELECT 'UserNotExist' AS [Message]
	END
END
GO
