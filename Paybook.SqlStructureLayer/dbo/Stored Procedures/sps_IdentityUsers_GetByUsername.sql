CREATE PROCEDURE [dbo].[sps_IdentityUsers_GetByUsername]
	@Username NVARCHAR(256)
AS
BEGIN
	SELECT [Id],[Username],[Email],[PhoneNumber],[Image],[FirstName],[LastName],[AddressLine1],[AddressLine2],[City],[StateId],[CountryId],[Pincode]
	FROM IdentityUsers
	WHERE Username = @Username AND IsActive = 1
END
