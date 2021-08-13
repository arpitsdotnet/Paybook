CREATE PROCEDURE [dbo].[spi_Clients_Insert]
	@Id INT = 0 OUTPUT,
    @BusinessId INT,
    @CreateBy NVARCHAR(256), 
    @Name NVARCHAR(256), 
    @AgencyName NVARCHAR(256) = '', 
    @PhoneNumber1 NVARCHAR(20), 
    @PhoneNumber2 NVARCHAR(20), 
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

		INSERT INTO [dbo].[Clients]([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[AgencyName],[PhoneNumber1],[PhoneNumber2],[Email],[AddressLine1],[AddressLine2],[City],[StateId],[CountryId],[Pincode])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@Name,@AgencyName,@PhoneNumber1,@PhoneNumber2,@Email,@AddressLine1,@AddressLine2,@City,@StateId,@CountryId,@Pincode)
			 
		SET @Id = SCOPE_IDENTITY();
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@CreateBy,'Clients','spi_Clients_Insert',@Error;

	END CATCH 
END