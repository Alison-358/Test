
--IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = 'ALISON' OR name = 'ALISON')))
--	BEGIN
CREATE DATABASE ALISON;
GO
USE ALISON
GO
IF OBJECT_ID('[Address]', 'U') IS NULL 
	BEGIN
		CREATE TABLE [dbo].[Address](
			[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
			[LastUpdateDate] [datetime] NOT NULL,
			[LastUpdateUser] [varchar](150) NOT NULL,
			[Description] [varchar](200) NOT NULL,
			[Complement] [varchar](200) NULL,
			[Neighborhood] [varchar](150) NOT NULL,
			[Number] [varchar](50) NOT NULL
		)
	END

IF OBJECT_ID('[Role]', 'U') IS NULL 
	BEGIN
		CREATE TABLE [dbo].[Role](
			[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
			[LastUpdateDate] [datetime] NOT NULL,
			[LastUpdateUser] [varchar](150) NOT NULL,
			[Name] [varchar](200) NOT NULL
		)
	END

IF OBJECT_ID('[User]', 'U') IS NULL 
	BEGIN
		CREATE TABLE [dbo].[User](
			[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
			[LastUpdateDate] [datetime] NOT NULL,
			[LastUpdateUser] [varchar](150) NOT NULL,
			[Name] [varchar](200) NOT NULL,
			[Email] [varchar](150) NOT NULL,
			[Password] [varchar](50) NOT NULL,
			[RoleId] [int] NOT NULL
			CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
			REFERENCES [dbo].[Role] ([Id])
		)
	END
GO
IF OBJECT_ID('[Role]', 'U') IS NOT NULL 
	BEGIN
		INSERT INTO [Role]
		SELECT GETDATE(), 'Admin', 'Admin' UNION ALL
		SELECT GETDATE(), 'Admin', 'User'
	END

IF OBJECT_ID('[User]', 'U') IS NOT NULL 
	BEGIN
		INSERT INTO [User]
		SELECT GETDATE(), 'Admin', 'Alison', 'alison@gmail.com', 'Alison', 1 UNION ALL
		SELECT GETDATE(), 'Admin', 'Paulo', 'jose@gmail.com', 'jose', 2
	END
--END

		                 

