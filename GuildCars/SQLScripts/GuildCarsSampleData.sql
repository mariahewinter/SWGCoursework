USE GuildCars
GO

SELECT * FROM Contact

SET IDENTITY_INSERT Contact ON
 
INSERT INTO Contact (ContactID, [Name], Email, Phone, [Message])
VALUES (1, 'Mariah Winter', 'mariahewinter@gmail.com', '', '1G1AL52F957593553'),
       (2, 'Jonathan Winter', '', '507-525-2260', 'WBAWC73568E033744'),
	   (3, 'Evelyn Dorian', 'evdorian@gmail.com', '507-525-2270', 'JF1GE6A67BH517070')
SET IDENTITY_INSERT Contact OFF

SELECT * FROM Contact

SET IDENTITY_INSERT PurchaseType ON
 
INSERT INTO PurchaseType (PurchaseTypeID, PurchaseTypeName)
VALUES (1, 'Bank Finance'),
       (2, 'Cash'),
	   (3, 'Dealer Finance')
SET IDENTITY_INSERT PurchaseType OFF

SELECT * FROM PurchaseType

SET IDENTITY_INSERT Transmission ON
 
INSERT INTO Transmission (TransmissionID, TransmissionName)
VALUES (1, 'Automatic'),
       (2, 'Manual')
SET IDENTITY_INSERT PurchaseType OFF

SELECT * FROM PurchaseType

SET IDENTITY_INSERT [Type] ON
 
INSERT INTO [Type] (TypeID, TypeName)
VALUES (1, 'New'),
       (2, 'Used')
SET IDENTITY_INSERT [Type] OFF

SELECT * FROM [Type]

SET IDENTITY_INSERT BodyStyle ON
 
INSERT INTO BodyStyle (BodyStyleID, BodyStyleName)
VALUES (1, 'Car'),
       (2, 'SUV'),
	   (3, 'Truck'),
	   (4, 'Van')
SET IDENTITY_INSERT BodyStyle OFF

SELECT * FROM BodyStyle

SET IDENTITY_INSERT Make ON
 
INSERT INTO Make (MakeID, MakeName)
VALUES (1, 'Mazda'),
       (2, 'Kia'),
	   (3, 'Tesla')
SET IDENTITY_INSERT Make OFF

SELECT * FROM Make

SET IDENTITY_INSERT Model ON
 
INSERT INTO Model (ModelID, ModelName, MakeID)
VALUES (1, 'Mazda3', 1),
       (2, 'CX-9', 1),
	   (3, 'MX-5 Miata', 1),
	   (4, 'Soul', 2),
	   (5, 'Sedona', 2),
	   (6, 'Telluride', 2),
	   (7, 'Model 3', 3),
	   (8, 'Model S', 3),
	   (9, 'Model X', 3),
	   (10, 'Model Y', 3)
SET IDENTITY_INSERT Model OFF

SELECT * FROM Model 

SET IDENTITY_INSERT Color ON
 
INSERT INTO Color (ColorID, ColorName)
VALUES (1, 'Vegas Gold'),
       (2, 'Carmine Pink'),
	   (3, 'Tea Green'),
	   (4, 'Raw Umber'),
	   (5, 'Smoky Black')
SET IDENTITY_INSERT Color OFF

SELECT * FROM Color 

 
INSERT INTO Vehicle 
(VinNumber, MakeID, ModelID, BodyStyleID, TransmissionID, ExteriorColor, InteriorColor, 
[Year], Mileage, MSRP, SalePrice, [Description], Picture, IsFeatured, IsPurchased)
VALUES ('JF1GE6A67BH517070', 1, 1, 1, 1, 1, 1, 2020, 350, 21500.00, 19500.00, 'A Mazda3!', 'this is where the picture url goes', 0, 0),
	   ('5NPEB4AC8DH837877', 2, 4, 4, 2, 2, 2, 2015, 60000, 10000.00, 9000.00, 'A Kia Sedona!', 'this is where the picture url goes', 0, 0),
	   ('3VWCC21C6YM431294', 3, 8, 1, 1, 5, 4, 2020, 0, 79990.00, 75000.00, 'A Tesla S!', 'this is where the picture url goes', 0, 0),
	   ('1HGCM82613A006357', 1, 2, 2, 1, 5, 1, 2020, 250, 22000.00, 20000.00, 'A Mazda CX-9!', 'this is where the picture url goes', 0, 0),
	   ('2FAFP71W27X142935', 2, 1, 2, 1, 4, 4, 2015, 60000, 10000.00, 9000.00, 'A Kia Soul!', 'this is where the picture url goes', 0, 0),
	   ('WDDGF4HB5CA622883', 3, 10, 2, 1, 5, 4, 2020, 0, 52990.00, 50000.00, 'A Tesla Y!', 'this is where the picture url goes', 1, 0)



USE GuildCars
GO

SET IDENTITY_INSERT Special ON
 
INSERT INTO Special (SpecialID, SpecialTitle, SpecialDescription)
VALUES (1, 'Cupcake Special', 'Cotton candy sesame snaps fruitcake liquorice halvah dessert. Macaroon sugar plum chocolate cake chocolate bar chocolate bar carrot cake chupa chups wafer jujubes. Chocolate bonbon dessert ice cream brownie danish lemon drops croissant pie. Toffee chocolate danish ice cream cookie.'),
       (2, 'Cheese Special', 'Cheese slices queso goat. Jarlsberg melted cheese hard cheese cheese triangles jarlsberg halloumi mascarpone when the cheese comes out everybody is happy. Red leicester brie goat feta mozzarella cottage cheese boursin stinking bishop. Lancashire cheese on toast cheese triangles gouda.'),
	   (3, 'Zombie Special', 'Zombie ipsum reversus ab viral inferno, nam rick grimes malum cerebro. De carne lumbering animata corpora quaeritis. Summus brains sit??, morbo vel maleficia? De apocalypsi gorger omero undead survivor dictum mauris. Hi mindless mortuis soulless creaturas, imo evil stalking monstra.'),
	   (4, 'Pirate Special', 'Prow scuttle parrel provost Sail ho shrouds spirits boom mizzenmast yardarm. Pinnace holystone mizzenmast quarter crows nest nipperkin grog yardarm hempen halter furl. Swab barque interloper chantey doubloon starboard grog black jack gangway rutters. Arrrrrrrrghhhhhh mateeeeyyyyyyy.'),
	   (5, 'Jelly Donut Special', 'Powder jelly pudding jelly-o wafer bear claw jelly sesame snaps cake. Jelly-o chupa chups muffin sweet tiramisu ice cream cake. Gummi bears candy lollipop cheesecake fruitcake powder gingerbread macaroon apple pie.')
SET IDENTITY_INSERT Special OFF

SELECT * FROM Special 
