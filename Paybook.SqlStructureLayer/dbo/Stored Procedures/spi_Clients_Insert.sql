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
    @Pincode NVARCHAR(10),
	@OpeningBalance DECIMAL(18,2)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION

		INSERT INTO [dbo].[Clients]([BusinessId],[IsActive],[CreateDate],[CreateBy],[Name],[AgencyName],[PhoneNumber1],[PhoneNumber2],[Email],[AddressLine1],[AddressLine2],[City],[StateId],[CountryId],[Pincode])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@Name,@AgencyName,@PhoneNumber1,@PhoneNumber2,@Email,@AddressLine1,@AddressLine2,@City,@StateId,@CountryId,@Pincode);
			 
		SET @Id = SCOPE_IDENTITY();

		IF(@OpeningBalance > 0)
		BEGIN
			-- INSERT PAYMENT
			INSERT INTO [dbo].[Payments]([BusinessId],[IsActive],[CreateDate],[CreateBy],[PaymentDate],[IsSuccess],[Method],[Amount])
				 VALUES(@BusinessId,1,GETDATE(),@CreateBy,GETDATE(),1,'OpeningBalance',@OpeningBalance)
			 
			DECLARE @PaymentId INT = SCOPE_IDENTITY();

			INSERT INTO [dbo].[ClientPayments]([ClientId],[PaymentId])
				VALUES(@Id,@PaymentId)

			INSERT INTO [dbo].[ClientBalances]([BusinessId],[IsActive],[CreateDate],[CreateBy],ClientId,Balance)
				VALUES(@BusinessId,1,GETDATE(),@CreateBy,@Id,@OpeningBalance);
		END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@CreateBy,'Clients','spi_Clients_Insert',@Error;

	END CATCH 
END