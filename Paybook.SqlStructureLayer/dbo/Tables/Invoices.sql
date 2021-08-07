﻿CREATE TABLE [dbo].[Invoices]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [ModifyDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
    [ModifyBy] NVARCHAR(50) NULL, 
    [InvoiceNumber] NVARCHAR(50) NOT NULL,  
    [Description] NVARCHAR(1000) NOT NULL,  
    [InvoiceDate] DATETIME NOT NULL, 
    [StatusId] INT NOT NULL,  
    [AgencyId] INT NULL, 
    [ClientId] INT NULL, 
    [ClientEmail] NVARCHAR(256) NOT NULL, 
    [IsEmailSend] BIT NULL, 
    [IsEmailSentSuccess] BIT NULL, 
    [BillingAddress] NVARCHAR(1000) NOT NULL,
    [TermsId] INT NULL,    
    [DueDate] DATETIME NOT NULL, 
    [IsOverdue] BIT NULL,
    [OverdueSteps] INT NULL,
    [Message] NVARCHAR(MAX) NULL,
    [Subtotal] DECIMAL(18, 2) NULL, 
    [TaxableTotal] DECIMAL(18, 2) NULL, 
    [DiscountTypeId] INT NULL, 
    [DiscountAmount] DECIMAL(18, 2) NULL, 
    [DiscountTotal] DECIMAL(18, 2) NULL, 
    [Total] DECIMAL(18, 2) NULL, 
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Invoices_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
    CONSTRAINT [FK_Invoices_ToCategoryMaster_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [CategoryMaster]([Id]), 
    CONSTRAINT [FK_Invoices_ToAgencies_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [Agencies]([Id]), 
    CONSTRAINT [FK_Invoices_ToClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients]([Id]), 
    CONSTRAINT [FK_Invoices_ToCategoryMaster_TermsId] FOREIGN KEY ([TermsId]) REFERENCES [CategoryMaster]([Id]), 
)