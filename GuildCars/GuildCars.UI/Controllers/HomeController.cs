using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new FeaturedVM();
            viewModel.FeaturedVehicles = VehicleRepositoryFactory.GetVehicleRepository().GetFeaturedVehicles();
            viewModel.Specials = SpecialRepositoryFactory.GetSpecialRepository().GetSpecials();
            viewModel.FeaturedMakes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            viewModel.FeaturedModels = ModelRepositoryFactory.GetModelRepository().GetModels();

            return View(viewModel);
        }

        public ActionResult Specials()
        {
            var viewModel = SpecialRepositoryFactory.GetSpecialRepository().GetSpecials();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Contact(string vinNumber)
        {
            var viewModel = new Contact();
            viewModel.Message = vinNumber;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Contact(Contact addContact)
        {
            if(ModelState.IsValid)
            {
                ContactRepositoryFactory.GetContactRepository().AddContact(addContact);
                return RedirectToAction("Index");
            }
            else
            {
                return View(addContact);
            }
        }
    }
}