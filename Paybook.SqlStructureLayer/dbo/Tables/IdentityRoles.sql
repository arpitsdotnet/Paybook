CREATE TABLE [dbo].[IdentityRoles]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(256) NOT NULL, 
    CONSTRAINT [PK_IdentityRoles] PRIMARY KEY ([Id])
)
