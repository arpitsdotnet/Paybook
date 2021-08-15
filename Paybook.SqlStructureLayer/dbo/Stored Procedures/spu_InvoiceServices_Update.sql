CREATE PROCEDURE [dbo].[spu_InvoiceServices_Update]
	@Id				INT,
    @BusinessId		INT,
    @ModifyBy		NVARCHAR(256),
	@InvoiceId		NVARCHAR(50),
	@Name			NVARCHAR(256),
	@WorkTypeId		INT,
	@VehicleNumber	NVARCHAR(50),
	@Qty			INT,
	@Rate			DECIMAL(18,2),
	@Subtotal		DECIMAL(18,2),
	@OrderBy		INT,
	@IsTaxable		BIT,
	@TaxTypeId		INT,
	@IGSTPercentage	INT,
	@IGSTAmount		DECIMAL(18, 2),
	@CGSTPercentage	INT,
	@CGSTAmount		DECIMAL(18, 2),
	@SGSTPercentage	INT,
	@SGSTAmount		DECIMAL(18, 2),
	@TaxableTotal	DECIMAL(18, 2),
	@Total			DECIMAL(18, 2)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION

		UPDATE [dbo].[InvoiceServices]
		SET
			[ModifyDate] = GETDATE(),
			[ModifyBy] = @ModifyBy,
			[InvoiceId] = @InvoiceId,
			[Name] = @Name,
			[WorkTypeId] = @WorkTypeId,
			[VehicleNumber] = @VehicleNumber,
			[Qty] = @Qty,
			[Rate] = @Rate,
			[Subtotal] = @Subtotal,
			[OrderBy] = @OrderBy,
			[IsTaxable] = @IsTaxable,
			[TaxTypeId] = @TaxTypeId,
			[IGSTPercentage] = @IGSTPercentage,
			[IGSTAmount] = @IGSTAmount,
			[CGSTPercentage] = @CGSTPercentage,
			[CGSTAmount] = @CGSTAmount,
			[SGSTPercentage] = @SGSTPercentage,
			[SGSTAmount] = @SGSTAmount,
			[TaxableTotal] = @TaxableTotal,
			[Total] = @Total
		WHERE [BusinessId] = @BusinessId AND [Id] = @Id AND IsActive = 1;				
			 		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@ModifyBy,'InvoiceServices','spu_InvoiceServices_Update',@Error;
		THROW;

	END CATCH 
END
