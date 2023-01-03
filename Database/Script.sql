CREATE DATABASE SMSDB
GO
USE [SMSDB]
GO

/****** Object:  Table [dbo].[Students]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Students](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[MobileNumber] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Class] [nvarchar](10) NULL,
	[Ad_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

