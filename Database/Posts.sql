CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ImageBytes] [varbinary](max) NULL, 
	[IsDeleted][Bite] NOT NULL,
	[DeletedAt][DateTime]
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Users] FOREIGN KEY ([UserId]) REFERENCES Users([Id])
) 