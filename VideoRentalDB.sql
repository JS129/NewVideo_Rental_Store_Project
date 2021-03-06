USE [master]
GO
/****** Object:  Database [VideoAssignTask]    Script Date: 04-Jul-21 9:43:47 PM ******/
CREATE DATABASE [VideoAssignTask] ON  PRIMARY 
( NAME = N'VideoAssignTask', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\VideoAssignTask.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'VideoAssignTask_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\VideoAssignTask_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [VideoAssignTask] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VideoAssignTask].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VideoAssignTask] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VideoAssignTask] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VideoAssignTask] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VideoAssignTask] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VideoAssignTask] SET ARITHABORT OFF 
GO
ALTER DATABASE [VideoAssignTask] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VideoAssignTask] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VideoAssignTask] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VideoAssignTask] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VideoAssignTask] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VideoAssignTask] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VideoAssignTask] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VideoAssignTask] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VideoAssignTask] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VideoAssignTask] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VideoAssignTask] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VideoAssignTask] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VideoAssignTask] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VideoAssignTask] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VideoAssignTask] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VideoAssignTask] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VideoAssignTask] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VideoAssignTask] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VideoAssignTask] SET  MULTI_USER 
GO
ALTER DATABASE [VideoAssignTask] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VideoAssignTask] SET DB_CHAINING OFF 
GO
USE [VideoAssignTask]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[book_id] [int] IDENTITY(1,1) NOT NULL,
	[book_cID] [int] NOT NULL,
	[book_vID] [int] NOT NULL,
	[book_start] [varchar](50) NULL,
	[book_end] [varchar](50) NULL,
	[book_status] [varchar](50) NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[book_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[cust_id] [int] IDENTITY(1,1) NOT NULL,
	[cust_name] [varchar](50) NULL,
	[cust_phone] [varchar](50) NULL,
	[cust_address] [varchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[cust_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Customer_1] UNIQUE NONCLUSTERED 
(
	[cust_phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Video]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Video](
	[video_id] [int] IDENTITY(1,1) NOT NULL,
	[video_title] [varchar](50) NULL,
	[video_gener] [varchar](50) NULL,
	[video_price] [varchar](10) NULL,
	[video_ratting] [varchar](50) NULL,
	[video_copies] [int] NULL,
	[video_year] [varchar](50) NULL,
 CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED 
(
	[video_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[OuttedVideo]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OuttedVideo]
AS
SELECT dbo.Video.video_id AS ID, dbo.Video.video_title AS Title, dbo.Video.video_gener AS Gener, dbo.Video.video_price AS Cost, dbo.Video.video_copies AS Copies, dbo.Video.video_ratting AS Ratting, dbo.Video.video_year AS Year
FROM   dbo.Booking INNER JOIN
             dbo.Video ON dbo.Booking.book_vID <> dbo.Video.video_id
GO
/****** Object:  View [dbo].[RentedVideo]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RentedVideo]
AS
SELECT dbo.Booking.book_vID AS ID, dbo.Video.video_title AS Title, dbo.Video.video_gener AS Gener, dbo.Video.video_price AS Cost, dbo.Video.video_copies AS Copies, dbo.Video.video_ratting AS Ratting, dbo.Video.video_year AS Year
FROM   dbo.Booking INNER JOIN
             dbo.Video ON dbo.Booking.book_vID = dbo.Video.video_id
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Customer] FOREIGN KEY([book_cID])
REFERENCES [dbo].[Customer] ([cust_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Customer]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Video] FOREIGN KEY([book_vID])
REFERENCES [dbo].[Video] ([video_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Video]
GO
/****** Object:  StoredProcedure [dbo].[getCustomer]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getCustomer]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT cust_id as 'ID', cust_name as 'Name',cust_phone as 'Phone',cust_address as 'Address' from Customer;
END
GO
/****** Object:  StoredProcedure [dbo].[getRental]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getRental] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT b.book_cID as 'CID',b.book_vID as 'VID', b.book_id as 'ID',c.cust_name as 'Customer',v.video_title as 'Video',v.video_price as 'Cost',b.book_start as 'Booking Date',b.book_end as 'Return Date', b.book_status as 'Status' from Booking b,Video v,Customer c where b.book_cID=c.cust_id and b.book_vID=v.video_id;
END
GO
/****** Object:  StoredProcedure [dbo].[getVideo]    Script Date: 04-Jul-21 9:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getVideo] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT video_id as 'ID',video_title as 'Title',video_gener as 'Gener',video_price+' $' as 'Cost',video_copies as 'Copies',video_ratting as 'Ratting',video_year as 'Year' from Video;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Booking"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Video"
            Begin Extent = 
               Top = 9
               Left = 336
               Bottom = 206
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OuttedVideo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OuttedVideo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Booking"
            Begin Extent = 
               Top = 7
               Left = 57
               Bottom = 206
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Video"
            Begin Extent = 
               Top = 15
               Left = 418
               Bottom = 212
               Right = 640
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RentedVideo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RentedVideo'
GO
USE [master]
GO
ALTER DATABASE [VideoAssignTask] SET  READ_WRITE 
GO
