CREATE PROCEDURE [dbo].[sps_Businesses_GetAllByUserId]
	@UserId int
AS
	SELECT b.[Id], b.[CreateDate],b.[Name],b.[Description],b.[Image],b.[GSTNumber],
		b.[PhoneNumber],b.[Email],b.[AddressLine1],b.[AddressLine2],b.[City],b.[State],b.[Country],b.[Pincode]
	FROM Businesses b
		INNER JOIN UserBusinesses ub ON b.Id = ub.BusinessId
	WHERE ub.UserId = @UserId AND b.IsActive = 1 
RETURN 0
