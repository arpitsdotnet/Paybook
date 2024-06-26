﻿CREATE PROCEDURE [dbo].[sps_Payments_GetAllByClientId]
	@BusinessId int,
	@ClientId int,
	@Page int = 0,
	@Search NVARCHAR(50) = '',
	@OrderBy NVARCHAR(50) = 'NameAsc'
AS
BEGIN
	IF(@Page = 0)
	BEGIN
		SELECT pay.[Id],pay.[CreateDate],pay.[PaymentDate],pay.[IsSuccess],pay.[Method],pay.[Amount],pay.[IsRefund],pay.[Attempts]
		FROM Payments pay
			INNER JOIN ClientPayments cpay ON pay.Id = cpay.PaymentId
		WHERE pay.BusinessId = @BusinessId AND cpay.ClientId = @ClientId AND pay.IsActive = 1
		ORDER BY pay.[PaymentDate] DESC;
	END
	ELSE
	BEGIN
		DECLARE @RowDisplay INT = 10;

		SELECT pay.[Id],pay.[CreateDate],pay.[PaymentDate],pay.[IsSuccess],pay.[Method],pay.[Amount],pay.[IsRefund],pay.[Attempts]
		FROM Payments pay
			INNER JOIN ClientPayments cpay ON pay.Id = cpay.PaymentId
		WHERE pay.BusinessId = @BusinessId AND cpay.ClientId = @ClientId AND pay.IsActive = 1 --AND
			--inv.[InvoiceNumber] LIKE '%'+@Search+'%'
		ORDER BY CASE WHEN @OrderBy = 'PaymentDateAsc' THEN pay.[PaymentDate] END ASC,
					CASE WHEN @OrderBy = 'PaymentDateDesc' THEN pay.[PaymentDate] END DESC,
					CASE WHEN @OrderBy = 'AmountAsc' THEN pay.[Amount] END ASC,
					CASE WHEN @OrderBy = 'AmountDesc' THEN pay.[Amount] END DESC
		OFFSET (@Page * @RowDisplay) ROWS
		FETCH NEXT @RowDisplay ROWS ONLY;
	END
END
