/******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
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

/* Create Tables */

CREATE TABLE User (
	login varchar(30),
	password varchar(60) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(30) NOT NULL,
	address varchar(100) NOT NULL,
	email varchar(50) NOT NULL,
	role tinyint NOT NULL,
	language tinyint NOT NULL,

	CONSTRAINT [PK_User] PRIMARY KEY (login)
)

PRINT N'Table User created.'
GO

CREATE TABLE Card (
	number varchar(16),
	cvv varchar(3) NOT NULL,
	expireDate date NOT NULL,
	name varchar(100) NOT NULL,
	default bit NOT NULL,
	type bit NOT NULL,

	CONSTRAINT [PK_Card] PRIMARY KEY (number)
)

PRINT N'Table Card created.'
GO

CREATE TABLE UserCard (
	login varchar(30),
	number varchar(16),

	CONSTRAINT [PK_UserCard] PRIMARY KEY (login, number),
	CONSTRAINT [FK_UserCardUser] FOREING KEY (login) REFERENCES User(login),
	CONSTRAINT [FK_UserCardCard] FOREING KEY (number) REFERENCES Card(number)
)


PRINT N'Table UserCard created.'
GO

CREATE TABLE Order (
	id bigint IDENTITY(1,1),
	date date NOT NULL,
	address varchar(100) NOT NULL,
	totalPrice float NOT NULL,
	cardNumber varchar(16) NOT NULL,

	CONSTRAINT [PK_Order] PRIMARY KEY (id),
	CONSTRAINT [FK_OrderCard] FOREING KEY (cardNumber) REFERENCES Card(number)
)


PRINT N'Table Order created.'
GO

CREATE TABLE Product (
	id bigint IDENTITY(1,1),
	name varchar(30) NOT NULL,
	price float NOT NULL,
	entryDate date NOT NULL,
	stock long NOT NULL,
	image varchar(100),
	description varchar(1000),
	categoryId bigint NOT NULL,

	CONSTRAINT [PK_Product] PRIMARY KEY (id),
	CONSTRAINT [FK_ProductCategory] FOREING KEY (categoryId) REFERENCES Category(id)

)

PRINT N'Table Product created.'
GO

CREATE TABLE OrderLine (
	id bigint IDENTITY(1,1),
	units long NOT NULL,
	price float NOT NULL,
	gift bit NOT NULL,
	productId bigint NOT NULL,
	orderId bigint NOT NULL,

	CONSTRAINT [PK_OrderLine] PRIMARY KEY (id),
	CONSTRAINT [FK_OrderLineProduct] FOREING KEY (productId) REFERENCES Product(id),
	CONSTRAINT [FK_OrderLineOrder] FOREING KEY (orderId) REFERENCES Order(id)
)

PRINT N'Table OrderLine created.'
GO

CREATE TABLE Category (
	id bigint IDENTITY(1,1),
	name varchar(30) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (id)
)

PRINT N'Table Category created.'
GO

/*
CREATE TABLE ProductCategory (
	categoryId bigint,
	productId bigint,

	CONSTRAINT [PK_ProductCategory] PRIMARY KEY (categoryId, productId),
	CONSTRAINT [FK_ProductCategoryCategory] FOREING KEY (categoryId) REFERENCES Category(id),
	CONSTRAINT [FK_ProductCategoryProduct] FOREING KEY (productId) REFERENCES Product(id)

PRINT N'Table ProductCategory created.'
GO
)
*/

CREATE TABLE Label (
	id bigint IDENTITY(1,1),
	value varchar(60) NOT NULL,

	CONSTRAINT [PK_Label] PRIMARY KEY (id)
)

PRINT N'Table Label created.'
GO

CREATE TABLE Comment (
	id bigint IDENTITY(1,1),
	title varchar(60) NOT NULL,
	value varchar(1000) NOT NULL,
	date date NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (id)
)

PRINT N'Table Comment created.'
GO

CREATE TABLE CommentLabel (
	commentId bigint,
	labelId bigint,

	CONSTRAINT [PK_CommentLabel] PRIMARY KEY (commentId, labelId),
	CONSTRAINT [FK_CommentLabelComment] FOREING KEY (commentId) REFERENCES Comment(id),
	CONSTRAINT [FK_CommentLabelLabel] FOREING KEY (labelId) REFERENCES Label(id)
)

PRINT N'Table CommentLabel created.'
GO