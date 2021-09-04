CREATE PROCEDURE [dbo].[sps_InvoicePayments_GetAllByInvoiceId]
	@BusinessId INT,
	@InvoiceId INT,
	@Page INT = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN
	IF(@Page = 0)
	BEGIN
		SELECT [Id],[CreateDate],[PayDate],[PayAmount]
		FROM InvoicePayments 
		WHERE BusinessId = @BusinessId AND InvoiceId = @InvoiceId AND IsActive = 1
		ORDER BY [PayDate] DESC;
	END
	ELSE
	BEGIN
		DECLARE @RowDisplay INT = 10;

		SELECT [Id],[CreateDate],[PayDate],[PayAmount]
		FROM InvoicePayments
		WHERE BusinessId = @BusinessId AND InvoiceId = @InvoiceId AND IsActive = 1
			--cl.[Name] LIKE '%'+@Search+'%'
		--ORDER BY CASE WHEN @OrderBy = 'NameAsc' THEN cl.[Name] END ASC,
		--			CASE WHEN @OrderBy = 'NameDesc' THEN cl.[Name] END DESC,
		--			CASE WHEN @OrderBy = 'AgencyNameAsc' THEN cl.[AgencyName] END ASC,
		--			CASE WHEN @OrderBy = 'AgencyNameDesc' THEN cl.[AgencyName] END DESC
		ORDER BY [PayDate] DESC
		OFFSET (@Page * @RowDisplay) ROWS
		FETCH NEXT @RowDisplay ROWS ONLY;
	END
END
