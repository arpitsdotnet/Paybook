CREATE TABLE [dbo].[InvoicePayments]
(
    [Id] INT NOT NULL IDENTITY,
    [InvoiceId] INT  NOT NULL, 
    [PaymentId] INT NOT NULL, 
    CONSTRAINT [PK_InvoicePayments] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_InvoicePayments_ToInvoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices]([Id]), 
    CONSTRAINT [FK_InvoicePayments_ToPayments_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [Payments]([Id]), 
)
