USE GuildCars
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='BodyStylesSelectAll')
	DROP PROCEDURE BodyStylesSelectAll
GO

CREATE PROCEDURE BodyStylesSelectAll AS
BEGIN
	SELECT BodyStyleId, BodyStyleName
	FROM BodyStyle
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='ColorsSelectAll')
	DROP PROCEDURE ColorsSelectAll
GO

CREATE PROCEDURE ColorsSelectAll AS
BEGIN
	SELECT ColorId, ColorName
	FROM Color
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='ContactsSelectAll')
	DROP PROCEDURE ContactsSelectAll
GO

CREATE PROCEDURE ContactsSelectAll AS
BEGIN
	SELECT ContactId, [Name], Email, Phone, [Message] 
	FROM Contact
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactsAdd')
	DROP PROCEDURE ContactsAdd
GO


CREATE PROCEDURE ContactsAdd (
	@ContactID int,
	@Name varchar(100),
	@Email varchar(200),
	@Phone varchar(25),
	@Message varchar(500)
)
AS
	SET IDENTITY_INSERT Contact ON;
	INSERT INTO Contact (ContactID,[Name], Email, Phone, [Message])
	VALUES (@ContactID, @Name, @Email, @Phone, @Message)
	SET IDENTITY_INSERT Contact OFF;

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='MakesSelectAll')
	DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll AS
BEGIN
	SELECT MakeID, MakeName
	FROM Make
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesAdd')
	DROP PROCEDURE MakesAdd
GO


CREATE PROCEDURE MakesAdd (
	@MakeID int,
	@MakeName varchar(100)
)
AS
	SET IDENTITY_INSERT Make ON;
	INSERT INTO Make (MakeID, MakeName)
	VALUES (@MakeID, @MakeName)
	SET IDENTITY_INSERT Make OFF;

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='ModelsSelectAll')
	DROP PROCEDURE ModelsSelectAll
GO

CREATE PROCEDURE ModelsSelectAll AS
BEGIN
	SELECT ModelID, ModelName, MakeID
	FROM Model
END


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectByMakeID')
	DROP PROCEDURE ModelsSelectByMakeID
GO

CREATE PROCEDURE ModelsSelectByMakeID (
	@MakeID int
)
AS

SELECT * 
FROM Model
WHERE MakeID = @MakeID
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsAdd')
	DROP PROCEDURE ModelsAdd
GO


CREATE PROCEDURE ModelsAdd (
	@ModelID int,
	@ModelName varchar(100),
	@MakeID int
)
AS
	SET IDENTITY_INSERT Model ON;
	INSERT INTO Model (MakeID, ModelName, ModelID)
	VALUES (@MakeID, @ModelName, @ModelID)
	SET IDENTITY_INSERT Model OFF;

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='PurchaseTypesSelectAll')
	DROP PROCEDURE PurchaseTypesSelectAll
GO

CREATE PROCEDURE PurchaseTypesSelectAll AS
BEGIN
	SELECT PurchaseTypeID, PurchaseTypeName
	FROM PurchaseType
END

SELECT * FROM Sale

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='SalesSelectAll')
	DROP PROCEDURE SalesSelectAll
GO

CREATE PROCEDURE SalesSelectAll AS
BEGIN
	SELECT SaleID, UserID, VinNumber, PurchaseTypeID, PurchaseDate, PurchasePrice, Name, Email, Phone, Address1, Address2, City, State, Zipcode
	FROM Sale
END


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SalesAdd')
	DROP PROCEDURE SalesAdd
GO


CREATE PROCEDURE SalesAdd (
	@SaleID int,
	@UserID int, 
	@VinNumber varchar(17),
	@PurchaseTypeID int,
	@PurchaseDate date,
	@PurchasePrice decimal(8,2),
	@Name varchar(100),
	@Email varchar(200),
	@Phone varchar(25),
	@Address1 varchar(100),
	@Address2 varchar(100),
	@City varchar(100),
	@State varchar(2),
	@Zipcode varchar(5)
)
AS
	SET IDENTITY_INSERT Sale ON;
	INSERT INTO Sale (SaleID, UserID, VinNumber, PurchaseTypeID, PurchaseDate, PurchasePrice, Name, Email, Phone, Address1, Address2, City, State, Zipcode)
	VALUES (@SaleID, @UserID, @VinNumber, @PurchaseTypeID, @PurchaseDate, @PurchasePrice, @Name, @Email, @Phone, @Address1, @Address2, @City, @State, @Zipcode)
	SET IDENTITY_INSERT Sale OFF;

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='SpecialsSelectAll')
	DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
	SELECT SpecialID, SpecialTitle, SpecialDescription
	FROM Special
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsAdd')
	DROP PROCEDURE SpecialsAdd
GO


