CREATE PROCEDURE [dbo].[spi_Payments_Insert]
	@Id INT = 0 OUTPUT,
	@BusinessId INT,
	@CreateBy NVARCHAR(256),
	@InvoiceId INT,
	@TransactionId NVARCHAR(256),
	@PaymentDate DATETIME,
	@Method NVARCHAR(50),
	@Amount DECIMAL(18,2)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION
		
		DECLARE @InvoiceTotal DECIMAL(18,2) = (SELECT [Total] FROM [dbo].[Invoices] WHERE Id = @InvoiceId)
		DECLARE @PaidTotal DECIMAL(18,2) = (SELECT ISNULL(SUM(pay.[Amount]),0)
												FROM [dbo].[Payments] pay 
													INNER JOIN [dbo].[InvoicePayments] ipay ON pay.Id = ipay.PaymentId 
												WHERE pay.BusinessId = @BusinessId AND ipay.InvoiceId = @InvoiceId)

		--PRINT '@Amount>' + CAST(@Amount AS VARCHAR)
		--PRINT '@InvoiceTotal>' + CAST(@InvoiceTotal AS VARCHAR)
		--PRINT '@PaidTotal>' + CAST(@PaidTotal AS VARCHAR)

		-- GET THE INVOICE STATUS ID BASED ON REMAINING AMOUNT
		DECLARE @InvoiceStatusId INT;
		IF(@Amount = (@InvoiceTotal - @PaidTotal))
		BEGIN 
			SELECT @InvoiceStatusId = [Id] FROM [fns_Category_GetByCore](@BusinessId,'InvoiceStatus','Paid')
		END
		ELSE IF(@Amount < (@InvoiceTotal - @PaidTotal))
		BEGIN 
			SELECT @InvoiceStatusId = [Id] FROM [fns_Category_GetByCore](@BusinessId,'InvoiceStatus','PaidPartial')
		END
		
		--PRINT '@InvoiceStatusId>' +  CAST(@InvoiceStatusId AS VARCHAR)

		-- UPDATE INVOICE STATUS
		UPDATE [dbo].[Invoices]
		SET
			ModifyBy = @CreateBy,
			ModifyDate = GETDATE(),
			StatusId = @InvoiceStatusId
		WHERE Id = @InvoiceId;

		-- INSERT PAYMENT
		INSERT INTO [dbo].[Payments]([BusinessId],[IsActive],[CreateDate],[CreateBy],[TransactionId],[PaymentDate],[IsSuccess],[Method],[Amount])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@TransactionId,@PaymentDate,1,@Method,@Amount)
			 
		SET @Id = SCOPE_IDENTITY();

		INSERT INTO [dbo].[InvoicePayments]([InvoiceId],[PaymentId])
			VALUES(@InvoiceId,@Id)
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] 0,@CreateBy,'Payments','spi_Payments_Insert',@Error;

		THROW;

	END CATCH 
END
