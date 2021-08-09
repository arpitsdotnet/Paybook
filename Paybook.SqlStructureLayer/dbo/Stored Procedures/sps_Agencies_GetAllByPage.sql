CREATE PROCEDURE [dbo].[sps_Agencies_GetAllByPage]
	@BusinessId int,
	@Page int = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN
	DECLARE @RowDisplay INT = 10;

	SELECT ag.[Id],ag.[CreateDate],ag.[Name],ag.[PhoneNumber1],ag.[PhoneNumber2],ag.[Email],
		ag.[AddressLine1],ag.[AddressLine2],ag.[City],ag.[StateId],sm.[Name] AS [StateName],ag.[CountryId],cm.[Name] AS [CountryName],ag.[Pincode]
	FROM Agencies ag
		LEFT JOIN StateMaster sm ON ag.[StateId] = sm.Id
		LEFT JOIN CountryMaster cm ON ag.[CountryId] = cm.Id
	WHERE ag.BusinessId = @BusinessId AND ag.IsActive = 1 AND
		ag.[Name] LIKE '%'+@Search+'%'
	ORDER BY CASE WHEN @OrderBy = 'NameAsc' THEN ag.[Name] END ASC,
			 CASE WHEN @OrderBy = 'NameDesc' THEN ag.[Name] END DESC
	OFFSET (@Page * @RowDisplay) ROWS
	FETCH NEXT @RowDisplay ROWS ONLY
END
