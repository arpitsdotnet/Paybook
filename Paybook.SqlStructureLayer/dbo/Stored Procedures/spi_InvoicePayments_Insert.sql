CREATE PROCEDURE [dbo].[spi_InvoicePayments_Insert]
	@Id INT = 0 OUTPUT,
    @BusinessId INT,
    @CreateBy NVARCHAR(256), 
	@InvoiceId INT,
	@PayDate DATETIME,
	@PayAmount DECIMAL(18,2)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION
					
		DECLARE @InvoiceTotal DECIMAL(18,2) = (SELECT [Total] FROM [dbo].[Invoices] WHERE Id = @InvoiceId)
		DECLARE @PaidTotal DECIMAL(18,2) = (SELECT ISNULL(SUM(pay.[PayAmount]),0)
												FROM [dbo].[InvoicePayments] pay 
												WHERE pay.BusinessId = @BusinessId AND pay.InvoiceId = @InvoiceId)
		-- GET THE INVOICE STATUS ID BASED ON REMAINING AMOUNT
		DECLARE @InvoiceStatusId INT, @IsClose BIT = 0;
		IF(@PayAmount = (@InvoiceTotal - @PaidTotal))
		BEGIN 
			SELECT @InvoiceStatusId = [Id] FROM [fns_Category_GetByCore](@BusinessId,'InvoiceStatus','Paid')
			SET @IsClose = 1;
		END
		ELSE IF(@PayAmount < (@InvoiceTotal - @PaidTotal))
		BEGIN 
			SELECT @InvoiceStatusId = [Id] FROM [fns_Category_GetByCore](@BusinessId,'InvoiceStatus','PaidPartial')
		END
		
		PRINT '@InvoiceStatusId>' +  CAST(@InvoiceStatusId AS VARCHAR)

		UPDATE [dbo].[Invoices]
		SET
			ModifyBy = @CreateBy,
			ModifyDate = GETDATE(),
			StatusId = @InvoiceStatusId,
			IsClose = @IsClose
		WHERE Id = @InvoiceId;


		-- INSERT INVOICE PAYMENT
		INSERT INTO [dbo].[InvoicePayments]([BusinessId],[IsActive],[CreateDate],[CreateBy],[InvoiceId],[PayDate],[PayAmount])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@InvoiceId,@PayDate,@PayAmount);
			 
		SET @Id = SCOPE_IDENTITY();

		DECLARE @ClientId INT = (SELECT cl.Id FROM Clients cl INNER JOIN Invoices inv ON cl.Id = inv.ClientId WHERE inv.Id = @InvoiceId AND cl.IsActive = 1 AND inv.IsActive = 1)
		
		IF EXISTS (SELECT Id FROM ClientBalances WHERE ClientId = @ClientId)
		BEGIN
			DECLARE @Balance DECIMAL(18,2) = (SELECT ISNULL(Balance,0) FROM ClientBalances WHERE ClientId = @ClientId);

			-- UPDATE CLIENT BALANCE
			UPDATE [dbo].[ClientBalances]
			SET
				Balance = Balance - @PayAmount
			WHERE ClientId = @ClientId;
			
		END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@CreateBy,'Clients','spi_Clients_Insert',@Error;

	END CATCH 
END
