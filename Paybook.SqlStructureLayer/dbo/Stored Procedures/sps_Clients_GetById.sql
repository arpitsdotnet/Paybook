CREATE PROCEDURE [dbo].[sps_Clients_GetById]
	@BusinessId INT,
	@Id INT
AS
BEGIN
	SELECT cl.[Id],cl.[IsActive],cl.[CreateDate],cl.[CreateBy],cl.[Name],cl.[AgencyName],cl.[PhoneNumber1],cl.[PhoneNumber2],cl.[Email],
		cl.[AddressLine1],cl.[AddressLine2],cl.[City],cl.[StateId],cl.[CountryId],cl.[Pincode]
	FROM Clients cl
	WHERE cl.BusinessId = @BusinessId AND cl.Id = @Id;
END
