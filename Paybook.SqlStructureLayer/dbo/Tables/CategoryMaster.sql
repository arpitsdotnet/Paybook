﻿CREATE TABLE [dbo].[CategoryMaster]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [ModifyDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
    [ModifyBy] NVARCHAR(50) NULL, 
    [CategoryTypeId] INT  NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Core] NVARCHAR(256) NOT NULL, 
    [OrderBy] INT NULL,
    CONSTRAINT [PK_CategoryMaster] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_CategoryMaster_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
    CONSTRAINT [FK_CategoryMaster_ToCategoryTypeMaster_CategoryTypeId] FOREIGN KEY ([CategoryTypeId]) REFERENCES [CategoryTypeMaster]([Id]), 
)