CREATE PROCEDURE [dbo].[sps_IdentityUser_IsValid]
	@Username VARCHAR(50),
	@PasswordHash VARCHAR(50),
	@Message NVARCHAR(20) OUT
AS
BEGIN
	IF EXISTS(SELECT Id FROM IdentityUsers WHERE Username=@UserName AND IsActive = 1)
	BEGIN 	
		SET	@Message = 'UserExist';
		IF EXISTS(SELECT Id FROM IdentityUsers WHERE Username=@UserName AND [PasswordHash]=@PasswordHash)
		BEGIN
			SET	@Message = 'UserMatch';
		END
		ELSE
		BEGIN
			SET	@Message = 'UserNotMatch'
		END
	END
	ELSE
	BEGIN
		SET	@Message = 'UserNotExist'
	END
END
GO
