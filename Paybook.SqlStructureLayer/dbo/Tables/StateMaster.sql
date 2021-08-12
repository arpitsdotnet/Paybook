CREATE TABLE [dbo].[StateMaster]
(
    [Id] INT NOT NULL IDENTITY, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    [ModifyBy] NVARCHAR(256) NULL, 
    [CountryId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL,
    [OrderBy] INT NULL, 
    CONSTRAINT [PK_StateMaster] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_StateMaster_ToCountryMaster_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [CountryMaster]([Id]), 
)
