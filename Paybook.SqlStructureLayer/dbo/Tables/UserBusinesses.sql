CREATE TABLE [dbo].[UserBusinesses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [BusinessId] INT NOT NULL, 
    CONSTRAINT [FK_UserBusinesses_ToIdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUsers]([Id]), 
    CONSTRAINT [FK_UserBusinesses_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id])
)
