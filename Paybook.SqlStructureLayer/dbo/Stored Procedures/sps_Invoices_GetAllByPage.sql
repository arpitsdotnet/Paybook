CREATE PROCEDURE [dbo].[sps_Invoices_GetAllByPage]
	@BusinessId INT,
	@Page INT = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN
	IF(@Page = 0)
	BEGIN
		SELECT inv.[Id],inv.[InvoiceNumber],inv.[Description],inv.[InvoiceDate],inv.[StatusId],inv.[ClientId],cl.[Name],inv.[DueDate],inv.[IsOverdue],inv.[Total]
		FROM Invoices inv
			INNER JOIN Clients cl ON inv.[ClientId] = cl.[Id]
		WHERE inv.[BusinessId] = @BusinessId AND inv.[IsActive] = 1
		ORDER BY [InvoiceDate] DESC;
	END
	ELSE
	BEGIN
		DECLARE @RowDisplay INT = 10;

		SELECT inv.[Id],inv.[InvoiceNumber],inv.[Description],inv.[InvoiceDate],inv.[StatusId],inv.[ClientId],cl.[Name],inv.[DueDate],inv.[IsOverdue],inv.[Total]
		FROM Invoices inv
			INNER JOIN Clients cl ON inv.[ClientId] = cl.[Id]
		WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND
			(inv.[InvoiceNumber] LIKE '%'+@Search+'%' OR inv.[Description] LIKE '%'+@Search+'%')
		ORDER BY inv.[InvoiceDate] DESC
		OFFSET (@Page * @RowDisplay) ROWS
		FETCH NEXT @RowDisplay ROWS ONLY;	
	END
END