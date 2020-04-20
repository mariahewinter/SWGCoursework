using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class SearchController : ApiController
    {
        [Route("api/Inventory/Used/SearchTerm/{searchTerm}/PriceMin/{priceMin}/PriceMax/{priceMax}/YearMin/{yearMin}/YearMax/{yearMax}/Mileage/{mileage}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchUsedVehicles(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            var vehicleRepo = VehicleRepositoryFactory.GetVehicleRepository();
            var bodystyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            var colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            var makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            var models = ModelRepositoryFactory.GetModelRepository().GetModels();
            var transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

            if (searchTerm == "chipmunk")
            {
                searchTerm = "";
            }

            try
            {
                var vehicles = vehicleRepo.GetVehiclesBySearchParameters(searchTerm, priceMin, priceMax, yearMin, yearMax, mileage);
                var vehiclesToReturn = new List<VehicleDisplayVM>();
                foreach (var vehicle in vehicles)
                {
                    var model = new VehicleDisplayVM();

                    model.Vehicle = vehicle;
                    model.Model = models.FirstOrDefault(m => m.ModelID == vehicle.ModelID);
                    model.Make = makes.FirstOrDefault(m => m.MakeID == vehicle.MakeID);
                    model.BodyStyle = bodystyles.FirstOrDefault(b => b.BodyStyleID == vehicle.BodyStyleID);
                    model.ExteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.ExteriorColor);
                    model.InteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.InteriorColor);
                    model.Transmission = transmission.FirstOrDefault(t => t.TransmissionID == vehicle.TransmissionID);

                    vehiclesToReturn.Add(model);

                }
                var viewModel = vehiclesToReturn.Where(v => v.Vehicle.Mileage > 1000).ToList();
                return Ok(viewModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("api/Admin/Vehicles/SearchTerm/{searchTerm}/PriceMin/{priceMin}/PriceMax/{priceMax}/YearMin/{yearMin}/YearMax/{yearMax}/Mileage/{mileage}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchAdminVehicles(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            var vehicleRepo = VehicleRepositoryFactory.GetVehicleRepository();
            var bodystyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            var colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            var makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            var models = ModelRepositoryFactory.GetModelRepository().GetModels();
            var transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

            if (searchTerm == "chipmunk")
            {
                searchTerm = "";
            }

            try
            {
                var vehicles = vehicleRepo.GetVehiclesBySearchParameters(searchTerm, priceMin, priceMax, yearMin, yearMax, mileage);
                var vehiclesToReturn = new List<VehicleDisplayVM>();
                foreach (var vehicle in vehicles)
                {
                    var model = new VehicleDisplayVM();

                    model.Vehicle = vehicle;
                    model.Model = models.FirstOrDefault(m => m.ModelID == vehicle.ModelID);
                    model.Make = makes.FirstOrDefault(m => m.MakeID == vehicle.MakeID);
                    model.BodyStyle = bodystyles.FirstOrDefault(b => b.BodyStyleID == vehicle.BodyStyleID);
                    model.ExteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.ExteriorColor);
                    model.InteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.InteriorColor);
                    model.Transmission = transmission.FirstOrDefault(t => t.TransmissionID == vehicle.TransmissionID);

                    vehiclesToReturn.Add(model);

                }
                vehiclesToReturn.RemoveAll(v => v.Vehicle.IsPurchased == true);
                return Ok(vehiclesToReturn);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("api/Sales/Index/SearchTerm/{searchTerm}/PriceMin/{priceMin}/PriceMax/{priceMax}/YearMin/{yearMin}/YearMax/{yearMax}/Mileage/{mileage}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesVehicles(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            var vehicleRepo = VehicleRepositoryFactory.GetVehicleRepository();
            var bodystyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            var colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            var makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            var models = ModelRepositoryFactory.GetModelRepository().GetModels();
            var transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

            if (searchTerm == "chipmunk")
            {
                searchTerm = "";
            }

            try
            {
                var vehicles = vehicleRepo.GetVehiclesBySearchParameters(searchTerm, priceMin, priceMax, yearMin, yearMax, mileage);
                var vehiclesToReturn = new List<VehicleDisplayVM>();
                foreach (var vehicle in vehicles)
                {
                    var model = new VehicleDisplayVM();

                    model.Vehicle = vehicle;
                    model.Model = models.FirstOrDefault(m => m.ModelID == vehicle.ModelID);
                    model.Make = makes.FirstOrDefault(m => m.MakeID == vehicle.MakeID);
                    model.BodyStyle = bodystyles.FirstOrDefault(b => b.BodyStyleID == vehicle.BodyStyleID);
                    model.ExteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.ExteriorColor);
                    model.InteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.InteriorColor);
                    model.Transmission = transmission.FirstOrDefault(t => t.TransmissionID == vehicle.TransmissionID);

                    vehiclesToReturn.Add(model);

                }
                vehiclesToReturn.RemoveAll(v => v.Vehicle.IsPurchased == true);
                return Ok(vehiclesToReturn);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("api/Admin/AddVehicle/MakeID/{makeID}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAddVehicleModels(int makeID)
        {

            try
            {
                var models = ModelRepositoryFactory.GetModelRepository().GetModelsByMakeID(makeID);
                return Ok(models);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("api/Admin/EditVehicle/MakeID/{makeID}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetEditVehicleModels(int makeID)
        {
            try
            {
                var models = ModelRepositoryFactory.GetModelRepository().GetModelsByMakeID(makeID);
                return Ok(models);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("api/Admin/Specials/SpecialID/{specialID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int specialID)
        {
            Special special = SpecialRepositoryFactory.GetSpecialRepository().GetSpecials().FirstOrDefault(s => s.SpecialID == specialID);

            if (special == null)
            {
                return NotFound();
            }

            SpecialRepositoryFactory.GetSpecialRepository().Delete(special);
            return Ok();
        }

        [Route("api/Admin/Vehicles/VinNumber/{vinNumber}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(string vinNumber)
        {
            Vehicle vehicle = VehicleRepositoryFactory.GetVehicleRepository().GetVehicleByVIN(vinNumber);

            if (vehicle == null)
            {
                return NotFound();
            }

            VehicleRepositoryFactory.GetVehicleRepository().Delete(vinNumber);
            return Ok();
        }

        [Route("api/Inventory/New/SearchTerm/{searchTerm}/PriceMin/{priceMin}/PriceMax/{priceMax}/YearMin/{yearMin}/YearMax/{yearMax}/Mileage/{mileage}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchNewVehicles(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage)
        {
            var vehicleRepo = VehicleRepositoryFactory.GetVehicleRepository();
            var bodystyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            var colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            var makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            var models = ModelRepositoryFactory.GetModelRepository().GetModels();
            var transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

            if (searchTerm == "chipmunk")
            {
                searchTerm = "";
            }

            try
            {
                var vehicles = vehicleRepo.GetVehiclesBySearchParameters(searchTerm, priceMin, priceMax, yearMin, yearMax, mileage);
                var vehiclesToReturn = new List<VehicleDisplayVM>();
                foreach (var vehicle in vehicles)
                {
                    var model = new VehicleDisplayVM();

                    model.Vehicle = vehicle;
                    model.Model = models.FirstOrDefault(m => m.ModelID == vehicle.ModelID);
                    model.Make = makes.FirstOrDefault(m => m.MakeID == vehicle.MakeID);
                    model.BodyStyle = bodystyles.FirstOrDefault(b => b.BodyStyleID == vehicle.BodyStyleID);
                    model.ExteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.ExteriorColor);
                    model.InteriorColor = colors.FirstOrDefault(c => c.ColorID == vehicle.InteriorColor);
                    model.Transmission = transmission.FirstOrDefault(t => t.TransmissionID == vehicle.TransmissionID);

                    vehiclesToReturn.Add(model);

                }
                var viewModel = vehiclesToReturn.Where(v => v.Vehicle.Mileage < 1000).ToList();
                return Ok(viewModel);
            }
            catch
            {
                return BadRequest();
            }

        }
    }

}

