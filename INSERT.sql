USE CarsSale

GO

SET IDENTITY_INSERT [ROLE] ON

INSERT INTO [ROLE]([ID], [NAME]) VALUES
	(1, 'USER'),
	(2, 'ADMIN')

SET IDENTITY_INSERT [ROLE] OFF

GO

GO

SET IDENTITY_INSERT [USER] ON

INSERT INTO [USER]([ID], [BIRTHDAY], [EMAIL], [LOGIN], [PHONE], [NAME], [PASSWORD], [ROLE_ID]) VALUES
	(1, '1993-08-14 00:00:00.000', 'OSTAPPANKEVYCH@GMAIL.COM', 'OPANKEVYCH', '0934220664', 'OSTAP PANKEVYCH', 'PANKEVYCH', 1)

SET IDENTITY_INSERT [USER] OFF

GO

SET IDENTITY_INSERT [REGION] ON

INSERT INTO [REGION]([ID], [NAME]) VALUES
	(1, 'ODESA'),
	(2, 'DNIPROPETROVS'),
	(3, 'CHERNIHIV'),
	(4, 'KHARKIV'),
	(5, 'ZHYTOMYR'),
	(6, 'POLTAVA'),
	(7, 'KHERSON'),
	(8, 'KIEV'),
	(9, 'ZAPORIZHIA'),
	(10, 'LUHANSK'),
	(11, 'DENETSK'),
	(12, 'VINNYTSIA'),
	(13, 'MYKOLAIV'),
	(14, 'KIROVOHRAD'),
	(15, 'SUMY'),
	(16, 'LVIV')

SET IDENTITY_INSERT [REGION] OFF

GO

SET IDENTITY_INSERT [BRAND] ON

INSERT INTO [BRAND]([ID], [NAME]) VALUES
	(1, 'VOLKSWAGEN'),
	(2, 'AUDI'),
	(3, 'LADA'),
	(4, 'BMW')

SET IDENTITY_INSERT [BRAND] OFF

GO

SET IDENTITY_INSERT [VEHICL_TYPE] ON

INSERT INTO [VEHICL_TYPE]([ID], [NAME]) VALUES
	(1, 'MICRO'),
	(2, 'HATCHBACK'),
	(3, 'MINIVAN'),
	(4, 'CAMPERVAN'),
	(5, 'TRUCK'),
	(6, 'BIG_TRUCK'),
	(7, 'MINI_TRUCK'),
	(8, 'CABRIOLET'),
	(9, 'SEDAN'),
	(10, 'BUS'),
	(11, 'MOTORCYCLE'),
	(12, 'VAN'),
	(13, 'MINI VAN')

SET IDENTITY_INSERT [VEHICL_TYPE] OFF

GO

SET IDENTITY_INSERT [TRANSMISSION_TYPE] ON

INSERT INTO [TRANSMISSION_TYPE]([ID], [NAME]) VALUES
	(1, 'HAND'),
	(2, 'AUTO')

SET IDENTITY_INSERT [TRANSMISSION_TYPE] OFF

GO