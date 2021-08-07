﻿CREATE TABLE [dbo].[IdentityUsers]
(
	[Id] INT NOT NULL IDENTITY, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [ModifyDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
    [ModifyBy] NVARCHAR(50) NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [PasswordHash] NVARCHAR(256) NULL, 
    [Email] NVARCHAR(256) NULL, 
    [IsEmailConfirmed] BIT NULL, 
    [PhoneNumber] NVARCHAR(20) NULL, 
    [IsPhoneNumberConfirmed] BIT NULL, 
    [IsTwoFactorEnabled] BIT NULL, 
    [IsLockoutEnabled] BIT NULL, 
    [LockoutEnd] DATETIME NULL, 
    [AccessFailedCount] INT NULL, 
    [Image] NVARCHAR(256) NULL,
    [FirstName] NVARCHAR(50) NULL,
    [LastName] NVARCHAR(50) NULL,
    [AddressLine1] NVARCHAR(256) NULL, 
    [AddressLine2] NVARCHAR(256) NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(256) NULL, 
    CONSTRAINT [PK_IdentityUsers] PRIMARY KEY ([Id]) 
)