CREATE PROCEDURE [dbo].[sps_Clients_GetCountersById]
	@BusinessId INT,
	@Id INT
AS
BEGIN
	
	DECLARE @InvoiceOpenId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Open'));
	DECLARE @InvoicePartialPaidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','PaidPartial'));
	DECLARE @InvoicePaidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Paid'));
	DECLARE @InvoiceVoidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Void'));
	DECLARE @InvoiceWriteOffId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','WriteOff'));
		
		
		DECLARE @Total DECIMAL(18,2) = (SELECT ISNULL(SUM(inv.Total),0)
								FROM Invoices AS inv
								WHERE inv.BusinessId = 10 AND inv.ClientId = @Id AND inv.IsActive = 1 AND 
											(inv.StatusId <> @InvoiceVoidId));

	DECLARE @PayAmount DECIMAL(18,2) = (SELECT ISNULL(SUM(ipay.PayAmount),0)
								FROM InvoicePayments AS ipay 
								INNER JOIN Invoices AS inv ON ipay.InvoiceId = inv.Id
								WHERE ipay.BusinessId = 10 AND inv.ClientId = @Id AND ipay.IsActive = 1 AND 
											(inv.StatusId <> @InvoiceVoidId));

	SELECT (@Total) AS [OpenTotal],(@Total - @PayAmount) AS [UnpaidTotal],

			(SELECT ISNULL(SUM(Total),0) FROM Invoices 
				WHERE  BusinessId = @BusinessId AND ClientId = @Id AND IsActive = 1 AND 
				(StatusId <> @InvoicePaidId AND StatusId <> @InvoiceWriteOffId AND StatusId <> @InvoiceVoidId) AND (DueDate < GETDATE()))
			AS [OverdueTotal],

			(SELECT ISNULL(SUM(pay.Amount),0) FROM Payments AS pay
				INNER JOIN ClientPayments AS cpay ON pay.Id = cpay.PaymentId
				INNER JOIN Clients AS cl ON cpay.ClientId = cl.Id
				WHERE pay.BusinessId = @BusinessId AND cl.Id = @Id AND pay.IsActive = 1) 
			AS [PaymentTotal],

			(SELECT ISNULL(SUM(bal.Balance),0) FROM ClientBalances AS bal
				LEFT JOIN Clients AS cl ON bal.ClientId = cl.Id
				WHERE cl.BusinessId = @BusinessId AND cl.Id = @Id AND cl.IsActive = 1) 
			AS [BalanceTotal];
END
