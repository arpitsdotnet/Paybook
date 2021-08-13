CREATE PROCEDURE [dbo].[sps_CategoryMaster_GetByCore]
	@BusinessId INT,
	@Core NVARCHAR(256)
AS
BEGIN
	SELECT cm.[Id], cm.[Name], cm.[Value]
	FROM CategoryMaster cm
	WHERE cm.BusinessId = @BusinessId AND cm.Core = @Core AND cm.IsActive = 1
	ORDER BY cm.OrderBy ASC;
END
