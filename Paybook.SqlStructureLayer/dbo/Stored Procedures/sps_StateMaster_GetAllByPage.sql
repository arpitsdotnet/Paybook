CREATE PROCEDURE [dbo].[sps_StateMaster_GetAllByPage]
	@CountryId INT,
	@Page int = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'OrderByAsc'
AS
BEGIN

	IF(@Page = 0)
	BEGIN
		SELECT [Id],[Name],[OrderBy]
		FROM StateMaster 
		WHERE IsActive = 1 AND CountryId = @CountryId
		ORDER BY [OrderBy] ASC;
	END
	ELSE
	BEGIN
		DECLARE @RowDisplay INT = 10;

		SELECT [Id],[IsActive],[CreateDate],[CreateBy],[Name],[OrderBy]
		FROM StateMaster
		WHERE IsActive = 1 AND CountryId = @CountryId AND [Name] LIKE '%'+@Search+'%'
		ORDER BY [OrderBy] ASC
		OFFSET (@Page * @RowDisplay) ROWS
		FETCH NEXT @RowDisplay ROWS ONLY
	END
END
