CREATE TABLE [dbo].[Activities]
(
    [Id] INT NOT NULL IDENTITY,
    [BusinessId] INT  NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [CreateBy] NVARCHAR(50) NULL, 
	[UserId] NVARCHAR(50) NOT NULL,
	[Status] NVARCHAR(50) NULL,
	[Text] TEXT NOT NULL,
	[TextHtml] TEXT NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Activities_ToBusinesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses]([Id]), 
)
