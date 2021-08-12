CREATE PROCEDURE [dbo].[sps_StateMaster_GetById]
	@Id INT
AS
BEGIN
	SELECT [Id],[Name],[CreateDate],[CreateBy],[OrderBy]
	FROM StateMaster
	WHERE Id = @Id AND IsActive = 1;
END
