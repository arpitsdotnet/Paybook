CREATE PROCEDURE [dbo].[sps_LastSavedNumbers_GetNewNumberByType]
	@BusinessId INT,
	@Type NVARCHAR(256)
AS
BEGIN
	BEGIN TRY		
	BEGIN TRANSACTION

		DECLARE @Number INT = 0, @NewNumber INT = 0, @CurrentYear INT = DATEPART(YEAR, GETDATE()), @CurrentMonth INT = DATEPART(MONTH, GETDATE());
		--SELECT * FROM @Months

		-- CHECK IF NUMBER EXISTS
		IF EXISTS(SELECT [Id] FROM LastSavedNumbers WHERE [BusinessId] = @BusinessId AND [Type] = @Type AND IsActive = 1)
		BEGIN
			DECLARE @LastYear INT = 0, @LastMonth VARCHAR(1) = ''; 

			SELECT @Number = CAST([LastNumber] AS INT), @LastYear = CAST([Year] AS INT), @LastMonth = CAST([Month] AS INT)
			FROM LastSavedNumbers 
			WHERE [BusinessId] = @BusinessId AND [Type] = @Type AND IsActive = 1


			-- UPDATE IF NUMBER EXISTS
			UPDATE LastSavedNumbers 
			SET 
				[Year] = @CurrentYear, 
				[Month] = @CurrentMonth, 
				[LastNumber] = [dbo].[fns_NewNumberGenerator](@Number,@LastYear,@LastMonth)
			WHERE [BusinessId] = @BusinessId AND [Type] = @Type AND IsActive = 1;
		END
		ELSE
		BEGIN
			-- INSERT IF NUMBER NOT EXISTS
			DECLARE @TypeInsert NVARCHAR(10) = SUBSTRING(@Type,1,3)
			INSERT INTO LastSavedNumbers([BusinessId],[IsActive],[Type],[Prefix],[Year],[Month],[LastNumber],[Seperator])
			VALUES (@BusinessId,1,@Type,UPPER(@TypeInsert),@CurrentYear,@CurrentMonth,'0001','/')
		END
	
		DECLARE @Months TABLE ([Id] INT, [Month] VARCHAR(1));
		INSERT @Months([Id],[Month]) 
		VALUES(1,'A'),(2,'B'),(3,'C'),(4,'D'),(5,'E'),(6,'F'),(7,'G'),(8,'H'),(9,'I'),(10,'J'),(11,'K'),(12,'L'); 

		DECLARE @MonthLetter NVARCHAR(1);
		(SELECT @MonthLetter = [Month] FROM @Months WHERE [Id] = DATEPART(MONTH, GETDATE()))

		-- SELECT THE NUMBER
		SELECT [Prefix],[Year],@MonthLetter AS [Month],[LastNumber],[Seperator],
				CONCAT([Prefix],[Seperator],[Year],[Seperator],@MonthLetter,[LastNumber]) AS [NewNumber]
		FROM LastSavedNumbers
		WHERE [BusinessId] = @BusinessId AND [Type] = @Type AND IsActive = 1;
			
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH		
		
		ROLLBACK TRANSACTION
		
		DECLARE @Error NVARCHAR(256) = ERROR_MESSAGE();
		EXEC [dbo].[spi_DBLogs_Insert] 0,'admin','LastSavedNumbers','sps_LastSavedNumbers_GetNewNumberByType',@Error;

		THROW;

	END CATCH 
END