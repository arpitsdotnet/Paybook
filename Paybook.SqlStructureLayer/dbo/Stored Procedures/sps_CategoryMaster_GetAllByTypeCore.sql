CREATE PROCEDURE [dbo].[sps_CategoryMaster_GetAllByTypeCore]
	@BusinessId INT,
	@TypeCore NVARCHAR(256)
AS
BEGIN
	SELECT cm.[Id], cm.[Name], cm.[Value], cm.[Color]
	FROM CategoryMaster cm
		INNER JOIN CategoryTypeMaster ctm ON cm.CategoryTypeId = ctm.Id
	WHERE cm.BusinessId = @BusinessId AND ctm.Core = @TypeCore AND cm.IsActive = 1
	ORDER BY cm.OrderBy ASC;
END
