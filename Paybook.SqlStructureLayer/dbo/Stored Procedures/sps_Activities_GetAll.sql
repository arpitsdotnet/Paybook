CREATE PROCEDURE [dbo].[sps_Activities_GetAll]
	@BusinessId int = 0,
	@UserId int = 0
AS
BEGIN

	IF(@UserId = 0)
	BEGIN
		--USER IS ADMIN 
		SELECT act.Id, act.CreateDate, act.CreateBy, act.[Status], act.[Text], act.[TextHtml]
		FROM Activities AS act
		WHERE act.BusinessId = @BusinessId AND act.IsActive = 1
		ORDER BY act.CreateDate DESC;
	END
	ELSE
	BEGIN
		--NORMAL USER
		SELECT act.Id, act.CreateDate, act.CreateBy, act.[Status], act.[Text], act.[TextHtml]
		FROM Activities AS act
		WHERE act.BusinessId = @BusinessId AND act.UserId = @UserId AND act.IsActive = 1
		ORDER BY act.CreateDate DESC;
	END
END
GO