USE GuildCars
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Contact')
	DROP TABLE Contact
GO

CREATE TABLE Contact (
	ContactID int identity(1,1) primary key not null,
	[Name] varchar(100) not null,
	Email varchar(200) null,
	Phone varchar(25) null,
	[Message] varchar(500) not null
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
	DROP TABLE PurchaseType
GO

CREATE TABLE PurchaseType (
	PurchaseTypeID int identity(1,1) primary key not null,
	PurchaseTypeName varchar(100) not null,
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='Transmission')
	DROP TABLE Transmission
GO

CREATE TABLE Transmission (
	TransmissionID int identity(1,1) primary key not null,
	TransmissionName varchar(100) not null,
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyle')
	DROP TABLE BodyStyle
GO

CREATE TABLE BodyStyle (
	BodyStyleID int identity(1,1) primary key not null,
	BodyStyleName varchar(100) not null,
)

-- ex: Ford, Kia, Tesla
IF EXISTS(SELECT * FROM sys.tables WHERE name='Make')
	DROP TABLE Make
GO

CREATE TABLE Make (
	MakeID int identity(1,1) primary key not null,
	MakeName varchar(100) not null,
	DateAdded date DEFAULT GETDATE(),
	[User] varchar(100) not null
)

-- ex: F-150, Taurus, Soul
IF EXISTS(SELECT * FROM sys.tables WHERE name='Model')
	DROP TABLE Model
GO

CREATE TABLE Model (
	ModelID int identity(1,1) primary key not null,
	ModelName varchar(100) not null,
	MakeID int not null,
		CONSTRAINT fk_Make FOREIGN KEY (MakeID)
		REFERENCES Make(MakeID),
	DateAdded date DEFAULT GETDATE(),
	[User] varchar(100) not null

)

IF EXISTS(SELECT * FROM sys.tables WHERE name='Color')
	DROP TABLE Color
GO

CREATE TABLE Color (
	ColorID int identity(1,1) primary key not null,
	ColorName varchar(20) not null,
)


IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicle')
	DROP TABLE Vehicle
GO

CREATE TABLE Vehicle (
	VinNumber varchar(17) primary key not null,
	ModelID int not null,
	CONSTRAINT fk_Model FOREIGN KEY (ModelID)
		REFERENCES Model(ModelID),
	MakeID int not null,
		CONSTRAINT fk_Make FOREIGN KEY (MakeID)
		REFERENCES Make(MakeID),
	BodyStyleID int not null,
	CONSTRAINT fk_BodyStyle FOREIGN KEY (BodyStyleID)
		REFERENCES BodyStyle(BodyStyleID),
	TransmissionID int not null,
	CONSTRAINT fk_Transmission FOREIGN KEY (TransmissionID)
		REFERENCES Transmission(TransmissionID),
	ExteriorColor int not null,
	CONSTRAINT fk_Exterior_Color FOREIGN KEY (ExteriorColor)
		REFERENCES Color(ColorID),
	InteriorColor int not null,
	CONSTRAINT fk_Interior_Color FOREIGN KEY (InteriorColor)
		REFERENCES Color(ColorID),
	[Year] int not null,
	Mileage int not null,
	MSRP decimal(8,2) not null,
	SalePrice decimal(8,2) not null,
	[Description] varchar(500) not null,
	Picture varchar(100) not null,
	IsFeatured bit not null default 0,
	IsPurchased bit not null default 0
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
	DROP TABLE PurchaseType
GO

-- financing for a vehicle, bank finance, dealer finance, etc...
CREATE TABLE PurchaseType (
	PurchaseTypeID int identity(1,1) primary key not null,
	PurchaseTypeName varchar(100)
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='Sale')
	DROP TABLE Sale
GO

CREATE TABLE Sale (
	SaleID int identity(1,1) primary key not null,
	UserID nvarchar(128) not null,
	CONSTRAINT fk_UserID FOREIGN KEY (UserID)
		REFERENCES AspNetUsers(Id),
	VinNumber varchar(17) not null,
	CONSTRAINT fk_VinNumber FOREIGN KEY (VinNumber)
		REFERENCES Vehicle(VinNumber),
	PurchaseTypeID int not null,
	CONSTRAINT fk_PurchaseTypeID FOREIGN KEY (PurchaseTypeID)
		REFERENCES PurchaseType(PurchaseTypeID),
	PurchaseDate date DEFAULT GETDATE() not null,
	PurchasePrice decimal(8,2) not null,
	[Name] varchar(100) not null,
	Email varchar(200) null,
	Phone varchar(25) null,
	Address1 varchar(100) not null,
	Address2 varchar(100) null,
	City varchar(100) not null,
	[State] varchar(2) not null,
	Zipcode varchar(5) not null
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='Color')
	DROP TABLE Color
GO

CREATE TABLE Color (
	ColorID int identity(1,1) primary key not null,
	ColorName varchar(100)
)


IF EXISTS(SELECT * FROM sys.tables WHERE name='Special')
	DROP TABLE Special
GO

CREATE TABLE Special (
	SpecialID int identity(1,1) primary key not null,
	SpecialTitle varchar(50),
	SpecialDescription varchar(500)
)
