CREATE PROCEDURE [dbo].[spi_Businesses_Insert]
	@Id INT = 0 OUTPUT,
    @CreateBy NVARCHAR(256), 
    @Name NVARCHAR(256), 
    @Description NVARCHAR(512), 
    @Image NVARCHAR(256), 
    @GSTNumber NVARCHAR(256), 
    @PhoneNumber NVARCHAR(20), 
    @Email NVARCHAR(256), 
    @AddressLine1 NVARCHAR(256), 
    @AddressLine2 NVARCHAR(256), 
    @City NVARCHAR(50), 
    @StateId INT, 
    @CountryId INT, 
    @Pincode NVARCHAR(10)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION

		DECLARE @IsSelected BIT = 1;
		IF EXISTS(SELECT Id FROM Businesses WHERE CreateBy = @CreateBy AND IsSelected = 1 AND IsActive = 1)
			SET @IsSelected = 0;
		
		INSERT INTO [dbo].[Businesses]([IsActive],[CreateDate],[CreateBy],[Name],[Description],[IsSelected],[Image],[GSTNumber],[PhoneNumber],[Email],[AddressLine1],[AddressLine2],[City],[StateId],[CountryId],[Pincode])
			 VALUES(1,GETDATE(),@CreateBy,@Name,@Description,@IsSelected,@Image,@GSTNumber,@PhoneNumber,@Email,@AddressLine1,@AddressLine2,@City,@StateId,@CountryId,@Pincode)

		SET @Id = SCOPE_IDENTITY();

		DECLARE @UserId INT
		SET @UserId = (SELECT Id FROM IdentityUsers WHERE Username = @CreateBy)

		INSERT INTO [dbo].[UserBusinesses]([UserId],[BusinessId])
			VALUES(@UserId, @Id)
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] 0,@CreateBy,'Businesses','spi_Businesses_Insert',@Error;

		THROW;

	END CATCH 
END