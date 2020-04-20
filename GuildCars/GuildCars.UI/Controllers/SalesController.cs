using GuildCars.Data.Factories;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin,Sales")]
    public class SalesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Purchase(string vinNumber)
        {
            var details = new DetailsVM();
            details.Vehicle = VehicleRepositoryFactory.GetVehicleRepository().GetVehicleByVIN(vinNumber);
            details.BodyStyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            details.Colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            details.Makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            details.Models = ModelRepositoryFactory.GetModelRepository().GetModels();
            details.Transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

            var viewModel = new SaleVM();
            viewModel.Sale = new GuildCars.Models.Tables.Sale();
            viewModel.Sale.VinNumber = vinNumber;
            viewModel.Details = details;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Purchase(SaleVM sold)
        {
            var details = new DetailsVM();
            details.Vehicle = VehicleRepositoryFactory.GetVehicleRepository().GetVehicleByVIN(sold.Sale.VinNumber);
            details.BodyStyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            details.Colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            details.Makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            details.Models = ModelRepositoryFactory.GetModelRepository().GetModels();
            details.Transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

            sold.Details = details;

            if (sold.Sale.Phone == null && sold.Sale.Email == null)
            {
                ModelState.AddModelError("", "You must enter a phone number or an email address.");
            }
            if(sold.Sale.Zipcode.Length > 5)
            {
                ModelState.AddModelError("", "Zipcode should only be 5 digits long, please.");
            }
            if(sold.Sale.PurchasePrice < ((sold.Details.Vehicle.MSRP * 95) * .01m))
            {
                ModelState.AddModelError("", "Purchase price should not be lower than 95% vehicle MSRP, please recalculate.");
            }
            if(sold.Sale.PurchasePrice > sold.Details.Vehicle.MSRP)
            {
                ModelState.AddModelError("", "Purchase price should not be greater than MSRP, please recalculate.");
            }


            var repo = SaleRepositoryFactory.GetSaleRepository();
            if (ModelState.IsValid)
            {
                sold.Sale.UserID = User.Identity.GetUserId();
                sold.Sale.PurchaseDate = DateTime.Now;
                
            }

            var result = repo.Add(sold.Sale);
            if(result != null)
            {
                var vehicleToUpdate = VehicleRepositoryFactory.GetVehicleRepository().GetVehicleByVIN(sold.Sale.VinNumber);
                vehicleToUpdate.IsPurchased = true;
                vehicleToUpdate.IsFeatured = false;
                VehicleRepositoryFactory.GetVehicleRepository().Edit(vehicleToUpdate);

                return View("Index");
            }


            // assign sold.UserID to currently logged in user
            // assign sold.PurchaseDate to DateTime.Now
            // if saving the purchase is a success, update vehicle.IsPurchased to true and vehicle.IsFeatured to false;  
            return View();
        }
        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public SalesController()
        {
        }

        public SalesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}