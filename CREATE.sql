CREATE DATABASE CarsSale;

USE CarsSale

GO

CREATE TABLE [REGION]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[NAME] NVARCHAR(50) UNIQUE NOT NULL,
	PRIMARY KEY ([ID])
);

CREATE TABLE [ROLE]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[NAME] nvarchar(10) UNIQUE NOT NULL,
	PRIMARY KEY([ID])
);

CREATE TABLE [USER]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[NAME] nvarchar(30) NOT NULL,
	[BIRTHDAY] DATE NOT NULL,
	[PHONE] CHAR(11) UNIQUE NOT NULL,
	[EMAIL] NVARCHAR(50) UNIQUE NOT NULL,
	[PASSWORD] NVARCHAR(30) NOT NULL,
	[LOGIN] NVARCHAR(30) UNIQUE NOT NULL,
	[ROLE_ID] INT NOT NULL,
	PRIMARY KEY ([ID]),
	FOREIGN KEY ([ROLE_ID]) REFERENCES [ROLE]([ID])
);

CREATE TABLE [BRAND]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[NAME] NVARCHAR(50) UNIQUE NOT NULL,
	PRIMARY KEY ([ID])
);

CREATE TABLE [VEHICL_TYPE]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[NAME] NVARCHAR(50) NOT NULL,
	PRIMARY KEY ([ID])
);

CREATE TABLE [ENGINE]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[VOLUME] INT UNIQUE NOT NULL,
	PRIMARY KEY ([ID])
);

CREATE TABLE [FUEL]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[NAME] NVARCHAR(10) UNIQUE NOT NULL,
	PRIMARY KEY ([ID])
);

CREATE TABLE [ENGINE_FUEL]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[ENGINE_ID] INT NOT NULL,
	[FUEL_ID] INT NOT NULL,
	PRIMARY KEY ([ID]),
	FOREIGN KEY ([ENGINE_ID]) REFERENCES ENGINE([ID]),
	FOREIGN KEY ([FUEL_ID]) REFERENCES FUEL([ID]),
	UNIQUE ([ENGINE_ID], [FUEL_ID])

);

CREATE TABLE [TRANSMISSION_TYPE]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[NAME] NVARCHAR(30) UNIQUE NOT NULL,
	PRIMARY KEY ([ID]),
);

CREATE TABLE [COMPLETESET]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[ENGINE_ID] INT NOT NULL,
	[TRANSMISSION_ID] INT NOT NULL,
	PRIMARY KEY ([ID]),
	FOREIGN KEY ([ENGINE_ID]) REFERENCES [ENGINE]([ID]),
	FOREIGN KEY ([TRANSMISSION_ID]) REFERENCES [TRANSMISSION_TYPE]([ID]),
	UNIQUE ([ENGINE_ID], [TRANSMISSION_ID])
);

CREATE TABLE [VEHICL]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[BRAND_ID] INT NOT NULL,
	[VEHICL_TYPE_ID] INT NOT NULL,
	[COMPLETESET_ID] INT NOT NULL,
	PRIMARY KEY ([ID]),
	FOREIGN KEY ([BRAND_ID]) REFERENCES [BRAND]([ID]),
	FOREIGN KEY ([VEHICL_TYPE_ID]) REFERENCES [VEHICL_TYPE]([ID]),
	FOREIGN KEY ([COMPLETESET_ID]) REFERENCES [COMPLETESET]([ID]),
	UNIQUE ([BRAND_ID], [VEHICL_TYPE_ID], [COMPLETESET_ID])
);

CREATE TABLE [ADVERTISEMENT]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[IS_ACTIVE] BIT NOT NULL,
	[EXPIRATION_DATE] DATETIME NOT NULL,
	[CREATED_DATE] DATETIME NOT NULL,
	[VEHICL_ID] INT NOT NULL,
	[USER_ID] INT NOT NULL,
	[REGION_ID] INT NOT NULL,
	PRIMARY KEY ([ID]),
	FOREIGN KEY ([USER_ID]) REFERENCES [USER]([ID]),
	FOREIGN KEY ([VEHICL_ID]) REFERENCES VEHICL([ID]),
	FOREIGN KEY ([REGION_ID]) REFERENCES REGION([ID])
);

GO

