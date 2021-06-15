/*-----------------------------------------------*/
/*------------------------------------------------
  By: Javier Cañadulce H.
  Copyright: Junio-2021
  Prueba Técnica Ingeneo SAS
  Script: Creación Base de Datos Clima Mundial.
-------------------------------------------------*/
/*-----------------------------------------------*/

/*---- Usamos la Base de Datos Maestra ----*/
USE [master]
GO

/*---- Eliminación de la Base de Datos ----*/
/**** Verificamos si Existe la Data [DBClima] ****/
DECLARE @DBName VARCHAR(50)
DECLARE @spidstr VARCHAR(8000)
DECLARE @ConnKilled SMALLINT
 
SET @ConnKilled = 0
SET @spidstr = ''
SET @DBName = 'DBClima'
 
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DBClima')
	IF Db_id(@DBName) < 4
		BEGIN
			PRINT 'Las Conexiones a las Bases de Datos del Sistema no se Pueden Eliminar' 
			RETURN
		END
	ELSE
		BEGIN
			SELECT @spidstr = COALESCE(@spidstr, ',') + 'kill ' + CONVERT(VARCHAR, spid) + '; '
				FROM master..sysprocesses
				WHERE dbid = Db_id(@DBName)
 
			IF Len(@spidstr) > 0
				BEGIN
					EXEC (@spidstr) 
					SELECT @ConnKilled = Count(1)
						FROM master..sysprocesses
						WHERE dbid = Db_id(@DBName)
				END
			DROP DATABASE DBClima
			PRINT 'Se Inició la Creación de la Base de Datos [DBClima]....'		
		END
GO

/*---- Creación de la Base de Datos ----*/
/**** Object: Database DBClima ****/
CREATE DATABASE [DBClima]
GO

ALTER DATABASE [DBClima] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
	BEGIN
		EXEC [DBClima].[dbo].[sp_fulltext_database] @action = 'enable'
	END
GO

ALTER DATABASE [DBClima] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBClima] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBClima] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBClima] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBClima] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBClima] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBClima] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBClima] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBClima] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBClima] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBClima] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBClima] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBClima] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBClima] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBClima] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBClima] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBClima] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBClima] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBClima] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBClima] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBClima] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBClima] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBClima] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBClima] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBClima] SET  MULTI_USER 
GO
ALTER DATABASE [DBClima] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBClima] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBClima] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBClima] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DBClima] SET  READ_WRITE 
GO

USE [DBClima]
GO

/*---- Creación de las Tablas ----*/
/**** Object: Table [dbo].[Usuarios] ****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO


/**** Object: Table [dbo].[Clima] ****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clima](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ciudad] [varchar] (40) NOT NULL,
	[Celsius] [decimal] (5, 2) NOT NULL,
	[Fahrenheit] [decimal] (5, 2) NULL,
	[Latitud] [decimal] (8, 3) NULL,
	[Longitud] [decimal] (8, 3) NULL, 
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_Clima] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Clima] ADD  CONSTRAINT [DF_Clima_Fecha]  DEFAULT (sysdatetime()) FOR [Fecha]
GO


/*---- Insercción Datos Usuarios ----*/
INSERT 
	INTO [dbo].[Usuarios] ("Usuario","Password") 
	VALUES ('admin','1234')
GO
