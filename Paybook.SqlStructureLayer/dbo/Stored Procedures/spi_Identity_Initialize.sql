CREATE PROCEDURE [dbo].[spi_Identity_Initialize]
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM IdentityRoles WHERE [Name] = 'Administrator')
	BEGIN
	
		--DEFAULT ROLE CREATE
		INSERT INTO IdentityRoles ([Name])
		VALUES ('Administrator'),('Chief'),('Manager'),('Employee');
		
		--DEFAULT USER CREATE
		INSERT INTO IdentityUsers ([IsActive],[CreateDate],[CreateBy],[Username],[PasswordHash],[Email],[IsEmailConfirmed],[FirstName],[LastName])
		VALUES (1,GETDATE(),'IdentityInitializer','admin','Ad$mi@n0','floydwebtech@gmail.com',1,'Administrator','User'),
				(1,GETDATE(),'IdentityInitializer','amituser','Am$it@1234','amity435@gmail.com',1,'Amit','Yadav');
		
		DECLARE @RoleId INT, @UserId INT;

		SELECT @RoleId = Id FROM IdentityRoles WHERE [Name] = 'Administrator';
		SELECT @UserId = Id FROM IdentityUsers WHERE [Username] = 'admin';

		INSERT INTO IdentityUserRoles ([UserId],[RoleId])
		VALUES (@UserId, @RoleId);
		
		SELECT @RoleId = Id FROM IdentityRoles WHERE [Name] = 'Chief';
		SELECT @UserId = Id FROM IdentityUsers WHERE [Username] = 'amituser';

		INSERT INTO IdentityUserRoles ([UserId],[RoleId])
		VALUES (@UserId, @RoleId);

		--DEFAULT COUNTRY / STATE CREATE
		INSERT INTO CountryMaster ([IsActive],[CreateDate],[CreateBy],[Name])
		VALUES (1,GETDATE(),'IdentityInitializer','India');
		
		DECLARE @CountryId INT;
		SELECT @CountryId = Id FROM CountryMaster WHERE [Name] = 'India';

		INSERT INTO StateMaster ([IsActive],[CreateDate],[CreateBy],[CountryId],[Name],[OrderBy])
		VALUES (1,GETDATE(),'IdentityInitializer',@CountryId,'Madhya Pradesh',1);


	END
END
GO