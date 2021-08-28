CREATE PROCEDURE [dbo].[sps_Dashboard_GetCounters]
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
				StatusId <> @InvoiceVoidId) 
			AS CountTotalOpenInvoice,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoiceVoidId) 
			AS SumofTotalOpenInvoice,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoiceVoidId AND (InvoiceDate BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE())) 				
			AS CountLastWeekOpenInvoice,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoiceVoidId AND (InvoiceDate BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE())) 
			AS SumLastWeekOpenInvoice,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId AND (DueDate < GETDATE())) 
			AS CountOfOverdue,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices 
				WHERE  BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId AND (DueDate < GETDATE()))
			AS SumOfOverdue,

			(SELECT Count(pay.Id) FROM Payments AS pay 
				INNER JOIN InvoicePayments AS invpay ON pay.Id = invpay.PaymentId
				INNER JOIN Invoices AS inv ON invpay.InvoiceId = inv.Id
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PaymentDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()) AND
				inv.StatusId = @InvoicePartialPaidId)
			AS CountOfPaidPartial,
			(SELECT SUM(ISNULL(pay.Amount,0)) FROM Payments AS pay 
				INNER JOIN InvoicePayments AS invpay ON pay.Id = invpay.PaymentId
				INNER JOIN Invoices AS inv ON invpay.InvoiceId = inv.Id
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PaymentDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()) AND
				inv.StatusId = @InvoicePartialPaidId)
			AS SumOfPaidPartialAmount,

			(SELECT Count(pay.Id) FROM Payments AS pay 
				INNER JOIN InvoicePayments AS invpay ON pay.Id = invpay.PaymentId
				INNER JOIN Invoices AS inv ON invpay.InvoiceId = inv.Id 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PaymentDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()) AND		
				inv.StatusId = @InvoicePaidId) 
			AS CountOfPaidAmount,
			(SELECT SUM(ISNULL(pay.Amount,0)) FROM Payments AS pay 
				INNER JOIN InvoicePayments AS invpay ON pay.Id = invpay.PaymentId
				INNER JOIN Invoices AS inv ON invpay.InvoiceId = inv.Id
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PaymentDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()) AND		
				inv.StatusId = @InvoicePaidId) 
			AS SumOfPaidAmount,

			(SELECT Count(inv.Id) FROM Invoices AS inv
				WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND 	
				inv.StatusId = @InvoicePaidId OR inv.StatusId = @InvoicePartialPaidId) 
			AS CountOfPaymentTotal,
			(SELECT ISNULL(SUM(pay.Amount),0) FROM Invoices AS inv
				LEFT JOIN InvoicePayments AS ipay ON inv.Id = ipay.InvoiceId
				LEFT JOIN Payments AS pay ON ipay.PaymentId = pay.Id 
				WHERE inv.BusinessId = @BusinessId AND inv.IsActive = 1 AND 	
				inv.StatusId = @InvoicePaidId OR inv.StatusId = @InvoicePartialPaidId) 
			AS SumOfPaymentTotal,

			(SELECT Count(Id) FROM Clients 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(CreateDate BETWEEN  DATEADD(DAY, -7, GETDATE()) AND GETDATE())) as CountofCustomers;
END
