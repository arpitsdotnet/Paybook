CREATE PROCEDURE [dbo].[spi_Clients_Insert]
    @BusinessId INT,
    @CreateBy NVARCHAR(50), 
    @Name NVARCHAR(256), 
    @PhoneNumber1 NVARCHAR(20), 
    @PhoneNumber2 NVARCHAR(20), 
    @Email NVARCHAR(256), 
    @AddressLine1 NVARCHAR(256), 
    @AddressLine2 NVARCHAR(256), 
    @City NVARCHAR(50), 
    @State NVARCHAR(50), 
    @Country NVARCHAR(50), 
    @Pincode NVARCHAR(10)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION

		INSERT INTO [dbo].[Clients]([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[PhoneNumber1],[PhoneNumber2],[Email],[AddressLine1],[AddressLine2],[City],[State],[Country],[Pincode])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@Name,@PhoneNumber1,@PhoneNumber2,@Email,@AddressLine1,@AddressLine2,@City,@State,@Country,@Pincode)

		SELECT SCOPE_IDENTITY() AS ID;
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,'Clients','spi_Clients_Insert',ERROR_MESSAGE;

	END CATCH 
END