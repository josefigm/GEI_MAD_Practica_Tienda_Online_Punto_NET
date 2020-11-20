USE [amazonia]

/*  Drop Tables  if already exists  */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[SaleLine]') AND type in ('U'))
DROP TABLE [SaleLine]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Sale]') AND type in ('U'))
DROP TABLE [Sale]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Bike]') AND type in ('U'))
DROP TABLE [Bike]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Car]') AND type in ('U'))
DROP TABLE [Car]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Computer]') AND type in ('U'))
DROP TABLE [Computer]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CommentLabel]') AND type in ('U'))
DROP TABLE [CommentLabel]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Label]') AND type in ('U'))
DROP TABLE [Label]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Product]') AND type in ('U'))
DROP TABLE [Product]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[ClientCard]') AND type in ('U'))
DROP TABLE [ClientCard]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Client]') AND type in ('U'))
DROP TABLE [Client]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Card]') AND type in ('U'))
DROP TABLE [Card]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO



CREATE TABLE Card (
	number varchar(16),
	cvv varchar(3) NOT NULL,
	expireDate date NOT NULL,
	name varchar(100) NOT NULL,
	type bit NOT NULL,

	CONSTRAINT [PK_Card] PRIMARY KEY (number)
)

PRINT N'Table Card created.'
GO

CREATE TABLE Client (
	login varchar(30),
	password varchar(60) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(30) NOT NULL,
	address varchar(100) NOT NULL,
	email varchar(50) NOT NULL,
	role tinyint NOT NULL,
	language tinyint NOT NULL,
	defaultCardNumber varchar(16),

	CONSTRAINT [PK_Client] PRIMARY KEY (login),
	CONSTRAINT [FK_ClientCard] FOREIGN KEY (defaultCardNumber) REFERENCES Card(number)
)

PRINT N'Table Client created.'
GO


CREATE TABLE ClientCard (
	login varchar(30),
	number varchar(16),

	CONSTRAINT [PK_ClientCard] PRIMARY KEY (login, number),
	CONSTRAINT [FK_ClientCardClient] FOREIGN KEY (login) REFERENCES Client(login),
	CONSTRAINT [FK_ClientCardCard] FOREIGN KEY (number) REFERENCES Card(number)
)


PRINT N'Table ClientCard created.'
GO

CREATE TABLE Sale (
	id bigint IDENTITY(1,1),
	date date NOT NULL,
	descName varchar(100) NOT NULL,
	address varchar(100) NOT NULL,
	totalPrice float NOT NULL,
	cardNumber varchar(16) NOT NULL,
	clientLogin varchar(30),

	CONSTRAINT [PK_Sale] PRIMARY KEY (id),
	CONSTRAINT [FK_SaleCard] FOREIGN KEY (cardNumber) REFERENCES Card(number),
	CONSTRAINT [FK_SaleClient] FOREIGN KEY (clientLogin) REFERENCES Client(login)
)


PRINT N'Table Sale created.'
GO

CREATE TABLE Category (
	id bigint IDENTITY(1,1),
	name varchar(30) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (id)
)

PRINT N'Table Category created.'
GO


CREATE TABLE Product (
	id bigint IDENTITY(1,1),
	name varchar(30) NOT NULL,
	price float NOT NULL,
	entryDate date NOT NULL,
	stock bigint NOT NULL,
	image varchar(100),
	description varchar(1000),
	categoryId bigint NOT NULL,

	CONSTRAINT [PK_Product] PRIMARY KEY (id),
	CONSTRAINT [FK_ProductCategory] FOREIGN KEY (categoryId) REFERENCES Category(id)

)

PRINT N'Table Product created.'
GO

CREATE TABLE Bike (
	productId bigint,
	colour varchar(30) NOT NULL,
	size varchar(5) NOT NULL,
	type varchar(5) NOT NULL,

	CONSTRAINT [PK_ProductBike] PRIMARY KEY (productId),
	CONSTRAINT [FK_BikeProduct] FOREIGN KEY (productId) REFERENCES Product(id) ON DELETE CASCADE
)

PRINT N'Table Bike created.'
GO

CREATE TABLE Car (
	productId bigint,
	colour varchar(30) NOT NULL,
	model varchar(20) NOT NULL,
	enginePower bigint NOT NULL,

	CONSTRAINT [PK_ProductCar] PRIMARY KEY (productId),
	CONSTRAINT [FK_ComputerProduct] FOREIGN KEY (productId) REFERENCES Product(id) ON DELETE CASCADE
)

PRINT N'Table Car created.'
GO

CREATE TABLE Computer (
	productId bigint,
	memoryROM varchar(30) NOT NULL,
	memoryRAM varchar(30) NOT NULL,
	cpuPower varchar(20) NOT NULL,
	model bigint NOT NULL,

	CONSTRAINT [PK_ProductComputer] PRIMARY KEY (productId),
	CONSTRAINT [FK_CarProduct] FOREIGN KEY (productId) REFERENCES Product(id) ON DELETE CASCADE
)

PRINT N'Table Computer created.'
GO

CREATE TABLE SaleLine (
	id bigint IDENTITY(1,1),
	units bigint NOT NULL,
	price float NOT NULL,
	gift bit NOT NULL,
	productId bigint NOT NULL,
	saleId bigint NOT NULL,

	CONSTRAINT [PK_SaleLine] PRIMARY KEY (id),
	CONSTRAINT [FK_SaleLineProduct] FOREIGN KEY (productId) REFERENCES Product(id),
	CONSTRAINT [FK_SaleLineSale] FOREIGN KEY (saleId) REFERENCES Sale(id)
)

PRINT N'Table SaleLine created.'
GO

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
	productId bigint

	CONSTRAINT [PK_Comment] PRIMARY KEY (id),
	CONSTRAINT [FK_CommentProduct] FOREIGN KEY (productId) REFERENCES Product(id)
)

PRINT N'Table Comment created.'
GO

CREATE TABLE CommentLabel (
	commentId bigint,
	labelId bigint,

	CONSTRAINT [PK_CommentLabel] PRIMARY KEY (commentId, labelId),
	CONSTRAINT [FK_CommentLabelComment] FOREIGN KEY (commentId) REFERENCES Comment(id),
	CONSTRAINT [FK_CommentLabelLabel] FOREIGN KEY (labelId) REFERENCES Label(id)
)

PRINT N'Table CommentLabel created.'
GO