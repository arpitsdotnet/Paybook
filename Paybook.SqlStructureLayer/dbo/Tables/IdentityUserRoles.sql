CREATE TABLE [dbo].[IdentityUserRoles]
(
    [Id] INT NOT NULL IDENTITY,
	[UserId] INT NOT NULL, 
    [RoleId] INT NOT NULL, 
    CONSTRAINT [PK_IdentityUserRoles] PRIMARY KEY ([Id]),  
    CONSTRAINT [FK_IdentityUserRoles_ToIdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUsers]([Id]), 
    CONSTRAINT [FK_IdentityUserRoles_ToIdentityRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRoles]([Id]), 
)
