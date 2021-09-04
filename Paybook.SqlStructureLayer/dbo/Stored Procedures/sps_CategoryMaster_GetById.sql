CREATE PROCEDURE [dbo].[sps_CategoryMaster_GetById]
	@BusinessId INT,
	@Id INT
AS
BEGIN
	SELECT cm.[Id], cm.[Name], cm.[Value], cm.[Color]
	FROM CategoryMaster cm
	WHERE cm.BusinessId = @BusinessId AND cm.Id = @Id AND cm.IsActive = 1
END
