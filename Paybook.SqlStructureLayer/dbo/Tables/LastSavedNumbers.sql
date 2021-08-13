CREATE TABLE [dbo].[LastSavedNumbers]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [Type] NVARCHAR(50) NOT NULL, 
    [Prefix] NVARCHAR(5) NULL, 
    [Year] NVARCHAR(4) NULL, 
    [Month] NVARCHAR(4) NULL, 
    [LastNumber] NVARCHAR(10) NULL, 
    [Seperator] NVARCHAR(2) NULL, 
    CONSTRAINT [PK_LastSavedNumbers] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_LastSavedNumbers_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
)
