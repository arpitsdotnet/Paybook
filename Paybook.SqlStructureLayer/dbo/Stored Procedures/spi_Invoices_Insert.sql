CREATE PROCEDURE [dbo].[spi_Invoices_Insert]
	@Id INT = 0 OUTPUT,
    @BusinessId INT,
    @CreateBy NVARCHAR(256),
	@InvoiecNumber NVARCHAR(50),
	@Description NVARCHAR(1000),
	@InvoiceDate DATETIME,
	@StatusId INT,
	@ClientId INT,
	@ClientEmail NVARCHAR(256),
	@BillingAddress NVARCHAR(1000),
	@TermsId INT,
	@DueDate DATETIME,
	@IsOverdue BIT,
	@Message NVARCHAR(MAX),
	@Subtotal DECIMAL(18,2),
	@TaxableTotal DECIMAL(18,2),
	@DiscountTypeId INT,
	@DiscountAmount DECIMAL(18,2),
	@DiscountTotal DECIMAL(18,2),
	@Total DECIMAL(18,2)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION
				
		INSERT INTO [dbo].[Invoices]([BusinessId],[IsActive],[CreateDate],[CreateBy],[InvoiceNumber],[Description],[InvoiceDate],[StatusId],[ClientId],[ClientEmail],[IsEmailSend],
					[BillingAddress],[TermsId],[DueDate],[IsOverdue],[Message],[Subtotal],[TaxableTotal],[DiscountTypeId],[DiscountAmount],[DiscountTotal],[Total])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@InvoiecNumber,@Description,@InvoiceDate,@StatusId,@ClientId,@ClientEmail,0,@BillingAddress,@TermsId,@DueDate,
					@IsOverdue,@Message,@Subtotal,@TaxableTotal,@DiscountTypeId,@DiscountAmount,@DiscountTotal,@Total)
			 
		SET @Id = SCOPE_IDENTITY();
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@CreateBy,'Invoices','spi_Invoices_Insert',@Error;
		THROW;

	END CATCH 
END
