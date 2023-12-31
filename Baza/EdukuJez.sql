USE [master]
GO
/****** Object:  Database [Edukujez]    Script Date: 10.12.2023 21:08:17 ******/
CREATE DATABASE [Edukujez]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EdukuJez', FILENAME = N'F:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EdukuJez.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EdukuJez_log', FILENAME = N'F:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EdukuJez_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Edukujez] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Edukujez].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Edukujez] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Edukujez] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Edukujez] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Edukujez] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Edukujez] SET ARITHABORT OFF 
GO
ALTER DATABASE [Edukujez] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Edukujez] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Edukujez] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Edukujez] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Edukujez] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Edukujez] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Edukujez] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Edukujez] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Edukujez] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Edukujez] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Edukujez] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Edukujez] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Edukujez] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Edukujez] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Edukujez] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Edukujez] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Edukujez] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Edukujez] SET RECOVERY FULL 
GO
ALTER DATABASE [Edukujez] SET  MULTI_USER 
GO
ALTER DATABASE [Edukujez] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Edukujez] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Edukujez] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Edukujez] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Edukujez] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Edukujez] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Edukujez', N'ON'
GO
ALTER DATABASE [Edukujez] SET QUERY_STORE = ON
GO
ALTER DATABASE [Edukujez] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Edukujez]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[ID_Group] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Parent_Group] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[ID_Subject] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Subject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Group]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Group](
	[ID_User] [int] NOT NULL,
	[ID_Group] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC,
	[ID_Group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID_User] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Surname] [varchar](255) NOT NULL,
	[Group] [varchar](255) NOT NULL,
	[Login] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ID_Group] [int] NULL,
	[Day] [varchar](20) NULL,
	[Hour] [time](7) NULL,
	[ID_Teacher] [int] NULL,
	[ID_Class] [int] IDENTITY(1,1) NOT NULL,
	[ID_Subject] [int] NULL,
 CONSTRAINT [PK__Classes__D7CF744CB48CAA89] PRIMARY KEY CLUSTERED 
(
	[ID_Class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SubjView]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SubjView] AS
SELECT u.ID_User, us.Login, s.ID_Subject, s.Subject
FROM User_Group u
Inner join Users us on u.ID_User = us.ID_User
INNER JOIN Groups g ON u.ID_Group = g.ID_Group
Inner JOIN Classes c on g.ID_Group = c.ID_Group
inner JOIN Subjects s on c.ID_Subject = s.ID_Subject
GO
/****** Object:  View [dbo].[LessonsPlan]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[LessonsPlan]
AS
SELECT        dbo.Subjects.Subject, dbo.Users.Name, dbo.Users.Surname, dbo.Classes.ID_Class, dbo.Classes.Hour, dbo.Classes.Day
FROM            dbo.Classes INNER JOIN
                         dbo.Subjects ON dbo.Classes.ID_Subject = dbo.Subjects.ID_Subject CROSS JOIN
                         dbo.Users
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[ID_Student] [int] IDENTITY(1,1) NOT NULL,
	[Grade] [int] NULL,
	[Grade_Weight] [int] NULL,
	[ID_Subject] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Student] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission_List]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission_List](
	[ID_Group] [int] NOT NULL,
	[ID_Permission] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Group] ASC,
	[ID_Permission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 10.12.2023 21:08:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[ID_Permission] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Permission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_Login]    Script Date: 10.12.2023 21:08:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Users_Login] ON [dbo].[Users]
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Groups] FOREIGN KEY([ID_Group])
REFERENCES [dbo].[Groups] ([ID_Group])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Groups]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Subjects] FOREIGN KEY([ID_Subject])
REFERENCES [dbo].[Subjects] ([ID_Subject])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Subjects]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Users] FOREIGN KEY([ID_Teacher])
REFERENCES [dbo].[Users] ([ID_User])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Users]
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Subjects] FOREIGN KEY([ID_Subject])
REFERENCES [dbo].[Subjects] ([ID_Subject])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Subjects]
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Users] FOREIGN KEY([ID_Student])
REFERENCES [dbo].[Users] ([ID_User])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Users]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Groups] FOREIGN KEY([Parent_Group])
REFERENCES [dbo].[Groups] ([ID_Group])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Groups]
GO
ALTER TABLE [dbo].[Permission_List]  WITH CHECK ADD  CONSTRAINT [FK_Permission_List_Groups] FOREIGN KEY([ID_Group])
REFERENCES [dbo].[Groups] ([ID_Group])
GO
ALTER TABLE [dbo].[Permission_List] CHECK CONSTRAINT [FK_Permission_List_Groups]
GO
ALTER TABLE [dbo].[Permission_List]  WITH CHECK ADD  CONSTRAINT [FK_Permission_List_Permissions] FOREIGN KEY([ID_Permission])
REFERENCES [dbo].[Permissions] ([ID_Permission])
GO
ALTER TABLE [dbo].[Permission_List] CHECK CONSTRAINT [FK_Permission_List_Permissions]
GO
ALTER TABLE [dbo].[User_Group]  WITH CHECK ADD  CONSTRAINT [FK_User_Group_Groups] FOREIGN KEY([ID_Group])
REFERENCES [dbo].[Groups] ([ID_Group])
GO
ALTER TABLE [dbo].[User_Group] CHECK CONSTRAINT [FK_User_Group_Groups]
GO
ALTER TABLE [dbo].[User_Group]  WITH CHECK ADD  CONSTRAINT [FK_User_Group_Users] FOREIGN KEY([ID_User])
REFERENCES [dbo].[Users] ([ID_User])
GO
ALTER TABLE [dbo].[User_Group] CHECK CONSTRAINT [FK_User_Group_Users]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [CK__Classes__Day__00200768] CHECK  (([Day]='Piatek' OR [Day]='Czwartek' OR [Day]='Sroda' OR [Day]='Wtorek' OR [Day]='Poniedzialek'))
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [CK__Classes__Day__00200768]
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
         Begin Table = "Classes"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Subjects"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LessonsPlan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LessonsPlan'
GO
USE [master]
GO
ALTER DATABASE [Edukujez] SET  READ_WRITE 
GO
