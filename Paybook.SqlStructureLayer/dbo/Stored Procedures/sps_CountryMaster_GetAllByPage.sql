CREATE PROCEDURE [dbo].[sps_CountryMaster_GetAllByPage]
	@Page int = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN

	IF(@Page = 0)
	BEGIN
		SELECT [Id],[Name]
		FROM CountryMaster 
		WHERE IsActive = 1
		ORDER BY [Name] ASC;
	END
	ELSE
	BEGIN
	DECLARE @RowDisplay INT = 10;

		SELECT [Id],[IsActive],[CreateDate],[CreateBy],[Name]
		FROM CountryMaster
		WHERE IsActive = 1 AND [Name] LIKE '%'+@Search+'%'
		ORDER BY CASE WHEN @OrderBy = 'NameAsc' THEN [Name] END ASC,
				 CASE WHEN @OrderBy = 'NameDesc' THEN [Name] END DESC
		OFFSET (@Page * @RowDisplay) ROWS
		FETCH NEXT @RowDisplay ROWS ONLY
	END
END
