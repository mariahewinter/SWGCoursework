using GuildCars.Data.Factories;
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
    public class QATests
    {
        [TestCase("", "Description")] // failing test, can't add w/o both title and desc
        [TestCase("Title", "")] // failing test, can't add w/o both title and desc
        [TestCase("Title", "Description")] // passing test
        public void CanAddAdminSpecials(string addTitle, string addDescription) // ----------------------ADD SPECIAL
        {
            var specialRepo = SpecialRepositoryFactory.GetSpecialRepository();

            var addSpecial = new Special();
            addSpecial.SpecialTitle = addTitle;
            addSpecial.SpecialDescription = addDescription;
            Special returnedSpecial = specialRepo.Add(addSpecial);


            if (returnedSpecial != null)
            {
                Assert.AreEqual(6, specialRepo.GetSpecials().Count());
                Assert.AreEqual(6, specialRepo.GetSpecials()[5].SpecialID);
                Assert.AreEqual("Title", specialRepo.GetSpecials()[5].SpecialTitle);
                Assert.AreEqual("Description", specialRepo.GetSpecials()[5].SpecialDescription);

            }
            else
            {
                Assert.AreEqual(5, specialRepo.GetSpecials().Count());
            }

        }

        [TestCase("10")] // pass
        [TestCase("1")] // no special w/this ID, pass
        public void CanDeleteAdminSpecials(int specialID)
        {
            var specials = SpecialRepositoryFactory.GetSpecialRepository().GetSpecials();

            Special special = SpecialRepositoryFactory.GetSpecialRepository().GetSpecials().FirstOrDefault(s => s.SpecialID == specialID);

            if (special == null)
            {
                Assert.AreEqual(5, specials.Count());
            }
            else
            {
                SpecialRepositoryFactory.GetSpecialRepository().Delete(special);
                Assert.AreEqual(4, specials.Count());
            }


        }

        // ADD VEHICLE -- REFACTOR AFTER IMPLEMENTING ACTUAL PICTURE UPLOADING? 

        // FAIL, description is blank
        [TestCase(1, "", 1, 1, true, false, 1, 1, 1001, 50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 2020)]
        // FAIL, year < 2000
        [TestCase(1, "test description", 1, 1, true, false, 1, 1, 1001, 50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 1999)]
        // FAIL, MSRP negative
        [TestCase(1, "", 1, 1, true, false, 1, 1, 1001, -50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 2020)]
        // FAIL, year > 2021
        [TestCase(1, "", 1, 1, true, false, 1, 1, 1001, -50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 2022)]
        // PASSING TEST
        [TestCase(1, "test description", 1, 1, true, false, 1, 1, 1001, 50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 2020)] // passing test!
        public void CanAddVehicle(int bodyStyleID, string description, int exteriorColor, int interiorColor, bool isFeatured, bool isPurchased, int makeID, int modelID, int mileage, decimal msrp, string picture, decimal salePrice, int transmissionID, string vinNumber, int year)
        {
            Vehicle addVehicle = new Vehicle() { BodyStyleID = bodyStyleID, Description = description, ExteriorColor = exteriorColor, InteriorColor = interiorColor, IsFeatured = isFeatured, IsPurchased = isPurchased, MakeID = makeID, ModelID = modelID, Mileage = mileage, MSRP = msrp, Picture = picture, SalePrice = salePrice, TransmissionID = transmissionID, VinNumber = vinNumber, Year = year };

            var repo = VehicleRepositoryFactory.GetVehicleRepository();

            var returnedVehicle = repo.Add(addVehicle);
            var vehicles = repo.GetVehicles();

            if (returnedVehicle != null)
            {
                Assert.AreEqual(7, vehicles.Count());
            }
            else
            {
                Assert.AreEqual(6, vehicles.Count());
            }

        }

        // EDIT VEHICLE
        // FAIL, year < 2000
        [TestCase(1, "test description", 1, 1, true, false, 1, 1, 1001, 50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 1999)]
        // FAIL, MSRP negative
        [TestCase(1, "", 1, 1, true, false, 1, 1, 1001, -50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 2020)]
        // FAIL, year > 2021
        [TestCase(1, "", 1, 1, true, false, 1, 1, 1001, -50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 2022)]
        //FAIL SalePrice > MSRP
        [TestCase(1, "", 1, 1, true, false, 1, 1, 1001, 50000.00, "placeholder.png", 60000.00, 1, "TEST1234667891234", 2022)]
        [TestCase(1, "test description", 1, 1, true, false, 1, 1, 1001, 50000.00, "placeholder.png", 45000.00, 1, "TEST1234667891234", 2020)] // passing test!
        public void CanEditVehicle(int bodyStyleID, string description, int exteriorColor, int interiorColor, bool isFeatured, bool isPurchased, int makeID, int modelID, int mileage, decimal msrp, string picture, decimal salePrice, int transmissionID, string vinNumber, int year)
        {
            Vehicle editVehicle = new Vehicle() { BodyStyleID = bodyStyleID, Description = description, ExteriorColor = exteriorColor, InteriorColor = interiorColor, IsFeatured = isFeatured, IsPurchased = isPurchased, MakeID = makeID, ModelID = modelID, Mileage = mileage, MSRP = msrp, Picture = picture, SalePrice = salePrice, TransmissionID = transmissionID, VinNumber = vinNumber, Year = year };

            editVehicle.Description = "This is the update!";

            var repo = VehicleRepositoryFactory.GetVehicleRepository();
            var returnedVehicle = repo.Edit(editVehicle);

            if (returnedVehicle != null)
            {
                var vehicleTest = VehicleRepositoryFactory.GetVehicleRepository().GetVehicleByVIN(vinNumber);

                Assert.AreEqual("This is the update!", vehicleTest.Description);
            }
            else
            {
                Assert.AreEqual(null, returnedVehicle);
            }

        }

        // DELETE VEHICLE
        [Test]
        public void CanDeleteVehicle()
        {
            var vehicles = VehicleRepositoryFactory.GetVehicleRepository().GetVehicles();

            var vehicleToDelete = vehicles[3];

            VehicleRepositoryFactory.GetVehicleRepository().Delete(vehicleToDelete.VinNumber);

            Assert.AreEqual(5, vehicles.Count());
        }

        [TestCase("", "", "", "Message")] // failing test, can't add w/o both name and message and phone or email
        [TestCase("Name", "", "", "")] // failing test, can't add w/o both name and message and phone or email
        [TestCase("", "", "Phone", "Message")] // failing test, can't add w/o name
        [TestCase("Name", "Email", "", "")] // failing test, can't add w/o message
        [TestCase("Name", "", "", "Message")] // failing test, can't add w/o phone or email
        [TestCase("Name", "Email", "Phone", "Message")] // passing test, all values
        [TestCase("Name", "Email", "", "Message")] // passing test, no phone
        [TestCase("Name", "", "Phone", "Message")] // passing test, no email
        public void CanAddContacts(string addName, string addEmail, string addPhone, string addMessage) // ----------------------ADD CONTACT
        {
            var contactRepo = ContactRepositoryFactory.GetContactRepository();

            var addContact = new Contact() { Name = addName, Email = addEmail, Phone = addPhone, Message = addMessage };



            Contact returnedContact = contactRepo.AddContact(addContact);


            if (returnedContact != null)
            {
                Assert.AreEqual(4, contactRepo.GetContacts().Count());
                Assert.AreEqual(4, contactRepo.GetContacts()[3].ContactID);
                Assert.AreEqual("Name", contactRepo.GetContacts()[3].Name);
                Assert.AreEqual("Message", contactRepo.GetContacts()[3].Message);

            }
            else
            {
                Assert.AreEqual(3, contactRepo.GetContacts().Count());
            }

        }

        //[TestCase("", 0.00, 100000.00, 2000, 2021, -1)] // used inventory test - no search terms entered
        //[TestCase("", 0.00, 100000.00, 2015, 2017, -1)] // used inventory test - year min/max entered
        //[TestCase("", 9000.00, 9200.00, 2000, 2021, -1)] // used inventory test - price min/max entered
        [TestCase("Ni", 0.00, 1000000.00, 2000, 2021, -1)] // used inventory test - searchTerm provided
        //[TestCase("Ni", 0.00, 9000.00, 2015, 2017, -1)] // used inventory test - searchTerm provided, priceMax set

        //[TestCase("", 0.00, 100000.00, 2000, 2021, -2)] // new inventory test - no search terms entered
        //[TestCase("", 0.00, 100000.00, 2020, 2021, -2)] // new inventory test - year min/max entered
        //[TestCase("", 9000.00, 9200.00, 2000, 2021, -1)] // new inventory test - price min/max entered
        //[TestCase("Ni", 0.00, 1000000.00, 2000, 2021, -1)] // new inventory test - searchTerm provided
        //[TestCase("Ni", 0.00, 9000.00, 2015, 2017, -1)] // new inventory test - searchTerm provided, priceMax set
        public void CanGetVehicleBySearchParameters(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            var vehicles = VehicleRepositoryFactory.GetVehicleRepository().GetVehicles();
            var makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            var models = ModelRepositoryFactory.GetModelRepository().GetModels();

            // used vehicle search, no search terms entered
            if (searchTerm == "" && priceMin == 0.00m && priceMax == 100000.00m && yearMin == 2000 && yearMax == 2021 && mileage == -1)
            {
                vehicles = vehicles.Where(v => v.Mileage > 1000).OrderBy(v => v.MSRP).Take(20).ToList();
                Assert.AreEqual(4, vehicles.Count());
            }
            // new vehicle search, no search terms entered
            else if (searchTerm == "" && priceMin == 0.00m && priceMax == 100000.00m && yearMin == 2000 && yearMax == 2021 && mileage == -2)
            {
                vehicles = vehicles.Where(v => v.Mileage < 1000).OrderBy(v => v.MSRP).Take(20).ToList();
                Assert.AreEqual(3, vehicles.Count());
            }
            // admin or sales vehicle search, no search terms entered
            else if (searchTerm == "" && priceMin == 0.00m && priceMax == 100000.00m && yearMin == 2000 && yearMax == 2021 && mileage == -3)
            {
                vehicles = vehicles.OrderBy(v => v.MSRP).Take(20).ToList();
                Assert.AreEqual(7, vehicles.Count());
            }
            else if (mileage == -1) // used vehicle, price min/max or year min/max was entered
            {

                vehicles = vehicles.Where(v => v.Mileage > 1000).ToList();
                var vehiclesToReturn = new List<Vehicle>();
                if (searchTerm != "")
                {

                    foreach (var vehicle in vehicles)
                    {
                        var model = new VehicleWithNames();

                        model.Vehicle = vehicle;
                        model.Model = models.FirstOrDefault(m => m.ModelID == vehicle.ModelID);
                        model.Make = makes.FirstOrDefault(m => m.MakeID == vehicle.MakeID);

                        if (model.Make.MakeName.Contains(searchTerm) || model.Model.ModelName.Contains(searchTerm) || model.Vehicle.Year.ToString().Contains(searchTerm))
                        {
                            vehiclesToReturn.Add(model.Vehicle);
                        }
                    }
                }

                if (vehiclesToReturn.Count() > 0)
                {
                    vehiclesToReturn.RemoveAll(v => v.SalePrice < priceMin);
                    vehiclesToReturn.RemoveAll(v => v.SalePrice > priceMax);
                    vehiclesToReturn.RemoveAll(v => v.Year < yearMin);
                    vehiclesToReturn.RemoveAll(v => v.Year > yearMax);

                    Assert.AreEqual(2, vehiclesToReturn.Count());
                }
                else
                {
                    vehicles.RemoveAll(v => v.SalePrice < priceMin);
                    vehicles.RemoveAll(v => v.SalePrice > priceMax);
                    vehicles.RemoveAll(v => v.Year < yearMin);
                    vehicles.RemoveAll(v => v.Year > yearMax);
                }

                Assert.AreEqual(2, vehicles.Count());
            }
            else if (mileage == -2)
            {
                vehicles = vehicles.Where(v => v.Mileage < 1000).ToList();
                Assert.AreEqual(3, vehicles.Count());

                if (searchTerm != "")
                {
                    foreach (var vehicle in vehicles)
                    {
                        var model = new VehicleWithNames();

                        model.Vehicle = vehicle;
                        model.Model = models.FirstOrDefault(m => m.ModelID == vehicle.ModelID);
                        model.Make = makes.FirstOrDefault(m => m.MakeID == vehicle.MakeID);

                        if (model.Make.MakeName.Contains(searchTerm) || model.Model.ModelName.Contains(searchTerm) || model.Vehicle.Year.ToString().Contains(searchTerm))
                        {
                            vehicles.Add(model.Vehicle);
                        }

                    }
                }

                vehicles.RemoveAll(v => v.SalePrice < priceMin);
                vehicles.RemoveAll(v => v.SalePrice > priceMax);
                vehicles.RemoveAll(v => v.Year < yearMin);
                vehicles.RemoveAll(v => v.Year > yearMax);

                throw new NotImplementedException();
            }
        }
    }
}
