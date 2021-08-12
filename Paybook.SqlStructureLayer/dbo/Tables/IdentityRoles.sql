CREATE TABLE [dbo].[IdentityRoles]
(
	[Id] INT NOT NULL IDENTITY, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
    [ModifyDate] DATETIME NULL, 
    [ModifyBy] NVARCHAR(256) NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    CONSTRAINT [PK_IdentityRoles] PRIMARY KEY ([Id]),
)
