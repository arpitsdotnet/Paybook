CREATE PROCEDURE [dbo].[sps_InvoiceServices_GetAllByInvoiceId]
	@BusinessId INT,
	@InvoiceId INT
AS
BEGIN
	SELECT [Id],[Name],[WorkTypeId],[VehicleNumber],[Qty],[Rate],[Subtotal],[OrderBy],[IsTaxable],[TaxableTotal],[Total]
	FROM [dbo].[InvoiceServices]
	WHERE BusinessId = @BusinessId AND InvoiceId = @InvoiceId AND IsActive = 1
END