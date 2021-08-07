CREATE PROCEDURE [dbo].[spi_Activities_Insert]
	@BusinessId int,
	@CreateBy VARCHAR(50),
	@UserId int,
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

		INSERT INTO [dbo].[Activities]([BusinessId],[IsActive],[CreateDate],[CreateBy],[UserId],[Status],[Text],[TextHtml])
			 VALUES(@BusinessId,1,GETDATE(),@CreateBy,@UserId,@Status,@Text,@TextHtml)

		SELECT SCOPE_IDENTITY() AS ID;
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		EXEC [dbo].[spi_DBLogs_Insert] @BusinessId,'Activities','spi_Activities_Insert',ERROR_MESSAGE;

	END CATCH 
END
GO