DECLARE @Default_DB_Path as VARCHAR(64)  
SET @Default_DB_Path = N'C:\SourceCode\DataBase\'
 
USE [master]

/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'amazonia')
DROP DATABASE [amazonia]

USE [master]

/* DataBase Creation */
								  
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [amazonia] 
	ON PRIMARY ( NAME = amazonia, FILENAME = "' + @Default_DB_Path + N'amazonia.mdf")
	LOG ON ( NAME = amazonia_log, FILENAME = "' + @Default_DB_Path + N'amazonia_log.ldf")'

EXEC(@sql)
PRINT N'Database [amazonia] created.'
GO