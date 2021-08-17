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
		
		SET @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net30','Net30','30',1),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net15','Net15','15',2),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net45','Net45','45',3),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net60','Net60','60',4),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Net90','Net90','90',5);

		--DEFAULT CATEGORY TYPE AND CATEGORY --> DiscountTypes
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Discount Types','DiscountTypes');
		
		SET @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Disc. Percentage','DiscountPercentage','',1),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Disc. Amount','DiscountAmount','',2);
				
		--DEFAULT CATEGORY TYPE AND CATEGORY --> InvoiceMessage
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Settings','Settings');
		
		SET @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Invoice Message','InvoiceMessage','Thank you for your business and have a great day!',1)
				
		--DEFAULT CATEGORY TYPE AND CATEGORY --> WorkTypes
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Work Types','WorkTypes');
		
		SET @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Cash','Cash','',1),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Fitness','Fitness','',2),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Insurance','Insurance','',3),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Licence','Licence','',4),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'National Insurance','NationalInsurance','',5),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'New Registration','NewRegistration','',6),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Permit','Permit','',7),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Permit Picnic / Marriage','PermitPicnicMarriage','',8),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'PUC','PUC','',9),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Renewal','Renewal','',10),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Tax M.','TaxM','',11),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Tax Q.','TaxQ','',12),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Transfer','Transfer','',13);
				
		--DEFAULT CATEGORY TYPE AND CATEGORY --> TaxTypes
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Tax Types','TaxTypes');
		
		SET @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'No Tax','NoTax','',1),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'State Tax','StateTax','18',2),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Center Tax','CenterTax','18',3);
				
		--DEFAULT CATEGORY TYPE AND CATEGORY --> InvoiceStatus
		INSERT INTO CategoryTypeMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Core])
		VALUES (@BusinessId,1,GETDATE(),'admin','Invoice Status','InvoiceStatus');
		
		SET @CateogryTypeId = SCOPE_IDENTITY();

		INSERT INTO CategoryMaster([BusinessId],[IsActive],[CreateDate],[CreateBy],[CategoryTypeId],[Name],[Core],[Value],[OrderBy])
		VALUES (@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Open','Open','You’ve created an invoice and it hasn’t been sent to the customer.',1),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Sent','Sent','Invoice has been sent to the customer',2),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Paid Partial','PaidPartial','Invoice has been partially paid by the customer and amount is still remaining',3),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Paid','Paid','Invoice has been fully paid by the customer',4),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Void','Void','You will void an invoice if it has been raised incorrectly. Customers cannot pay for a voided invoice.',5),
				(@BusinessId,1,GETDATE(),'admin',@CateogryTypeId,'Write Off','WriteOff','You can Write Off an invoice only you’re sure that the amount the customer owes is uncollectible.',6);

	END
END
GO