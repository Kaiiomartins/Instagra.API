CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ImagemUrl] [nvarchar](255) NULL,
	[PostType] [nvarchar](50) NULL,
	[PostDate] [datetime] NOT NULL, 
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Users] FOREIGN KEY ([UserId]) REFERENCES Users([Id])
) 