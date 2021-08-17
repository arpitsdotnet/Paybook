CREATE PROCEDURE [dbo].[spi_InvoiceServices_Insert]
(
	@Id				INT = 0 OUTPUT,
    @BusinessId		INT,
	@IsActive		BIT,
	@CreateDate		DATETIME,
    @CreateBy		NVARCHAR(256),
	@ModifyDate		DATETIME,
    @ModifyBy		NVARCHAR(256),
	@InvoiceId		INT,
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
)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION
						
		INSERT INTO [dbo].[InvoiceServices]([BusinessId],[IsActive],[CreateDate],[CreateBy],[InvoiceId],[Name],[WorkTypeId],[VehicleNumber],[Qty],[Rate],[Subtotal],[OrderBy],
						[IsTaxable],[TaxTypeId],[IGSTPercentage],[IGSTAmount],[CGSTPercentage],[CGSTAmount],[SGSTPercentage],[SGSTAmount],[TaxableTotal],[Total])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@InvoiceId,@Name,@WorkTypeId,@VehicleNumber,@Qty,@Rate,@Subtotal,@OrderBy,
						@IsTaxable,@TaxTypeId,@IGSTPercentage,@IGSTAmount,@CGSTPercentage,@CGSTAmount,@SGSTPercentage,@SGSTAmount,@TaxableTotal,@Total)
			 
		SET @Id = SCOPE_IDENTITY();
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@CreateBy,'InvoiceServices','spi_InvoiceServices_Insert',@Error;
		THROW;

	END CATCH 
END
