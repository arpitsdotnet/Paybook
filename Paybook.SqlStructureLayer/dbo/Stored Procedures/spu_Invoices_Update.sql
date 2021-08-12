CREATE PROCEDURE [dbo].[spu_Invoices_Update]
	@Id INT,
    @BusinessId INT,
    @ModifyBy NVARCHAR(256),
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

		UPDATE [dbo].[Invoices]
		SET
			[ModifyDate] = GETDATE(),
			[ModifyBy] = @ModifyBy,
			[InvoiceNumber] = @InvoiecNumber,
			[Description] = @Description,
			[InvoiceDate] = @InvoiceDate,
			[StatusId] = @StatusId,
			[ClientId] = @ClientId,
			[ClientEmail] = @ClientEmail,
			[BillingAddress] = @BillingAddress,
			[TermsId] = @TermsId,
			[DueDate] = @DueDate,
			[IsOverdue] = @IsOverdue,
			[Message] = @Message,
			[Subtotal] = @Subtotal,
			[TaxableTotal] = @TaxableTotal,
			[DiscountTypeId] = @DiscountTypeId,
			[DiscountAmount] = @DiscountAmount,
			[DiscountTotal] = @DiscountTotal,
			[Total] = @Total
		WHERE BusinessId = @BusinessId AND Id = @Id AND IsActive = 1
						
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@ModifyBy,'Invoices','spu_Invoices_Update',@Error;
		THROW;

	END CATCH 
END
