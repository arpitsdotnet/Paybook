CREATE TABLE [dbo].[Activities]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(256) NOT NULL, 
	[Text] TEXT NOT NULL,
	[TextHtml] TEXT NULL,
	[Status] NVARCHAR(50) NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Activities_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
)
