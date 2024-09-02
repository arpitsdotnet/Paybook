CREATE PROCEDURE [dbo].[sps_Businesses_GetById]
	@Id INT
AS
BEGIN
	SELECT b.[Id],b.[IsActive],b.[CreateDate],b.[CreateBy],b.[Name],b.[Description],b.[IsSelected],b.[Image],b.[GSTNumber],
		b.[PhoneNumber],b.[Email],b.[AddressLine1],b.[AddressLine2],b.[City],b.[StateId],b.[CountryId],b.[Pincode]
	FROM Businesses b
	WHERE b.Id = @Id
END
