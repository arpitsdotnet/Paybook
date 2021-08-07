CREATE PROCEDURE [dbo].[sps_Clients_GetAllByPage]
	@BusinessId int,
	@Page int = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN
	DECLARE @RowDisplay INT = 10;

	SELECT cl.[Id],cl.[CreateDate],cl.[Name],cl.[PhoneNumber1],cl.[PhoneNumber2],cl.[Email],
		cl.[AddressLine1],cl.[AddressLine2],cl.[City],cl.[State],sm.[Name] AS [StateName],cl.[Country],cm.[Name] AS [CountryName],cl.[Pincode]
	FROM Clients cl
		LEFT JOIN StateMaster sm ON cl.[State] = sm.Id
		LEFT JOIN CountryMaster cm ON cl.[Country] = cm.Id
	WHERE cl.BusinessId = @BusinessId AND cl.IsActive = 1 AND
		cl.[Name] LIKE '%'+@Search+'%'
	ORDER BY CASE WHEN @OrderBy = 'NameAsc' THEN cl.[Name] END ASC,
			 CASE WHEN @OrderBy = 'NameDesc' THEN cl.[Name] END DESC
	OFFSET (@Page * @RowDisplay) ROWS
	FETCH NEXT @RowDisplay ROWS ONLY
END
