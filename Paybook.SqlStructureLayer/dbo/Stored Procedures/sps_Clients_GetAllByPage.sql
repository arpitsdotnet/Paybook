CREATE PROCEDURE [dbo].[sps_Clients_GetAllByPage]
	@BusinessId int,
	@Page int = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN
	IF(@Page = 0)
	BEGIN
		SELECT [Id],[CreateDate],[Name],[AgencyName],[PhoneNumber1],[Email],[City]
		FROM Clients 
		WHERE BusinessId = @BusinessId AND IsActive = 1
		ORDER BY [Name] ASC;
	END
	ELSE
	BEGIN
		DECLARE @RowDisplay INT = 10;

		SELECT cl.[Id],cl.[CreateDate],cl.[Name],cl.[AgencyName],cl.[PhoneNumber1],cl.[PhoneNumber2],cl.[Email],
			cl.[AddressLine1],cl.[AddressLine2],cl.[City],cl.[StateId],sm.[Name] AS [StateName],cl.[CountryId],cm.[Name] AS [CountryName],cl.[Pincode]
		FROM Clients cl
			LEFT JOIN StateMaster sm ON cl.[StateId] = sm.Id
			LEFT JOIN CountryMaster cm ON cl.[CountryId] = cm.Id
		WHERE cl.BusinessId = @BusinessId AND cl.IsActive = 1 AND
			cl.[Name] LIKE '%'+@Search+'%'
		ORDER BY CASE WHEN @OrderBy = 'NameAsc' THEN cl.[Name] END ASC,
					CASE WHEN @OrderBy = 'NameDesc' THEN cl.[Name] END DESC,
					CASE WHEN @OrderBy = 'AgencyNameAsc' THEN cl.[AgencyName] END ASC,
					CASE WHEN @OrderBy = 'AgencyNameDesc' THEN cl.[AgencyName] END DESC
		OFFSET (@Page * @RowDisplay) ROWS
		FETCH NEXT @RowDisplay ROWS ONLY;
	END
END
