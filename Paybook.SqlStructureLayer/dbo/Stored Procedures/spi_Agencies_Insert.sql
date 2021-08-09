CREATE PROCEDURE [dbo].[spi_Agencies_Insert]
    @BusinessId INT,
    @CreateBy NVARCHAR(50), 
    @Name NVARCHAR(256), 
    @Type NVARCHAR(256), 
    @ContactName NVARCHAR(256), 
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

		INSERT INTO [dbo].[Agencies]([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[Type],[ContactName],[PhoneNumber1],[PhoneNumber2],[Email],[AddressLine1],[AddressLine2],[City],[StateId],[CountryId],[Pincode])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@Name,@Type,@ContactName,@PhoneNumber1,@PhoneNumber2,@Email,@AddressLine1,@AddressLine2,@City,@StateId,@CountryId,@Pincode)

		SELECT SCOPE_IDENTITY() AS Id;
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,'Agencies','spi_Agencies_Insert',ERROR_MESSAGE;

	END CATCH 
END