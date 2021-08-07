CREATE FUNCTION [dbo].[fns_Category_GetByCore]
(
	@BusinessId int,
	@TypeCore NVARCHAR(256),
	@CategoryCore NVARCHAR(256)
)
RETURNS TABLE
AS
RETURN
	SELECT c.[Id],c.[Name],c.[OrderBy]
	FROM CategoryMaster c
		INNER JOIN CategoryTypeMaster ct ON c.CategoryTypeId = ct.Id
	WHERE c.BusinessId = @BusinessId AND ct.BusinessId = @BusinessId AND ct.Core = @TypeCore AND c.Core = @CategoryCore 