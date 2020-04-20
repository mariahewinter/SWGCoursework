using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QAVehicleRepository : IVehicleRepository
    {
        public static List<Vehicle> _vehicles = new List<Vehicle>()
        {
            new Vehicle() {VinNumber="TESTONEC8DH837877", MakeID=1, ModelID=1, BodyStyleID=2, TransmissionID=1, ExteriorColor=2, InteriorColor=2, Year=2020, Mileage=0, MSRP=10001.00m, SalePrice=9600.00m, Description="A New Toyota RAV-4!", Picture="placeholder.png", IsFeatured=false, IsPurchased=true },
            new Vehicle() {VinNumber="3VWCC21C6YM431294", MakeID=2, ModelID=6, BodyStyleID=2, TransmissionID=2, ExteriorColor=5, InteriorColor=4, Year=2015, Mileage=1001, MSRP=10004.00m, SalePrice=9000.00m, Description="A Used Nissan Frontier!", Picture="placeholder.png", IsFeatured=true, IsPurchased=false },
            new Vehicle() {VinNumber="1HGCM82613A006357", MakeID=3, ModelID=8, BodyStyleID=2, TransmissionID=1, ExteriorColor=3, InteriorColor=1, Year=2019, Mileage=500, MSRP=10002.00m, SalePrice=8000.00m, Description="A New Honda CR-V!", Picture="placeholder.png", IsFeatured=true, IsPurchased=false },
            new Vehicle() {VinNumber="JF2SJAEC2EH401655", MakeID=1, ModelID=3, BodyStyleID=2, TransmissionID=2, ExteriorColor=5, InteriorColor=1, Year=2020, Mileage=250, MSRP=10005.00m, SalePrice=10000.00m, Description="A New Toyota Land Cruiser!", Picture="placeholder.png", IsFeatured=true, IsPurchased=false },
            new Vehicle() {VinNumber="WVGEK9BP5CD012507", MakeID=2, ModelID=5, BodyStyleID=2, TransmissionID=2, ExteriorColor=5, InteriorColor=4, Year=2017, Mileage=1002, MSRP=10003.00m, SalePrice=9200.00m, Description="A Used Nissan Sentra!", Picture="placeholder.png", IsFeatured=true, IsPurchased=false },
            new Vehicle() {VinNumber="TESTTWOPGCM826138", MakeID=3, ModelID=9, BodyStyleID=2, TransmissionID=1, ExteriorColor=3, InteriorColor=1, Year=2019, Mileage=1003, MSRP=10006.00m, SalePrice=9300.00m, Description="A Used Honda Odyssey!", Picture="placeholder.png", IsFeatured=false, IsPurchased=true },
            new Vehicle() {VinNumber="DUPLICATECM826138", MakeID=3, ModelID=9, BodyStyleID=2, TransmissionID=1, ExteriorColor=3, InteriorColor=1, Year=2019, Mileage=9999, MSRP=10007.00m, SalePrice=9300.00m, Description="A Used Honda Odyssey!", Picture="placeholder.png", IsFeatured=false, IsPurchased=false }
        };

        public List<Vehicle> GetVehicles()
        {
            return _vehicles;
        }

        public Vehicle GetVehicleByVIN(string vinNumber)
        {
            List<Vehicle> vehicles = _vehicles.Where(v => v.VinNumber == vinNumber).ToList();
            Vehicle requestedVehicle = vehicles[0];
            return requestedVehicle;
        }

        public Vehicle Add(Vehicle vehicle)
        {
            if(vehicle.Year < 2000 || vehicle.Year > 2021 || string.IsNullOrEmpty(vehicle.VinNumber) || vehicle.MSRP < 0 || vehicle.SalePrice < 0 || vehicle.SalePrice > vehicle.MSRP || string.IsNullOrEmpty(vehicle.Description) || string.IsNullOrEmpty(vehicle.Picture))
            {
                return null;
            }

            _vehicles.Add(vehicle);
            return vehicle;
        }

        public Vehicle Edit(Vehicle vehicle)
        {

            if (vehicle.Year < 2000 || vehicle.Year > 2021 || string.IsNullOrEmpty(vehicle.VinNumber) || vehicle.MSRP < 0 || vehicle.SalePrice < 0 || vehicle.SalePrice > vehicle.MSRP || string.IsNullOrEmpty(vehicle.Description) || string.IsNullOrEmpty(vehicle.Picture))
            {
                return null;
            }

            Vehicle found = _vehicles.FirstOrDefault(v => v.VinNumber == vehicle.VinNumber);

            if (found != null)
            {
                _vehicles.RemoveAll(v => v.VinNumber == vehicle.VinNumber);
            }

            _vehicles.Add(vehicle);
            return vehicle;
        }

        public void Delete(string vinNumber)
        {
            _vehicles.RemoveAll(v => v.VinNumber == vinNumber);
        }

        public List<Vehicle> GetFeaturedVehicles()
        {
            return _vehicles.Where(v => v.IsFeatured == true).ToList();
        }

        public List<Vehicle> GetVehiclesBySearchParameters(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            var makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            var models = ModelRepositoryFactory.GetModelRepository().GetModels();

            // used vehicle search, no search terms entered
            if (searchTerm == "" && priceMin == 0.00m && priceMax == 100000.00m && yearMin == 2000 && yearMax == 2021 && mileage == -1)
            {
                return _vehicles.Where(v => v.Mileage > 1000).OrderBy(v => v.MSRP).Take(20).ToList(); // 4 VEHICLES
            }
            // new vehicle search, no search terms entered
            else if (searchTerm == "" && priceMin == 0.00m && priceMax == 100000.00m && yearMin == 2000 && yearMax == 2021 && mileage == -2)
            {
                return _vehicles.Where(v => v.Mileage < 1000).OrderBy(v => v.MSRP).Take(20).ToList(); // 3 VEHICLES
            }
            // admin or sales vehicle search, no search terms entered
            else if (searchTerm == "" && priceMin == 0.00m && priceMax == 100000.00m && yearMin == 2000 && yearMax == 2021 && mileage == -3)
            {
                return _vehicles.OrderBy(v => v.MSRP).Take(20).ToList(); // 7 VEHICLES
            }
            else if (mileage == -1) // used vehicle, searchTerm, price min/max or year min/max was entered
            {

                List<Vehicle> vehicles = _vehicles.Where(v => v.Mileage > 1000).ToList();
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
                
                if(vehiclesToReturn.Count() > 0)
                {
                    vehiclesToReturn.RemoveAll(v => v.SalePrice < priceMin);
                    vehiclesToReturn.RemoveAll(v => v.SalePrice > priceMax);
                    vehiclesToReturn.RemoveAll(v => v.Year < yearMin);
                    vehiclesToReturn.RemoveAll(v => v.Year > yearMax);

                    return vehiclesToReturn;
                }
                else
                {
                    vehicles.RemoveAll(v => v.SalePrice < priceMin);
                    vehicles.RemoveAll(v => v.SalePrice > priceMax);
                    vehicles.RemoveAll(v => v.Year < yearMin);
                    vehicles.RemoveAll(v => v.Year > yearMax);
                }

                return vehicles;
            }
            else if (mileage == -2) // new vehicle, searchterm, price min/max or year min/max selected
            {
                List<Vehicle> vehicles = _vehicles.Where(v => v.Mileage < 1000).ToList();
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

                    return vehiclesToReturn;
                }
                else
                {
                    vehicles.RemoveAll(v => v.SalePrice < priceMin);
                    vehicles.RemoveAll(v => v.SalePrice > priceMax);
                    vehicles.RemoveAll(v => v.Year < yearMin);
                    vehicles.RemoveAll(v => v.Year > yearMax);
                }

                return vehicles;
            }
            else
            {
                List<Vehicle> vehicles = _vehicles;
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

                    return vehiclesToReturn;
                }
                else
                {
                    vehicles.RemoveAll(v => v.SalePrice < priceMin);
                    vehicles.RemoveAll(v => v.SalePrice > priceMax);
                    vehicles.RemoveAll(v => v.Year < yearMin);
                    vehicles.RemoveAll(v => v.Year > yearMax);
                }

                return vehicles;
            }
        }
    }

}