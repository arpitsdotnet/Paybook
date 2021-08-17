CREATE PROCEDURE [dbo].[sps_Dashboard_GetCounters]
	@BusinessId int
AS
BEGIN
	DECLARE @InvoiceOpenId INT;
	SELECT @InvoiceOpenId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Open');
	
	DECLARE @InvoicePartialPaidId INT;
	SELECT @InvoicePartialPaidId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','PaidPartial');

	DECLARE @InvoicePaidId INT;
	SELECT @InvoicePaidId = [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Paid');

	SELECT (SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId = @InvoiceOpenId) 
			AS CountTotalOpenInvoice,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId = @InvoiceOpenId) 
			AS SumofTotalOpenInvoice,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId = @InvoiceOpenId AND (InvoiceDate BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE())) 				
			AS CountLastWeekOpenInvoice,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId = @InvoiceOpenId AND (InvoiceDate BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE())) 
			AS SumLastWeekOpenInvoice,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND (DueDate < GETDATE())) 
			AS CountOfOverdue,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices 
				WHERE  BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND (DueDate < GETDATE()))
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

			(SELECT Count(pay.Id) FROM Payments AS pay 
				INNER JOIN InvoicePayments AS invpay ON pay.Id = invpay.PaymentId
				INNER JOIN Invoices AS inv ON invpay.InvoiceId = inv.Id 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 		
				inv.StatusId = @InvoicePaidId) 
			AS CountOfPaymentTotal,
			(SELECT SUM(ISNULL(pay.Amount,0)) FROM Payments AS pay 
				INNER JOIN InvoicePayments AS invpay ON pay.Id = invpay.PaymentId
				INNER JOIN Invoices AS inv ON invpay.InvoiceId = inv.Id
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 		
				inv.StatusId = @InvoicePaidId) 
			AS SumOfPaymentTotal,

			(SELECT Count(Id) FROM Clients 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(CreateDate BETWEEN  DATEADD(DAY, -7, GETDATE()) AND GETDATE())) as CountofCustomers;
END
