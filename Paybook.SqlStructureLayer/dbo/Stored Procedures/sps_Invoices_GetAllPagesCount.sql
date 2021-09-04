CREATE PROCEDURE [dbo].[sps_Invoices_GetAllPagesCount]
	@BusinessId INT,
	@Page INT = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN
	DECLARE @RowDisplay INT = 10;
	DECLARE @Count INT;

	IF(@Page = 0)
	BEGIN
		SELECT @Count = COUNT(inv.[Id])
		FROM Invoices inv
		WHERE inv.[BusinessId] = @BusinessId AND inv.[IsActive] = 1
	END
	ELSE
	BEGIN
		
		SELECT @Count = COUNT(inv.[Id])
		FROM Invoices inv
		WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 --AND
	--(inv.[InvoiceNumber] LIKE '%'+@Search+'%' OR inv.[Description] LIKE '%'+@Search+'%')			
	END

	IF(@Count % @RowDisplay = 0)
		SELECT @Count / @RowDisplay;
	ELSE
		SELECT (@Count / @RowDisplay) + 1
END