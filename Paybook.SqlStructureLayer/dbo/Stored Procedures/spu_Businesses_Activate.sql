CREATE PROCEDURE [dbo].[spu_Businesses_Activate]
	@Id INT,
	@Username NVARCHAR(256),
	@Active BIT
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION

		UPDATE Businesses
			SET
				IsActive = @Active,
				ModifyDate = GETDATE(),
				ModifyBy = @Username				
			WHERE Id = @Id AND CreateBy = @Username;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		EXEC [dbo].[spi_DBLogs_Insert] @Id,'Businesses','spu_Businesses_Selected',ERROR_MESSAGE;

	END CATCH 
END
