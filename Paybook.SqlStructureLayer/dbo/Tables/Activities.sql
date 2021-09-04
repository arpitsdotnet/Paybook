CREATE TABLE [dbo].[Activities]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
	[Status] NVARCHAR(50) NULL,
	[Text] NVARCHAR(MAX) NOT NULL,
	[TextHtml] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Activities_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
)
