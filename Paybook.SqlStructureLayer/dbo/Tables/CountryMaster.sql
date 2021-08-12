CREATE TABLE [dbo].[CountryMaster]
(
    [Id] INT NOT NULL IDENTITY,
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    [ModifyBy] NVARCHAR(256) NULL,  
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_CountryMaster] PRIMARY KEY ([Id]), 
)
