CREATE PROCEDURE [dbo].[spi_Activities_Insert]
	@Id INT = 0 OUTPUT,
	@BusinessId int,
	@CreateBy VARCHAR(256),
	@Status NVARCHAR(50),
	@Text TEXT,
	@TextHtml TEXT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY		
		BEGIN TRANSACTION

		INSERT INTO [dbo].[Activities]([BusinessId],[IsActive],[CreateDate],[CreateBy],[Status],[Text],[TextHtml])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@Status,@Text,@TextHtml)
			 
		SELECT @Id = SCOPE_IDENTITY();
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,@CreateBy,'Activities','spi_Activities_Insert',@Error;

	END CATCH 
END
GO