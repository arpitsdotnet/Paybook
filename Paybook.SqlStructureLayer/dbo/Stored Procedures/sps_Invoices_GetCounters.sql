CREATE PROCEDURE [dbo].[sps_Invoices_GetCounters]
	@BusinessId int
AS
BEGIN
	
	--Open | Sent | PaidPartial | Paid | Void | WriteOff
	
	DECLARE @InvoiceOpenId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Open'));
	DECLARE @InvoicePartialPaidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','PaidPartial'));
	DECLARE @InvoicePaidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Paid'));
	DECLARE @InvoiceVoidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Void'));
	DECLARE @InvoiceWriteOffId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','WriteOff'));
	
	
	DECLARE @InvoiceTotal DECIMAL(18,2) = (SELECT ISNULL(SUM(Total),0) FROM Invoices 
											WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
											(StatusId <> @InvoiceVoidId))
	DECLARE @PaymentTotal DECIMAL(18,2) = (SELECT ISNULL(SUM(ipay.PayAmount),0) FROM Invoices AS inv
											LEFT JOIN InvoicePayments AS ipay ON inv.Id = ipay.InvoiceId
											WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND 	
											inv.StatusId = @InvoicePaidId OR inv.StatusId = @InvoicePartialPaidId);

	print @PaymentTotal

	SELECT (SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(StatusId <> @InvoiceVoidId)) 
			AS CountOfInvoices,
			(@InvoiceTotal) 
			AS SumOfInvoices,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId) AND (DueDate < GETDATE())) 
			AS CountOfOverdue,
			(SELECT ISNULL(SUM(Total),0) FROM Invoices 
				WHERE  BusinessId = @BusinessId AND IsActive = 1 AND 
				(StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId) AND (DueDate < GETDATE()))
			AS SumOfOverdue,
			
			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId)) 
			AS CountOfUnpaid,
			(@InvoiceTotal - @PaymentTotal)
			AS SumOfUnpaid,

			(SELECT Count(inv.Id) FROM Invoices AS inv
				WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND 	
				inv.StatusId = @InvoicePaidId OR inv.StatusId = @InvoicePartialPaidId) 
			AS CountOfPaid,
			(@PaymentTotal) 
			AS SumOfPaid;
END
