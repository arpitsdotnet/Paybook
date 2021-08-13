CREATE FUNCTION [dbo].[fns_NewNumberGenerator] 
(
	@Number INT,
	@LastYear INT, --FOR FUTURE
	@LastMonth INT
)
RETURNS NVARCHAR(20)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result NVARCHAR(20)
		
	DECLARE @CurrentYear INT = DATEPART(YEAR, GETDATE()); -- FOR FUTURE
	DECLARE @CurrentMonth INT = DATEPART(MONTH, GETDATE());

	IF(@LastYear <> @CurrentYear) --IF YEAR DOES NOT MATCH
		SET @Number = 1;
	IF(@LastMonth <> @CurrentMonth) --IF MONTH DOES NOT MATCH
		SET @Number = 1;
	ELSE
		SET @Number = @Number + 1

	IF(LEN(@Number) = 1)
		SET @Result = ('000' + CAST(@Number AS VARCHAR))
	ELSE IF(LEN(@Number) = 2)
		SET @Result = ('00' + CAST(@Number AS VARCHAR))
	ELSE IF(LEN(@Number) = 3)
		SET @Result = ('0' + CAST(@Number AS VARCHAR))
	ELSE
		SET @Result = (CAST(@Number AS VARCHAR))

	RETURN @Result

END