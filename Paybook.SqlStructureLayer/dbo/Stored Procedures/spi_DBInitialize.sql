CREATE PROCEDURE [dbo].[spi_DBInitialize]
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM IdentityRoles WHERE [Name] = 'Administrator')
	BEGIN
	
		--DEFAULT ROLE CREATE
		INSERT INTO IdentityRoles ([IsActive],[CreateDate],[CreateBy],[Name])
		VALUES (1, GETDATE(), 'admin', 'Administrator'),
				(1, GETDATE(), 'admin', 'Chief'),
				(1, GETDATE(), 'admin', 'Manager'),
				(1, GETDATE(), 'admin', 'Employee');
		
		--DEFAULT USER CREATE
		INSERT INTO IdentityUsers ([IsActive],[CreateDate],[CreateBy],[Username],[PasswordHash],[Email],[IsEmailConfirmed],[FirstName],[LastName])
		VALUES (1,GETDATE(),'admin','admin','Ad$mi@n0','floydwebtech@gmail.com',1,'Administrator','User'),
				(1,GETDATE(),'admin','amituser','Am$it@1234','amity435@gmail.com',1,'Amit','Yadav');
		
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
		VALUES (1,GETDATE(),'admin','India');
		
		DECLARE @CountryId INT;
		SET @CountryId = SCOPE_IDENTITY();;

		INSERT INTO StateMaster ([IsActive],[CreateDate],[CreateBy],[CountryId],[Name],[OrderBy])
		VALUES (1,GETDATE(),'admin',@CountryId,'Madhya Pradesh',1);
		
		DECLARE @StateId INT;
		SET @StateId = SCOPE_IDENTITY();

		--DEFAULT BUSINESS
		INSERT INTO Businesses ([IsActive],[CreateDate],[CreateBy],[Name],[Description],[IsSelected],[StateId],[CountryId])
		VALUES(1,GETDATE(),'admin','Administrator Business','Default Business for Administrator',1,@StateId,@CountryId)
		
		DECLARE @BusinessId INT;
		SET @BusinessId = SCOPE_IDENTITY();

		--DEFAULT CATEGORY TYPE AND CATEGORY --> Terms
		DECLARE @CateogryTypeId INT;
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Terms','Terms');
		
		SELECT @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net30','Net30','30',1),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net15','Net15','15',2),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net45','Net45','45',3),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net60','Net60','60',4),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net90','Net90','90',5);

		--DEFAULT CATEGORY TYPE AND CATEGORY --> DiscountTypes
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Discount Types','DiscountTypes');
		
		SELECT @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Disc. Percentage','DiscountPercentage','',1),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Disc. Amount','DiscountAmount','',2);
				
		--DEFAULT CATEGORY TYPE AND CATEGORY --> InvoiceMessage
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Settings','Settings');
		
		SELECT @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Invoice Message','InvoiceMessage','Thank you for your business and have a great day!',1)

	END
END
GO