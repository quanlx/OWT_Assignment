USE [OWT_SSE]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 7/7/2021 4:08:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[ID] [int] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Author] [nvarchar](150) NOT NULL,
	[Owner] [nvarchar](150) NOT NULL,
	[CoverPhoto] [nvarchar](150) NULL,
	[CateID] [int] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 7/7/2021 4:08:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/7/2021 4:08:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](70) NOT NULL,
	[Password] [varchar](150) NOT NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[LastName] [nvarchar](70) NOT NULL,
	[Role] [varchar](70) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Book] ([ID], [Title], [Description], [Author], [Owner], [CoverPhoto], [CateID]) VALUES (1, N'Endurance ', N'Endurance ', N'Liberty ', N'admin', N'/images/1.png', 1)
GO
INSERT [dbo].[Book] ([ID], [Title], [Description], [Author], [Owner], [CoverPhoto], [CateID]) VALUES (2, N'Atonement ', N'Atonement ', N'Liberty ', N'admin', N'/images/1.png', 1)
GO
INSERT [dbo].[Book] ([ID], [Title], [Description], [Author], [Owner], [CoverPhoto], [CateID]) VALUES (3, N'Nevermore ', N'Nevermore ', N'Liberty ', N'admin', N'/images/2.png', 1)
GO
INSERT [dbo].[Book] ([ID], [Title], [Description], [Author], [Owner], [CoverPhoto], [CateID]) VALUES (4, N'Boneshaker ', N'Boneshaker ', N'Liberty ', N'admin', N'/images/3.png', 1)
GO
INSERT [dbo].[Book] ([ID], [Title], [Description], [Author], [Owner], [CoverPhoto], [CateID]) VALUES (5, N'Mockingbird ', N'Mockingbird ', N'Mockingbird ', N'contributor', N'/images/3.png', 2)
GO
INSERT [dbo].[Book] ([ID], [Title], [Description], [Author], [Owner], [CoverPhoto], [CateID]) VALUES (6, N'Watching', N'Watching', N'Watching', N'contributor', N'/images/0.png', 3)
GO
INSERT [dbo].[Category] ([ID], [Title]) VALUES (1, N'Love Story')
GO
INSERT [dbo].[Category] ([ID], [Title]) VALUES (2, N'History')
GO
INSERT [dbo].[Category] ([ID], [Title]) VALUES (3, N'Chemistry')
GO
INSERT [dbo].[Category] ([ID], [Title]) VALUES (4, N'Young Adult')
GO
INSERT [dbo].[Category] ([ID], [Title]) VALUES (5, N'Food')
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [FirstName], [LastName], [Role]) VALUES (1, N'normal', N'1', N'normal', N'OWT', N'normal')
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [FirstName], [LastName], [Role]) VALUES (2, N'contributor', N'1', N'contributor', N'OWT', N'contributor')
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [FirstName], [LastName], [Role]) VALUES (3, N'admin', N'1', N'admin', N'OWT', N'admin')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Category] FOREIGN KEY([CateID])
REFERENCES [dbo].[Category] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Category]
GO
