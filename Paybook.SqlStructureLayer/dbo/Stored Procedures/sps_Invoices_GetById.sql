CREATE PROCEDURE [dbo].[sps_Invoices_GetById]
	@BusinessId INT,
	@Id INT	
AS
BEGIN

	SELECT [Id],[InvoiceNumber],[Description],[InvoiceDate],[StatusId],[ClientId],[ClientEmail],[IsEmailSend],
			[IsEmailSentSuccess],[BillingAddress],[TermsId],[DueDate],[IsOverdue],[OverdueSteps],[Message],[Subtotal],
			[TaxableTotal],[DiscountTypeId],[DiscountAmount],[DiscountTotal],[Total]
	FROM Invoices 
	WHERE BusinessId = @BusinessId AND [Id] = @Id AND IsActive = 1
	ORDER BY [InvoiceDate] DESC	
	
END