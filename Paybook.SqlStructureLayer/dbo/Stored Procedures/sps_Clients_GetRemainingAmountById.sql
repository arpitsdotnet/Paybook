CREATE PROCEDURE [dbo].[sps_Clients_GetRemainingAmountById]
	@BusinessId INT,
	@Id	INT
AS
BEGIN	
	--Remaining Amount = Total Invoice - Total Payment

	DECLARE @TotalInvoice DECIMAL(18,2) = 0, @TotalPayment DECIMAL(18,2) = 0;

	SELECT @TotalInvoice = SUM(ISNULL(Total,0))
	FROM Invoices 
	WHERE BusinessId = @BusinessId AND ClientId = @Id AND IsActive = 1
	
	SELECT @TotalPayment = SUM(ISNULL(pay.Amount,0))
	FROM Payments pay
		INNER JOIN InvoicePayments invp ON pay.Id = invp.PaymentId
		INNER JOIN Invoices inv ON invp.InvoiceId = inv.Id
	WHERE pay.BusinessId = @BusinessId AND inv.ClientId = @Id AND pay.IsSuccess = 1 AND pay.IsActive = 1

	SELECT (ISNULL(@TotalInvoice,0) - ISNULL(@TotalPayment,0)) AS [RemainingAmount]

END