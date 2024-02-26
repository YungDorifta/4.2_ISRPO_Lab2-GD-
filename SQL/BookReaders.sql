USE [db_Belashev_ISRPO]
GO

/****** Object:  Table [dbo].[BookReaders]    Script Date: 26.02.2024 10:25:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BookReaders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BookID] [int] NOT NULL,
	[ReaderID] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PK_BookReaders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BookReaders]  WITH CHECK ADD  CONSTRAINT [FKB_BookReaders] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([ID])
GO

ALTER TABLE [dbo].[BookReaders] CHECK CONSTRAINT [FKB_BookReaders]
GO

ALTER TABLE [dbo].[BookReaders]  WITH CHECK ADD  CONSTRAINT [FKR_BookReaders] FOREIGN KEY([ReaderID])
REFERENCES [dbo].[Readers] ([ID])
GO

ALTER TABLE [dbo].[BookReaders] CHECK CONSTRAINT [FKR_BookReaders]
GO

ALTER TABLE [dbo].[BookReaders]  WITH CHECK ADD  CONSTRAINT [CheckDate] CHECK  (([StartDate]<=[EndDate]))
GO

ALTER TABLE [dbo].[BookReaders] CHECK CONSTRAINT [CheckDate]
GO


