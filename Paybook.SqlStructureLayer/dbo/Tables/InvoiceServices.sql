﻿CREATE TABLE [dbo].[InvoiceServices]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [ModifyDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
    [ModifyBy] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(256) NOT NULL,
    [WorkTypeId] INT NULL, 
    [VehicleNumber] NVARCHAR(50) NULL, 
    [Qty] INT NOT NULL, 
    [Rate] DECIMAL(18, 2) NOT NULL, 
    [Subtotal] DECIMAL(18, 2) NULL, 
    [IsTaxable] BIT NULL, 
    [TaxTypeId] INT NULL, 
    [IGSTPercentage] INT NULL, 
    [IGSTAmount] DECIMAL(18, 2) NULL, 
    [CGSTPercentage] INT NULL, 
    [CGSTAmount] DECIMAL(18, 2) NULL, 
    [SGSTPercentage] INT NULL, 
    [SGSTAmount] DECIMAL(18, 2) NULL, 
    [TaxableTotal] DECIMAL(18, 2) NULL, 
    [Total] DECIMAL(18, 2) NULL, 
    CONSTRAINT [PK_InvoiceServices] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_InvoiceServices_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
    CONSTRAINT [FK_InvoiceServices_ToCategoryMaster_WorkTypeId] FOREIGN KEY ([WorkTypeId]) REFERENCES [CategoryMaster]([Id]), 
    CONSTRAINT [FK_InvoiceServices_ToCategoryMaster_TaxTypeId] FOREIGN KEY ([TaxTypeId]) REFERENCES [CategoryMaster]([Id]), 
)