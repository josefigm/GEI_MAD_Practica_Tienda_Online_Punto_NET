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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Card]') AND type in ('U'))
DROP TABLE [Card]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Client]') AND type in ('U'))
DROP TABLE [Client]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO

CREATE TABLE Client (
    id bigint IDENTITY(1,1),
	login varchar(30) NOT NULL UNIQUE,
	password varchar(60) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(30) NOT NULL,
	address varchar(100) NOT NULL,
	email varchar(50) NOT NULL,
	role tinyint NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,


	CONSTRAINT [PK_Client] PRIMARY KEY (id)
)

PRINT N'Table Client created.'
GO

CREATE TABLE Card (
	id bigint IDENTITY(1,1),
	number varchar(16) NOT NULL UNIQUE,
	cvv varchar(3) NOT NULL,
	expireDate date NOT NULL,
	clientId bigint NOT NULL,
	type bit NOT NULL,
	defaultCard bit NOT NULL,

	CONSTRAINT [PK_Card] PRIMARY KEY (id),
	CONSTRAINT [FK_CardClient] FOREIGN KEY (clientId) REFERENCES Client(id)
)

PRINT N'Table Card created.'
GO

CREATE TABLE Sale (
	id bigint IDENTITY(1,1),
	date datetime2 NOT NULL,
	descName varchar(100) NOT NULL,
	address varchar(100) NOT NULL,
	totalPrice float NOT NULL,
	cardId bigint NOT NULL,
	clientId bigint NOT NULL,

	CONSTRAINT [PK_Sale] PRIMARY KEY (id),
	CONSTRAINT [FK_SaleCard] FOREIGN KEY (cardId) REFERENCES Card(id),
	CONSTRAINT [FK_SaleClient] FOREIGN KEY (clientId) REFERENCES Client(id)
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
	entryDate datetime2 NOT NULL,
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
	date datetime2 NOT NULL,
	productId bigint NOT NULL,
	clientId bigint NOT NULL,



	CONSTRAINT [PK_Comment] PRIMARY KEY (id),
	CONSTRAINT [FK_CommentProduct] FOREIGN KEY (productId) REFERENCES Product(id),
	CONSTRAINT [FK_CommentClient] FOREIGN KEY (clientId) REFERENCES Client(id)
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

INSERT INTO Category(name)
VALUES
('Car');


INSERT INTO Category(name)
VALUES
('Bike');


INSERT INTO Category(name)
VALUES
('Computer');

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('McLaren 720S', 170000, CURRENT_TIMESTAMP, 1, '', 'The McLaren 720S', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Ferrari Pista 488', 245000, CURRENT_TIMESTAMP, 1, '', 'The Ferrari Pista 488', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('BMW Series 1 120i', 28000, CURRENT_TIMESTAMP, 1, '', 'The BMW Series 1', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('BMW Series 2 Grand Coupé', 34000, CURRENT_TIMESTAMP, 1, '', 'The BMW Series 2', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('BMW Series 3 320d', 38000, CURRENT_TIMESTAMP, 1, '', 'The BMW Series 3', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('BMW Series M2 Competition', 83000, CURRENT_TIMESTAMP, 1, '', 'The BMW M2 Series', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('BMW Series M4', 120000, CURRENT_TIMESTAMP, 1, '', 'The BMW M4 Series', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('BMW Series M8', 205150, CURRENT_TIMESTAMP, 1, '', 'The BMW M8 Series', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Audi A3 TFSI 3.0', 27500, CURRENT_TIMESTAMP, 1, '', 'The Audi A3', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Audi A4 TFSI 3.0', 38000, CURRENT_TIMESTAMP, 1, '', 'The Audi A4', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Audi A3 Sportback TFSI 3.0', 31050, CURRENT_TIMESTAMP, 1, '', 'The Audi A3', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Audi A3 TFSI 3.0', 27500, CURRENT_TIMESTAMP, 1, '', 'The Audi A3', 1);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Dell XPS 13 Intel i3', 1150, CURRENT_TIMESTAMP, 1, '', 'The Dell XPS', 3);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Dell XPS 13 Intel i5', 1400, CURRENT_TIMESTAMP, 1, '', 'The Dell XPS', 3);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Dell XPS 13 Intel i7', 1850, CURRENT_TIMESTAMP, 1, '', 'The Dell XPS', 3);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Dell XPS 13 Intel i9', 2300, CURRENT_TIMESTAMP, 1, '', 'The Dell XPS', 3);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Dell XPS 15 Intel i5', 1650, CURRENT_TIMESTAMP, 1, '', 'The Dell XPS', 3);

INSERT INTO Product(name, price, entryDate, stock, image, description, categoryId)
Values
('Dell Inspiron 13 Intel i5', 1250, CURRENT_TIMESTAMP, 1, '', 'The Dell inspiron', 3);