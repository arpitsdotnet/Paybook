CREATE TABLE [dbo].[StateMaster]
(
    [Id] INT NOT NULL IDENTITY, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [ModifyDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
    [ModifyBy] NVARCHAR(50) NULL, 
    [CountryId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL,
    [OrderBy] INT NULL, 
    CONSTRAINT [PK_StateMaster] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_StateMaster_ToCountryMaster_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [CountryMaster]([Id]), 
)
