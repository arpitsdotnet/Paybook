CREATE PROCEDURE [dbo].[sps_Dashboard_GetCounters]
	@BusinessId int
AS
BEGIN
	--Open | Sent | PaidPartial | Paid | Void | WriteOff
		
	DECLARE @InvoiceOpenId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Open'));
	DECLARE @InvoicePartialPaidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','PaidPartial'));
	DECLARE @InvoicePaidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Paid'));
	DECLARE @InvoiceVoidId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','Void'));
	DECLARE @InvoiceWriteOffId INT = (SELECT [Id] FROM fns_Category_GetByCore(@BusinessId,'InvoiceStatus','WriteOff'));

	SELECT (SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoiceVoidId) 
			AS OpenInvoiceCount,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoiceVoidId) 
			AS OpenInvoiceTotal,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoiceVoidId AND (InvoiceDate BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE())) 				
			AS OpenInvoiceLastWeekCount,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoiceVoidId AND (InvoiceDate BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE())) 
			AS OpenInvoiceLastWeekTotal,

			(SELECT COUNT(Id) FROM Invoices 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId AND (DueDate < GETDATE())) 
			AS OverdueInvoiceCount,
			(SELECT SUM(ISNULL(Total,0)) FROM Invoices 
				WHERE  BusinessId = @BusinessId AND IsActive = 1 AND 
				StatusId <> @InvoicePaidId AND StatusId <> @InvoiceVoidId AND (DueDate < GETDATE()))
			AS OverdueInvoiceTotal,

			(SELECT Count(pay.Id) FROM InvoicePayments AS pay 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PayDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()))
			AS PartialPaidInvoiceCount,
			(SELECT SUM(ISNULL(pay.PayAmount,0)) FROM InvoicePayments AS pay 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PayDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()))
			AS PartialPaidInvoiceTotal,

			(SELECT Count(pay.Id) FROM InvoicePayments AS pay 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PayDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE())) 
			AS PaidInvoiceCount,
			(SELECT SUM(ISNULL(pay.PayAmount,0)) FROM InvoicePayments AS pay 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1 AND 
				(pay.PayDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE()) ) 
			AS PaidInvoiceTotal,

			(SELECT Count(pay.Id) FROM Payments AS pay 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1) 
			AS DepositCount,
			(SELECT SUM(ISNULL(pay.Amount,0)) FROM Payments AS pay 
				WHERE pay.BusinessId = @BusinessId AND pay.IsActive = 1) 
			AS DepositTotal,

			(SELECT Count(Id) FROM Clients 
				WHERE BusinessId = @BusinessId AND IsActive = 1 AND 
				(CreateDate BETWEEN  DATEADD(DAY, -7, GETDATE()) AND GETDATE())) as CustomerCount;

END