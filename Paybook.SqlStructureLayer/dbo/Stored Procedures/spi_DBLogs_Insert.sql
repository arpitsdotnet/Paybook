CREATE PROCEDURE [dbo].[spi_DBLogs_Insert]
(
	@BusinessId INT = 0,
	@CreateBy NVARCHAR(256),
	@TableName VARCHAR(50),
	@StoredProcedureName VARCHAR(256),
	@ErrorMessage VARCHAR(512)
)
AS
BEGIN

	INSERT INTO [dbo].DBLogs([BusinessId],[CreateDate],[CreateBy],[TableName],[StoredProcedureName],[ErrorMessage])
	VALUES(@BusinessId,GETDATE(),@CreateBy,@TableName,@StoredProcedureName,@ErrorMessage);
	
	SELECT @ErrorMessage AS ErrorMessage;
END
Go