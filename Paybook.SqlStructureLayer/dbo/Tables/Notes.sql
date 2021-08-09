﻿CREATE TABLE [dbo].[Notes]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [ModifyDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
    [ModifyBy] NVARCHAR(50) NULL, 
    [Text] TEXT NOT NULL, 
    [WorkTypeId] INT NULL, 
    [VehicleNumber] NVARCHAR(50) NULL, 
    [ClientName] NVARCHAR(50) NULL, 
    [MobileNumber] NVARCHAR(20) NULL, 
    [Amount] DECIMAL(18, 2) NULL, 
    [Awak] DECIMAL(18, 2) NULL, 
    [Jawak] DECIMAL(18, 2) NULL, 
    [Balance] DECIMAL(18, 2) NULL, 
    CONSTRAINT [PK_Notes] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Notes_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
    CONSTRAINT [FK_Notes_ToCategoryMaster_WorkTypeId] FOREIGN KEY ([WorkTypeId]) REFERENCES [CategoryMaster]([Id]), 
)
