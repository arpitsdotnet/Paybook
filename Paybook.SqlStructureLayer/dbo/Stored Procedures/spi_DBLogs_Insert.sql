CREATE PROCEDURE [dbo].[spi_DBLogs_Insert]
(
	@BusinessId int = 0,
	@TableName VARCHAR(50),
	@StoredProcedureName VARCHAR(256),
	@ErrorMessage VARCHAR(512)
)
AS
BEGIN

	INSERT INTO [dbo].DBLogs([BusinessId],[CreateDate],[TableName],[StoredProcedureName],[ErrorMessage])
	VALUES(@BusinessId,GETDATE(),@TableName,@StoredProcedureName,@ErrorMessage);
	
	SELECT @ErrorMessage AS ErrorMessage;
END
Go