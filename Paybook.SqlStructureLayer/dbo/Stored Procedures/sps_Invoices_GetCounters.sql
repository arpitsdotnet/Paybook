CREATE PROCEDURE [dbo].[sps_Invoices_GetCounters]
	@BusinessId int
AS
BEGIN
	
	--Open | Sent | PaidPartial | Paid | Void | WriteOff

	DECLARE @InvoiceOpenId INT;
	SELECT @InvoiceOpenId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Open');
	
	DECLARE @InvoicePartialPaidId INT;
	SELECT @InvoicePartialPaidId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','PaidPartial');

	DECLARE @InvoicePaidId INT;
	SELECT @InvoicePaidId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Paid');

	DECLARE @InvoiceVoidId INT;
	SELECT @InvoiceVoidId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Void');

	DECLARE @InvoiceWriteOffId INT;
	SELECT @InvoiceWriteOffId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','WriteOff');

	SELECT (SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(StatusId <> @InvoiceVoidId)) 
			AS CountOfInvoices,
			(SELECT ISNULL(SUM(Total),0) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(StatusId <> @InvoiceVoidId)) 
			AS SumOfInvoices,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId AND (DueDate < GETDATE())) 
			AS CountOfOverdue,
			(SELECT ISNULL(SUM(Total),0) FROM Invoices 
				WHERE  BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId AND (DueDate < GETDATE()))
			AS SumOfOverdue,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId)) 
			AS CountOfUnpaid,
			(SELECT (ISNULL(SUM(inv.Total),0) - ISNULL(SUM(pay.Amount),0)) FROM Invoices inv
				LEFT JOIN InvoicePayments AS ipay ON inv.Id = ipay.InvoiceId
				LEFT JOIN Payments AS pay ON ipay.PaymentId = pay.Id 
				WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND 
				(inv.StatusId <> @InvoicePaidId AND inv.StatusId <> @InvoiceVoidId)) 
			AS SumOfUnpaid,

			(SELECT Count(inv.Id) FROM Invoices AS inv
				WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND 	
				inv.StatusId = @InvoicePaidId OR inv.StatusId = @InvoicePartialPaidId) 
			AS CountOfPaid,
			(SELECT ISNULL(SUM(pay.Amount),0) FROM Invoices AS inv
				LEFT JOIN InvoicePayments AS ipay ON inv.Id = ipay.InvoiceId
				LEFT JOIN Payments AS pay ON ipay.PaymentId = pay.Id 
				WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND 	
				inv.StatusId = @InvoicePaidId OR inv.StatusId = @InvoicePartialPaidId) 
			AS SumOfPaid;
END
