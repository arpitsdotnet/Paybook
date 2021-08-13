CREATE PROCEDURE [dbo].[spu_Clients_Update]
	@Id INT,
    @BusinessId INT,
    @ModifyBy NVARCHAR(256), 
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
		
		UPDATE Clients
		SET
			[ModifyDate] = GETDATE(),
			[ModifyBy] = @ModifyBy,
			[Name] = @Name,
			[AgencyName] = @AgencyName,
			[PhoneNumber1] = @PhoneNumber1,
			[PhoneNumber2] = @PhoneNumber2,
			[Email] = @Email,
			[AddressLine1] = @AddressLine1,
			[AddressLine2] = @AddressLine2,
			[City] = @City,
			[StateId] = @StateId,
			[CountryId] = @CountryId,
			[Pincode] = @Pincode
		WHERE BusinessId = @BusinessId AND Id = @Id AND IsActive = 1;
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@ModifyBy,'Clients','spu_Clients_Update',@Error;

	END CATCH 
END