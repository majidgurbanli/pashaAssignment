USE [master]
GO
/****** Object:  Database [PashaInsuranceDB]    Script Date: 10/18/2024 3:01:15 PM ******/
CREATE DATABASE [PashaInsuranceDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PashaInsuranceDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PashaInsuranceDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PashaInsuranceDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PashaInsuranceDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PashaInsuranceDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PashaInsuranceDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PashaInsuranceDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PashaInsuranceDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PashaInsuranceDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PashaInsuranceDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PashaInsuranceDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET RECOVERY FULL 
GO
ALTER DATABASE [PashaInsuranceDB] SET  MULTI_USER 
GO
ALTER DATABASE [PashaInsuranceDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PashaInsuranceDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PashaInsuranceDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PashaInsuranceDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PashaInsuranceDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PashaInsuranceDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PashaInsuranceDB', N'ON'
GO
ALTER DATABASE [PashaInsuranceDB] SET QUERY_STORE = OFF
GO
USE [PashaInsuranceDB]
GO
/****** Object:  Table [dbo].[admin_user]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin_user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[answer]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[answer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[applicant_id] [int] NOT NULL,
	[vacancy_id] [int] NOT NULL,
	[question_id] [int] NOT NULL,
	[choice_id] [int] NULL,
	[start_time] [datetime] NOT NULL,
	[answer_time] [datetime] NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[applicants]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[applicants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[surname] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[phone_number] [nvarchar](255) NOT NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[application]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[application](
	[applicant_id] [int] NOT NULL,
	[vacancy_id] [int] NOT NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[vacancy_id] ASC,
	[applicant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[choices]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[choices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[choice_text] [nvarchar](255) NOT NULL,
	[is_correct] [bit] NOT NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
	[question_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileEntity]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileEntity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[vacancy_id] [int] NULL,
	[applicant_id] [int] NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[vacancy_id] ASC,
	[applicant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[question]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[question_text] [nvarchar](255) NOT NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[id] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vacancy]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vacancy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[description] [nvarchar](255) NULL,
	[reguid] [int] NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
	[question_count] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vacancy_question]    Script Date: 10/18/2024 3:01:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vacancy_question](
	[vacancy_id] [int] NOT NULL,
	[question_id] [int] NOT NULL,
	[reg_date] [datetime] NULL,
	[edituid] [int] NULL,
	[edit_date] [datetime] NULL,
	[archived_user_id] [int] NULL,
	[reguid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[vacancy_id] ASC,
	[question_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[admin_user] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[answer] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[answer] ADD  DEFAULT ((0)) FOR [is_active]
GO
ALTER TABLE [dbo].[applicants] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[application] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[choices] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[FileEntity] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[question] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[vacancy] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[vacancy_question] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD FOREIGN KEY([vacancy_id], [applicant_id])
REFERENCES [dbo].[application] ([vacancy_id], [applicant_id])
GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD FOREIGN KEY([choice_id])
REFERENCES [dbo].[choices] ([Id])
GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([Id])
GO
ALTER TABLE [dbo].[application]  WITH CHECK ADD FOREIGN KEY([applicant_id])
REFERENCES [dbo].[applicants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[application]  WITH CHECK ADD FOREIGN KEY([vacancy_id])
REFERENCES [dbo].[vacancy] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[choices]  WITH CHECK ADD  CONSTRAINT [FK_Choices_Question] FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[choices] CHECK CONSTRAINT [FK_Choices_Question]
GO
ALTER TABLE [dbo].[FileEntity]  WITH CHECK ADD FOREIGN KEY([vacancy_id], [applicant_id])
REFERENCES [dbo].[application] ([vacancy_id], [applicant_id])
GO
ALTER TABLE [dbo].[vacancy_question]  WITH CHECK ADD FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vacancy_question]  WITH CHECK ADD FOREIGN KEY([vacancy_id])
REFERENCES [dbo].[vacancy] ([Id])
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [PashaInsuranceDB] SET  READ_WRITE 
GO
