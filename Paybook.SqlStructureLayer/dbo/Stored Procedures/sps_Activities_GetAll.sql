CREATE PROCEDURE [dbo].[sps_Activities_GetAll]
	@BusinessId INT,	
	@CreateBy VARCHAR(256)=''
AS
BEGIN

	IF(@CreateBy = '')
	BEGIN
		--USER IS ADMIN 
		SELECT TOP 20 act.Id, act.CreateDate, act.CreateBy, act.[Status], act.[Text], act.[TextHtml]
		FROM Activities AS act
		WHERE act.BusinessId = @BusinessId AND act.IsActive = 1
		ORDER BY act.CreateDate DESC;
	END
	ELSE
	BEGIN
		--NORMAL USER
		SELECT TOP 20 act.Id, act.CreateDate, act.CreateBy, act.[Status], act.[Text], act.[TextHtml]
		FROM Activities AS act
		WHERE act.BusinessId = @BusinessId AND act.CreateBy = @CreateBy AND act.IsActive = 1
		ORDER BY act.CreateDate DESC;
	END
END
GO