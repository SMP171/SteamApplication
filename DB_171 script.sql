USE [master]
GO
/****** Object:  Database [DB_171]    Script Date: 23.04.2018 10:10:55 ******/
CREATE DATABASE [DB_171]

ALTER DATABASE [DB_171] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_171].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_171] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_171] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_171] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_171] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_171] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_171] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_171] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_171] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_171] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_171] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_171] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_171] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_171] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_171] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_171] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_171] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_171] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_171] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_171] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_171] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_171] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_171] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_171] SET RECOVERY FULL 
GO
ALTER DATABASE [DB_171] SET  MULTI_USER 
GO
ALTER DATABASE [DB_171] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_171] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_171] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_171] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_171] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB_171', N'ON'
GO
ALTER DATABASE [DB_171] SET QUERY_STORE = OFF
GO
USE [DB_171]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DB_171]
GO
/****** Object:  Table [dbo].[developers]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[developers](
	[developers_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[website] [varchar](max) NULL,
	[IsDeleted] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[developers_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_DEVELOPERS_NAME] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[friend_messages]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[friend_messages](
	[FM_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[friend_id] [int] NOT NULL,
	[message] [varchar](max) NOT NULL,
	[send_date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FM_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[friends]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[friends](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[friend_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_FRIENDS_userID_friendID] UNIQUE NONCLUSTERED 
(
	[user_id] ASC,
	[friend_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[group_comments]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group_comments](
	[gc_id] [int] IDENTITY(1,1) NOT NULL,
	[group_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[comment_text] [text] NOT NULL,
	[send_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[gc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[group_id] [int] IDENTITY(1,1) NOT NULL,
	[group_name] [varchar](255) NOT NULL,
	[created_date] [date] NOT NULL,
	[user_id] [int] NOT NULL,
	[IsDeleted] [tinyint] NOT NULL,
 CONSTRAINT [PK_GROUPS_ID] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_GROUPS_NAME] UNIQUE NONCLUSTERED 
(
	[group_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups_users]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups_users](
	[user_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[gu_id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_GROUPS_USERS_GU_ID] PRIMARY KEY CLUSTERED 
(
	[gu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_GROUPSUSERS_USER_GROUP] UNIQUE NONCLUSTERED 
(
	[user_id] ASC,
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_statuses]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_statuses](
	[OS_id] [int] IDENTITY(1,1) NOT NULL,
	[OS_name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OS_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ORDER_STATUSES_OS_NAME] UNIQUE NONCLUSTERED 
(
	[OS_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[order_date] [datetime] NULL,
	[status_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_comments]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_comments](
	[PC_id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[text] [varchar](max) NOT NULL,
	[send_date] [datetime] NOT NULL,
	[comment_mark] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PC_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productComment_marks]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productComment_marks](
	[mark_id] [int] IDENTITY(1,1) NOT NULL,
	[mark] [varchar](25) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[mark_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_PM_MARK] UNIQUE NONCLUSTERED 
(
	[mark] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[products_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[description] [text] NULL,
	[rating] [int] NOT NULL,
	[devoloper_id] [int] NOT NULL,
	[price] [money] NOT NULL,
	[create_date] [date] NOT NULL,
	[positive_marks] [int] NOT NULL,
	[negative_marks] [int] NOT NULL,
	[IsDeleted] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[products_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_PRODUCTS_NAME] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[thread_answers]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[thread_answers](
	[TA_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[thread_id] [int] NOT NULL,
	[send_date] [datetime] NOT NULL,
	[text] [varchar](1000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TA_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[threads]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[threads](
	[thread_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[creator_id] [int] NOT NULL,
	[text] [varchar](1000) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[IsDeleted] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[thread_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_THREADS_NAME] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[threads_users]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[threads_users](
	[tu_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[thread_id] [int] NOT NULL,
 CONSTRAINT [PK_THEREADS_USERS_ID] PRIMARY KEY CLUSTERED 
(
	[tu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_statuses]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_statuses](
	[US_id] [int] IDENTITY(1,1) NOT NULL,
	[US_name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[US_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_USER_STATUSES_US_NAME] UNIQUE NONCLUSTERED 
(
	[US_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[nickname] [varchar](100) NOT NULL,
	[register_date] [datetime] NULL,
	[status_id] [int] NOT NULL,
	[IsDeleted] [tinyint] NOT NULL,
	[password] [varchar](255) NOT NULL,
	[wallet_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_USERS_NICKNAME] UNIQUE NONCLUSTERED 
(
	[nickname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_products]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_products](
	[up_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[product_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[up_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_UP_USERID_PRODUCTID] UNIQUE NONCLUSTERED 
(
	[user_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wallets]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wallets](
	[balance] [money] NOT NULL,
	[wallet_id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_WALLETS_ID] PRIMARY KEY CLUSTERED 
(
	[wallet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[developers] ADD  CONSTRAINT [DF_DEVELOPERS_ISDELETED]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[friend_messages] ADD  DEFAULT (getdate()) FOR [send_date]
GO
ALTER TABLE [dbo].[group_comments] ADD  DEFAULT (getdate()) FOR [send_date]
GO
ALTER TABLE [dbo].[groups] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [order_date]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_ORDERS_STATUS_ID]  DEFAULT ((1)) FOR [status_id]
GO
ALTER TABLE [dbo].[product_comments] ADD  DEFAULT (getdate()) FOR [send_date]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF_PRODUCTS_RATING]  DEFAULT ((50)) FOR [rating]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [positive_marks]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [negative_marks]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[thread_answers] ADD  CONSTRAINT [DF_THREAD_ANSWERS]  DEFAULT (getdate()) FOR [send_date]
GO
ALTER TABLE [dbo].[threads] ADD  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[threads] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (getdate()) FOR [register_date]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_USERS_STATUS_ID]  DEFAULT ((1)) FOR [status_id]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[friend_messages]  WITH CHECK ADD  CONSTRAINT [FK_FM_FRIEND_ID] FOREIGN KEY([friend_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[friend_messages] CHECK CONSTRAINT [FK_FM_FRIEND_ID]
GO
ALTER TABLE [dbo].[friend_messages]  WITH CHECK ADD  CONSTRAINT [FK_FM_USER_ID] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[friend_messages] CHECK CONSTRAINT [FK_FM_USER_ID]
GO
ALTER TABLE [dbo].[friends]  WITH CHECK ADD FOREIGN KEY([friend_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[friends]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[group_comments]  WITH CHECK ADD  CONSTRAINT [FK_GROUP_COMMENTS_GROUP_ID] FOREIGN KEY([group_id])
REFERENCES [dbo].[groups] ([group_id])
GO
ALTER TABLE [dbo].[group_comments] CHECK CONSTRAINT [FK_GROUP_COMMENTS_GROUP_ID]
GO
ALTER TABLE [dbo].[group_comments]  WITH CHECK ADD  CONSTRAINT [FK_GROUP_COMMENTS_USER_ID] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[group_comments] CHECK CONSTRAINT [FK_GROUP_COMMENTS_USER_ID]
GO
ALTER TABLE [dbo].[groups]  WITH CHECK ADD  CONSTRAINT [FK_CREATOR_ID] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[groups] CHECK CONSTRAINT [FK_CREATOR_ID]
GO
ALTER TABLE [dbo].[groups_users]  WITH CHECK ADD  CONSTRAINT [FK_GROUPS_USERS_GROUP_ID] FOREIGN KEY([group_id])
REFERENCES [dbo].[groups] ([group_id])
GO
ALTER TABLE [dbo].[groups_users] CHECK CONSTRAINT [FK_GROUPS_USERS_GROUP_ID]
GO
ALTER TABLE [dbo].[groups_users]  WITH CHECK ADD  CONSTRAINT [FK_GROUPSUSERS_USER_ID] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[groups_users] CHECK CONSTRAINT [FK_GROUPSUSERS_USER_ID]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([products_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_products]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_ORDERS_STATUS_ID] FOREIGN KEY([status_id])
REFERENCES [dbo].[order_statuses] ([OS_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_ORDERS_STATUS_ID]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_users]
GO
ALTER TABLE [dbo].[product_comments]  WITH CHECK ADD FOREIGN KEY([comment_mark])
REFERENCES [dbo].[productComment_marks] ([mark_id])
GO
ALTER TABLE [dbo].[product_comments]  WITH CHECK ADD  CONSTRAINT [FK_PC_PRODUCT_ID] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([products_id])
GO
ALTER TABLE [dbo].[product_comments] CHECK CONSTRAINT [FK_PC_PRODUCT_ID]
GO
ALTER TABLE [dbo].[product_comments]  WITH CHECK ADD  CONSTRAINT [FK_PC_USER_ID] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[product_comments] CHECK CONSTRAINT [FK_PC_USER_ID]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_developers] FOREIGN KEY([devoloper_id])
REFERENCES [dbo].[developers] ([developers_id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_developers]
GO
ALTER TABLE [dbo].[thread_answers]  WITH CHECK ADD FOREIGN KEY([thread_id])
REFERENCES [dbo].[threads] ([thread_id])
GO
ALTER TABLE [dbo].[thread_answers]  WITH CHECK ADD  CONSTRAINT [FK_THREADS_ANSWERS_USER_ID] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[thread_answers] CHECK CONSTRAINT [FK_THREADS_ANSWERS_USER_ID]
GO
ALTER TABLE [dbo].[threads]  WITH CHECK ADD  CONSTRAINT [FK_THREADS_CREATOR_ID] FOREIGN KEY([creator_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[threads] CHECK CONSTRAINT [FK_THREADS_CREATOR_ID]
GO
ALTER TABLE [dbo].[threads_users]  WITH CHECK ADD  CONSTRAINT [FK_THREADS_USERS_THREAD_ID] FOREIGN KEY([thread_id])
REFERENCES [dbo].[threads] ([thread_id])
GO
ALTER TABLE [dbo].[threads_users] CHECK CONSTRAINT [FK_THREADS_USERS_THREAD_ID]
GO
ALTER TABLE [dbo].[threads_users]  WITH CHECK ADD  CONSTRAINT [FK_THREADS_USERS_USER_ID] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[threads_users] CHECK CONSTRAINT [FK_THREADS_USERS_USER_ID]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_USERS_STATUS_ID] FOREIGN KEY([status_id])
REFERENCES [dbo].[user_statuses] ([US_id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_USERS_STATUS_ID]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_USERS_WALLET_ID] FOREIGN KEY([wallet_id])
REFERENCES [dbo].[wallets] ([wallet_id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_USERS_WALLET_ID]
GO
ALTER TABLE [dbo].[users_products]  WITH CHECK ADD  CONSTRAINT [FK_users_products_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([products_id])
GO
ALTER TABLE [dbo].[users_products] CHECK CONSTRAINT [FK_users_products_products]
GO
ALTER TABLE [dbo].[users_products]  WITH CHECK ADD  CONSTRAINT [FK_users_products_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[users_products] CHECK CONSTRAINT [FK_users_products_users]
GO
ALTER TABLE [dbo].[developers]  WITH CHECK ADD  CONSTRAINT [CH_DEVELOPERS_ISDELETED] CHECK  (([IsDeleted]=(1) OR [IsDeleted]=(0)))
GO
ALTER TABLE [dbo].[developers] CHECK CONSTRAINT [CH_DEVELOPERS_ISDELETED]
GO
ALTER TABLE [dbo].[friend_messages]  WITH CHECK ADD  CONSTRAINT [CH_FM_USER_FRIEND] CHECK  (([user_id]<>[friend_id]))
GO
ALTER TABLE [dbo].[friend_messages] CHECK CONSTRAINT [CH_FM_USER_FRIEND]
GO
ALTER TABLE [dbo].[groups]  WITH CHECK ADD  CONSTRAINT [CH_CREATED_DATE] CHECK  (([created_date]<=getdate()))
GO
ALTER TABLE [dbo].[groups] CHECK CONSTRAINT [CH_CREATED_DATE]
GO
ALTER TABLE [dbo].[groups]  WITH CHECK ADD  CONSTRAINT [CH_GROUPS_ISDELETED] CHECK  (([IsDeleted]=(1) OR [IsDeleted]=(0)))
GO
ALTER TABLE [dbo].[groups] CHECK CONSTRAINT [CH_GROUPS_ISDELETED]
GO
ALTER TABLE [dbo].[product_comments]  WITH CHECK ADD  CONSTRAINT [CH_PC_MARK] CHECK  (([comment_mark]>=(0) AND [comment_mark]<(3)))
GO
ALTER TABLE [dbo].[product_comments] CHECK CONSTRAINT [CH_PC_MARK]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [CH_PRODUCTS_ISDELETED] CHECK  (([IsDeleted]=(1) OR [IsDeleted]=(0)))
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [CH_PRODUCTS_ISDELETED]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [CH_PRODUCTS_RATING] CHECK  (([rating]>=(0) AND [rating]<=(100)))
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [CH_PRODUCTS_RATING]
GO
ALTER TABLE [dbo].[threads]  WITH CHECK ADD  CONSTRAINT [CH_THREADS_ISDELETED] CHECK  (([IsDeleted]=(1) OR [IsDeleted]=(0)))
GO
ALTER TABLE [dbo].[threads] CHECK CONSTRAINT [CH_THREADS_ISDELETED]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [CH_USERS_ISDELETED] CHECK  (([IsDeleted]=(1) OR [IsDeleted]=(0)))
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [CH_USERS_ISDELETED]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [CH_USERS_PASSWORD] CHECK  ((len([password])>=(8)))
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [CH_USERS_PASSWORD]
GO
ALTER TABLE [dbo].[wallets]  WITH CHECK ADD  CONSTRAINT [CH_WALLETS_BALANCE] CHECK  (([balance]>=(0)))
GO
ALTER TABLE [dbo].[wallets] CHECK CONSTRAINT [CH_WALLETS_BALANCE]
GO
/****** Object:  StoredProcedure [dbo].[GetGroupMembers]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetGroupMembers] @GroupId int
as
begin
	select u.* from users u, groups_users gu
	where gu.group_id = @GroupId and u.user_id = gu.user_id
end

GO
/****** Object:  StoredProcedure [dbo].[sp_DB_171_UpdateOrderStatus]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_DB_171_UpdateOrderStatus]
as
begin
	update orders
	set status_id = 2
	where status_id = 1
	and datediff(dd, order_date, getdate()) >= 7
end
GO
/****** Object:  Trigger [dbo].[TR_FRINEDS_CREATE]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_FRINEDS_CREATE] on [dbo].[friends] after insert
as
begin
	insert into friends
	select friend_id, user_id from inserted
end
GO
ALTER TABLE [dbo].[friends] ENABLE TRIGGER [TR_FRINEDS_CREATE]
GO
/****** Object:  Trigger [dbo].[TR_GROUPS_CREATE]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_GROUPS_CREATE]
on [dbo].[groups] after insert
as begin
	insert into groups_users(user_id, group_id)
	select user_id, group_id from inserted
end
GO
ALTER TABLE [dbo].[groups] ENABLE TRIGGER [TR_GROUPS_CREATE]
GO
/****** Object:  Trigger [dbo].[TR_PC_CREATE]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_PC_CREATE] on [dbo].[product_comments]
after insert as
begin
	declare @Enumerator table (id int)
	
	insert into @Enumerator
	select PC_id
	from inserted
	
	declare @id int
	
	while exists (select 1 from @Enumerator)
	begin
		select top 1 @id = id from @Enumerator 

		declare @product_id int = (select product_id from inserted 
								   where @id = PC_id)

		if((select comment_mark from inserted
			where @id = PC_id) = 1)
			begin
				update products
				set positive_marks += 1
				where products_id = @product_id
			end

		if((select comment_mark from inserted
		where @id = PC_id) = 2)
		begin
			update products
			set negative_marks += 1
			where products_id = @product_id
		end

		delete from @Enumerator where id = @id
	end
end
GO
ALTER TABLE [dbo].[product_comments] ENABLE TRIGGER [TR_PC_CREATE]
GO
/****** Object:  Trigger [dbo].[TR_THREADS_CREATE]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_THREADS_CREATE]
on [dbo].[threads] after insert
as begin
	insert into threads_users(thread_id, user_id)
	select creator_id, thread_id from inserted
end
GO
ALTER TABLE [dbo].[threads] ENABLE TRIGGER [TR_THREADS_CREATE]
GO
/****** Object:  Trigger [dbo].[TR_USER_WALLETCREATE]    Script Date: 23.04.2018 10:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[TR_USER_WALLETCREATE]
on [dbo].[users] after insert as
begin
	insert into wallets
	values (0)

	declare @wallet_id int = (select top 1 wallet_id 
				from wallets order by wallet_id desc)

	update users
	set wallet_id = @wallet_id
	where user_id = (select user_id from inserted)
end
GO
ALTER TABLE [dbo].[users] ENABLE TRIGGER [TR_USER_WALLETCREATE]
GO

insert into user_statuses
values ('Active'), ('Not Active'), ('Banned')

insert into productComment_marks
values ('Positive'), ('Negative'), ('Neutral')

insert into order_statuses
values ('Purchased'), ('Might be returned'), ('Returned')