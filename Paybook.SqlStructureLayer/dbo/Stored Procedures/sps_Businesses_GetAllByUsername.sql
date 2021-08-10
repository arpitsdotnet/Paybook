CREATE PROCEDURE [dbo].[sps_Businesses_GetAllByUsername]
	@Username NVARCHAR(256)
AS
BEGIN
	SELECT b.Id,b.[Name],b.[Description],b.[Email],b.[PhoneNumber],b.[AddressLine1],b.[AddressLine2],b.[City],b.[StateId],b.[CountryId],b.[Pincode],b.[IsSelected]
	FROM Businesses b
	INNER JOIN UserBusinesses ub ON ub.BusinessId = b.Id
	INNER JOIN IdentityUsers u ON u.Id = ub.UserId AND u.IsActive = 1
	WHERE u.Username = @Username AND b.IsActive = 1;
END