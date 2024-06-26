﻿CREATE PROCEDURE [dbo].[sps_IdentityUsers_IsValid]
	@Username VARCHAR(50),
	@PasswordHash VARCHAR(50),
	@Message NVARCHAR(20) OUT
AS
BEGIN
	IF EXISTS(SELECT Id FROM IdentityUsers WHERE Username=@Username AND IsActive = 1)
	BEGIN 	
		IF EXISTS(SELECT Id FROM IdentityUsers WHERE Username=@Username AND [PasswordHash]=@PasswordHash)
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
		SET	@Message = 'UserNotFound'
	END
END
GO
