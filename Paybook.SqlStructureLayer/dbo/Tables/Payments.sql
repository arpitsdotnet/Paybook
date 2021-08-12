CREATE TABLE [dbo].[Payments]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    [ModifyBy] NVARCHAR(256) NULL, 
    [PaymentDate] DATETIME NOT NULL, 
    [IsSuccess] BIT NULL, 
    [Method] NVARCHAR(50) NULL,
    [Amount] DECIMAL(18, 2) NULL, 
    [IsRefund] BIT NULL, 
    [Attempts] INT NULL, 
    CONSTRAINT [PK_Payments] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Payments_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
)
