CREATE PROCEDURE [dbo].[spu_Businesses_UpdateSelected]
	@Id INT,
	@CreateBy NVARCHAR(256)
AS
BEGIN
	BEGIN TRY		
		BEGIN TRANSACTION

		DECLARE @PrevSelectedId INT;
		SELECT @PrevSelectedId = Id 
		FROM Businesses
		WHERE CreateBy = @CreateBy AND IsSelected = 1 AND IsActive = 1;

		IF EXISTS(SELECT @PrevSelectedId)
		BEGIN
			UPDATE Businesses
			SET
				IsSelected = 0
			WHERE CreateBy = @CreateBy AND IsSelected = 1 AND IsActive = 1;
		END
				
		UPDATE Businesses
		SET
			IsSelected = 1
		WHERE Id = @Id AND CreateBy = @CreateBy AND IsActive = 1;
				
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		EXEC [dbo].[spi_DBLogs_Insert] @Id,'Businesses','spu_Businesses_Selected',ERROR_MESSAGE;

	END CATCH 
END
