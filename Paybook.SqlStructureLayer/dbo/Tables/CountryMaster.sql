CREATE TABLE [dbo].[CountryMaster]
(
    [Id] INT NOT NULL IDENTITY,
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_CountryMaster] PRIMARY KEY ([Id]), 
)
