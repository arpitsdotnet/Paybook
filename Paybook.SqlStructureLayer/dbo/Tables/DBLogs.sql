CREATE TABLE [dbo].[DBLogs]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [TableName] NVARCHAR(50) NULL,
    [StoredProcedureName] NVARCHAR(256) NULL, 
    [ErrorMessage] NVARCHAR(512) NULL, 
    CONSTRAINT [PK_DBLogs] PRIMARY KEY ([Id]), 
)
