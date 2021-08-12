CREATE PROCEDURE [dbo].[sps_Businesses_GetById]
	@Id INT,
	@Username NVARCHAR(256)
AS
BEGIN
	SELECT b.[Id],b.[IsActive],b.[CreateDate],b.[CreateBy],b.[Name],b.[Description],b.[IsSelected],b.[Image],b.[GSTNumber],
		b.[PhoneNumber],b.[Email],b.[AddressLine1],b.[AddressLine2],b.[City],b.[StateId],b.[CountryId],b.[Pincode]
	FROM Businesses b
		INNER JOIN UserBusinesses ub ON ub.BusinessId = b.Id
		INNER JOIN IdentityUsers u ON u.Id = ub.UserId
	WHERE b.Id = @Id AND u.Username = @Username;
END
