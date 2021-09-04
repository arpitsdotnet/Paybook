CREATE TABLE [dbo].[ClientPayments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NULL, 
    [PaymentId] INT NULL, 
    CONSTRAINT [FK_ClientPayments_ToClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients]([Id]),
    CONSTRAINT [FK_ClientPayments_ToPayments_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [Payments]([Id]), 

)
