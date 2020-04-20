using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.PROD;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class ADOTests
    {
        [Test]
        public void CanLoadColors()
        {
            var repo = new ColorRepositoryADO();
            var colors = repo.GetColors();

            Assert.AreEqual(5, colors.Count());
            Assert.AreEqual("Vegas Gold", colors[0].ColorName);

        }

        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepositoryADO();
            var bodyStyles = repo.GetBodyStyles();

            Assert.AreEqual(4, bodyStyles.Count());
            Assert.AreEqual(4, bodyStyles[3].BodyStyleID);
            Assert.AreEqual("Truck", bodyStyles[2].BodyStyleName);

        }


        [Test]
        public void CanLoadContacts()
        {
            var repo = new ContactRepositoryADO();
            var contacts = repo.GetContacts();

            Assert.AreEqual(3, contacts.Count());
            Assert.AreEqual(3, contacts[2].ContactID);
            Assert.AreEqual("Evelyn Dorian", contacts[2].Name);
            Assert.AreEqual("", contacts[0].Phone);

        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakeRepositoryADO();
            var makes = repo.GetMakes();

            Assert.AreEqual(3, makes.Count());
            Assert.AreEqual(3, makes[2].MakeID);
            Assert.AreEqual("Kia", makes[1].MakeName);
        }

        [Test]
        public void CanLoadModels()
        {
            var repo = new ModelRepositoryADO();
            var models = repo.GetModels();

            Assert.AreEqual(10, models.Count());
            Assert.AreEqual(1, models[2].MakeID);
            Assert.AreEqual("Model X", models[8].ModelName);
        }

        [TestCase(0)] // failing test -- there is no make with ID 0
        [TestCase(1)] // can get just Mazda Makes
        [TestCase(2)] // can get just Kia Makes
        [TestCase(3)] // can get just Tesla Makes
        public void CanLoadModelsByMakeID(int makeID)
        {
            var repo = new ModelRepositoryADO();
            var models = repo.GetModelsByMakeID(makeID);

            if (makeID == 1)
            {
                Assert.AreEqual(3, models.Count());
                Assert.AreEqual(2, models[1].ModelID);
                Assert.AreEqual("MX-5 Miata", models[2].ModelName);

            }
            else if (makeID == 2)
            {
                Assert.AreEqual(3, models.Count());

            }
            else if (makeID == 3)
            {
                Assert.AreEqual(4, models.Count());
            }
            else
            {
                Assert.AreEqual(models.Count(), 0);
            }
        }

        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypeRepositoryADO();
            var purchaseTypes = repo.GetPurchaseTypes();

            Assert.AreEqual(3, purchaseTypes.Count());
            Assert.AreEqual(1, purchaseTypes[0].PurchaseTypeID);
            Assert.AreEqual("Cash", purchaseTypes[1].PurchaseTypeName);
        }

        //[Test] --- I HAVEN'T ADDED ANY SALES TO LOAD YET.......
        //public void CanLoadSales()
        //{
        //    var repo = new SaleRepositoryADO();
        //    var sales = repo.GetSales();

        //    Assert.AreEqual(3, sales.Count());
        //    Assert.AreEqual(1, sales[0].SaleID);
        //    Assert.AreEqual("", sales[1].Name);
        //} 

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialRepositoryADO();
            var specials = repo.GetSpecials();

            Assert.AreEqual(5, specials.Count());
            Assert.AreEqual(1, specials[0].SpecialID);
            Assert.AreEqual("Zombie Special", specials[2].SpecialTitle);
        }

        [Test]
        public void CanLoadTransmissions()
        {
            var repo = new TransmissionTypeRepositoryADO();
            var transmissions = repo.GetTransmissions();

            Assert.AreEqual(2, transmissions.Count());
            Assert.AreEqual(1, transmissions[0].TransmissionID);
            Assert.AreEqual("Manual", transmissions[1].TransmissionName);
        }

        [Test]
        public void CanLoadVehicles()
        {
            var repo = new VehicleRepositoryADO();
            var vehicles = repo.GetVehicles();

            Assert.AreEqual(6, vehicles.Count());
            Assert.AreEqual("1HGCM82613A006357", vehicles[0].VinNumber);
            Assert.AreEqual(false, vehicles[4].IsFeatured);

        }

        [TestCase("12345678910ABCDEFG")] // failing test -- there is no vehicle in database w/VinNumber "12345678910ABCDEFG"
        [TestCase("3VWCC21C6YM431294")] // can get vehicle w/all columns aligned correctly
        public void CanLoadVehiclesByVin(string vinNumber)
        {
            var repo = new VehicleRepositoryADO();
            var requestedVehicle = repo.GetVehicleByVIN(vinNumber);

            if (requestedVehicle != null)
            {
                Assert.AreEqual(requestedVehicle.ModelID, 8);
                Assert.AreEqual(requestedVehicle.MakeID, 3);
                Assert.AreEqual(requestedVehicle.BodyStyleID, 1);
                Assert.AreEqual(requestedVehicle.TransmissionID, 1);
                Assert.AreEqual(requestedVehicle.ExteriorColor, 5);
                Assert.AreEqual(requestedVehicle.InteriorColor, 4);
                Assert.AreEqual(requestedVehicle.Mileage, 0);
                Assert.AreEqual(requestedVehicle.MSRP, 79990.00m);
                Assert.AreEqual(requestedVehicle.SalePrice, 75000.00m);
                Assert.AreEqual(requestedVehicle.Description, "A Tesla S!");
                Assert.AreEqual(requestedVehicle.Picture, "this is where the picture url goes");
                Assert.AreEqual(requestedVehicle.IsFeatured, false);
                Assert.AreEqual(requestedVehicle.IsPurchased, false);

            }


        }

        [Test]
        public void CanLoadFeaturedVehicles()
        {
            var repo = new VehicleRepositoryADO();
            var featuredvehicles = repo.GetFeaturedVehicles();

            if (featuredvehicles != null)
            {
                Assert.AreEqual(1, featuredvehicles.Count());
                Assert.AreEqual("WDDGF4HB5CA622883", featuredvehicles[0].VinNumber);
                Assert.AreEqual(true, featuredvehicles[0].IsFeatured);

            }
        }

        [TestCase("", 25000.00, 75000.00, 2000, 2021, 999)]
        public void CanLoadVehiclesBySearchParameters(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            var repo = new VehicleRepositoryADO();
            var foundVehicles = repo.GetVehiclesBySearchParameters(searchTerm, priceMin, priceMax, yearMin, yearMax, mileage);

            if (foundVehicles != null)
            {
                Assert.AreEqual(2, foundVehicles.Count());
            }
        }
    }
}

