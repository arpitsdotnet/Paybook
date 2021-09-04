CREATE TABLE [dbo].[InvoicePayments]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    [ModifyBy] NVARCHAR(256) NULL, 
    [InvoiceId] INT  NOT NULL, 
    [PayDate] DATETIME NOT NULL, 
    [PayAmount] DECIMAL(18,2) NOT NULL, 
    CONSTRAINT [PK_InvoicePayments] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_InvoicePayments_ToInvoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices]([Id]), 
)
