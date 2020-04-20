using GuildCars.Data.Factories;
using GuildCars.Models.Queries;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Used()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Details(string vinNumber)
        {
            var viewModel = new DetailsVM();
            viewModel.Vehicle = VehicleRepositoryFactory.GetVehicleRepository().GetVehicleByVIN(vinNumber);
            viewModel.BodyStyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            viewModel.Colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            viewModel.Makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            viewModel.Models = ModelRepositoryFactory.GetModelRepository().GetModels();
            viewModel.Transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

            return View(viewModel);
        }

    }
}