
CREATE TABLE [dbo].[Comments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
	[Comment] [varchar](500) NULL,
	[DateComment] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	 CONSTRAINT FK_Comments_Users FOREIGN KEY (UserId)
        REFERENCES Users(Id),

    CONSTRAINT FK_Comments_Posts FOREIGN KEY (PostId)
        REFERENCES Posts(Id)
);
