CREATE PROCEDURE [dbo].[sps_CountryMaster_GetById]
	@Id INT
AS
BEGIN
	SELECT [Id],[Name],[CreateDate],[CreateBy]
	FROM CountryMaster
	WHERE Id = @Id AND IsActive = 1;
END
