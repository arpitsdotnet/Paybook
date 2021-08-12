CREATE TABLE [dbo].[CategoryTypeMaster]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    [ModifyBy] NVARCHAR(256) NULL, 
    [Name] NVARCHAR(256) NULL,     
    [Core] NVARCHAR(256) NULL, 
    CONSTRAINT [PK_CategoryTypeMaster] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_CategoryTypeMaster_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
)