CREATE PROCEDURE SpecialsAdd (
	@SpecialID int,
	@SpecialTitle varchar(50),
	@SpecialDescription varchar(500)
)
AS
	SET IDENTITY_INSERT Special ON;
	INSERT INTO Special (SpecialID, SpecialTitle, SpecialDescription)
	VALUES (@SpecialID, @SpecialTitle, @SpecialDescription)
	SET IDENTITY_INSERT Special OFF;

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DeleteSpecial')
      DROP PROCEDURE DeleteSpecial
GO

CREATE PROCEDURE SpecialsDelete (
	@SpecialID int
)
AS
	DELETE FROM Special
	WHERE SpecialID = @SpecialID
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='TransmissionsSelectAll')
	DROP PROCEDURE TransmissionsSelectAll
GO

CREATE PROCEDURE TransmissionsSelectAll AS
BEGIN
	SELECT TransmissionID, TransmissionName
	FROM Transmission
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='VehiclesSelectAll')
	DROP PROCEDURE VehiclesSelectAll
GO

--VinNumber, ModelID, MakeID, BodyStyleID, TransmissionID, ExteriorColor, InteriorColor, [Year], Mileage, MSRP, SalePrice, Description, Picture, IsFeatured, IsPurchased

CREATE PROCEDURE VehiclesSelectAll AS
BEGIN
	SELECT *
	FROM Vehicle
END


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesSelectByVinNumber')
	DROP PROCEDURE VehiclesSelectByVinNumber
GO

CREATE PROCEDURE VehiclesSelectByVinNumber (
	@VinNumber varchar(17)
)
AS

SELECT * 
FROM Vehicle
WHERE VinNumber = @VinNumber
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'AddVehicle')
	DROP PROCEDURE AddVehicle
GO


CREATE PROCEDURE AddVehicle (
	@VinNumber varchar(17), 
	@ModelID int, 
	@MakeID int, 
	@BodyStyleID int, 
	@TransmissionID int, 
	@ExteriorColor int, 
	@InteriorColor int, 
	@Year varchar(4), 
	@Mileage int, 
	@MSRP decimal(8,2), 
	@SalePrice decimal(8,2), 
	@Description varchar(500), 
	@Picture varchar(100), 
	@IsFeatured bit, 
	@IsPurchased bit
)
AS
	INSERT INTO Vehicle (VinNumber, ModelID, MakeID, BodyStyleID, TransmissionID, ExteriorColor, InteriorColor, [Year], Mileage, MSRP, SalePrice, [Description], Picture, IsFeatured, IsPurchased
)
	VALUES (@VinNumber, @ModelID, @MakeID, @BodyStyleID, @TransmissionID, @ExteriorColor, @InteriorColor, @Year, @Mileage, @MSRP, @SalePrice, @Description, @Picture, @IsFeatured, @IsPurchased)
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'EditVehicle')
	DROP PROCEDURE EditVehicle
GO


CREATE PROCEDURE EditVehicle (
	@VinNumber varchar(17), 
	@ModelID int, 
	@MakeID int, 
	@BodyStyleID int, 
	@TransmissionID int, 
	@ExteriorColor int, 
	@InteriorColor int, 
	@Year varchar(4), 
	@Mileage int, 
	@MSRP decimal(8,2), 
	@SalePrice decimal(8,2), 
	@Description varchar(500), 
	@Picture varchar(100), 
	@IsFeatured bit, 
	@IsPurchased bit
)
AS
	INSERT INTO Vehicle (VinNumber, ModelID, MakeID, BodyStyleID, TransmissionID, ExteriorColor, InteriorColor, [Year], Mileage, MSRP, SalePrice, [Description], Picture, IsFeatured, IsPurchased
)
	VALUES (@VinNumber, @ModelID, @MakeID, @BodyStyleID, @TransmissionID, @ExteriorColor, @InteriorColor, @Year, @Mileage, @MSRP, @SalePrice, @Description, @Picture, @IsFeatured, @IsPurchased)
GO

SELECT * FROM Vehicle

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetVehiclesByParameters')
	DROP PROCEDURE GetVehiclesByParameters
GO

CREATE PROCEDURE GetVehiclesByParameters (
	@YearMin int,
	@YearMax int,
	@PriceMin decimal(8,2),
	@PriceMax decimal(8,2),
	@SearchTerm varchar(100)


)
AS

SELECT TOP 20 VinNumber, BodyStyleID, TransmissionID, ExteriorColor, InteriorColor, Mileage, MSRP, [Description], Picture, IsFeatured, IsPurchased, Model.MakeID, Model.ModelID, Make.MakeName, 
ModelName, [Year], SalePrice
FROM Vehicle
INNER JOIN Model ON Vehicle.ModelID = Model.ModelID
INNER JOIN Make ON Model.MakeID = Make.MakeID
WHERE (MakeName LIKE @SearchTerm +'%' OR ModelName LIKE @SearchTerm +'%' OR [Year] LIKE @SearchTerm +'%')
AND (SalePrice <= @PriceMax) AND (SalePrice >= @PriceMin) 
AND ([Year] <= @YearMax) AND ([Year] >= @YearMin)
GO



