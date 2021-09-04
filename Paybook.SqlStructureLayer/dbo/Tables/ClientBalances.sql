CREATE TABLE [dbo].[ClientBalances]
(
	[Id] INT NOT NULL PRIMARY KEY  IDENTITY, 
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    [ModifyBy] NVARCHAR(256) NULL, 
    [ClientId] INT NULL, 
    [Balance] DECIMAL(18, 2) NULL, 
    CONSTRAINT [FK_ClientBalances_ToClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients]([Id])
)
