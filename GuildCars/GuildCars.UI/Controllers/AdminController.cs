using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult Vehicles()
        {
            return View();
        }

        public ActionResult AddVehicle()
        {
            var viewModel = new DetailsVM();
            viewModel.BodyStyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            viewModel.Colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            viewModel.Makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            viewModel.Models = ModelRepositoryFactory.GetModelRepository().GetModels();
            viewModel.Transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddVehicle(DetailsVM addVehicle)
        {
            if (addVehicle.Vehicle.Year < 2000 || addVehicle.Vehicle.Year > 2021)
            {
                ModelState.AddModelError("Vehicle.Year", "Please enter a year between 2000 and 2021.");
            }
            if (addVehicle.Vehicle.MSRP < 0)
            {
                ModelState.AddModelError("Vehicle.MSRP", "MSRP must be a positive number.");
            }
            if (addVehicle.Vehicle.SalePrice < 0)
            {
                ModelState.AddModelError("Vehicle.SalePrice", "SalePrice must be a positive number.");
            }
            if (addVehicle.Vehicle.MSRP < addVehicle.Vehicle.SalePrice)
            {
                ModelState.AddModelError("Vehicle.MSRP", "SalePrice must be lower than MSRP.");
            }
            if (ModelState.IsValid)
            {
                addVehicle.Vehicle.Picture = "placeholder.png";
                VehicleRepositoryFactory.GetVehicleRepository().Add(addVehicle.Vehicle);
                return RedirectToAction("EditVehicle", new { vinNumber = addVehicle.Vehicle.VinNumber });
            }
            else
            {
                var viewModel = addVehicle;
                viewModel.BodyStyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
                viewModel.Colors = ColorRepositoryFactory.GetColorRepository().GetColors();
                viewModel.Makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
                viewModel.Models = ModelRepositoryFactory.GetModelRepository().GetModels();
                viewModel.Transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult EditVehicle(string vinNumber)
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

        [HttpPost]
        public ActionResult EditVehicle(DetailsVM editVehicle)
        {
            if (editVehicle.Vehicle.Year < 2000 || editVehicle.Vehicle.Year > 2021)
            {
                ModelState.AddModelError("Vehicle.Year", "Please enter a year between 2000 and 2021.");
            }
            if (editVehicle.Vehicle.MSRP <= 0)
            {
                ModelState.AddModelError("Vehicle.MSRP", "MSRP must be greater than $0.");
            }
            if (editVehicle.Vehicle.SalePrice < 0)
            {
                ModelState.AddModelError("Vehicle.SalePrice", "SalePrice must be greater than $0.");
            }
            if (editVehicle.Vehicle.MSRP < editVehicle.Vehicle.SalePrice)
            {
                ModelState.AddModelError("Vehicle.MSRP", "SalePrice must be lower than MSRP.");
            }
            if (ModelState.IsValid)
            {
                editVehicle.Vehicle.Picture = "placeholder.png";
                VehicleRepositoryFactory.GetVehicleRepository().Edit(editVehicle.Vehicle);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var viewModel = new DetailsVM();
                viewModel.Vehicle = editVehicle.Vehicle;
                viewModel.BodyStyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
                viewModel.Colors = ColorRepositoryFactory.GetColorRepository().GetColors();
                viewModel.Makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
                viewModel.Models = ModelRepositoryFactory.GetModelRepository().GetModels();
                viewModel.Transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();

                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var viewModel = new SpecialVM();
            viewModel.AdminSpecials = SpecialRepositoryFactory.GetSpecialRepository().GetSpecials().ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Specials(Special addSpecial)
        {
            if (ModelState.IsValid)
            {
                SpecialRepositoryFactory.GetSpecialRepository().Add(addSpecial);
                return RedirectToAction("Specials");
            }
            else
            {
                return View(addSpecial);
            }
        }

        [HttpGet]
        public ActionResult Makes()
        {
            var viewModel = new AddMake();
            viewModel.AdminMakes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            foreach(var make in viewModel.AdminMakes)
            {
                make.UserID = UserManager.FindById(make.UserID).Email;
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Makes(AddMake make)
        {
            if (ModelState.IsValid)
            {
                var addMake = new Make() { MakeID = make.MakeID, MakeName = make.MakeName, DateAdded = DateTime.Now, UserID = User.Identity.GetUserName() };
                MakeRepositoryFactory.GetMakeRepository().AddMake(addMake);
                return RedirectToAction("Makes", "Admin");

            }
            else
            {
                return View(make);
            }
        }

        [HttpGet]
        public ActionResult Models()
        {
            var viewModel = new AddModel();
            viewModel.AdminMakes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            viewModel.AdminModels = ModelRepositoryFactory.GetModelRepository().GetModels();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Models(AddModel model)
        {
            if (ModelState.IsValid)
            {
                var addModel = new Model() { MakeID = model.MakeID, ModelID = model.ModelID, ModelName = model.ModelName, DateAdded = DateTime.Now, UserID = User.Identity.GetUserName() };
                ModelRepositoryFactory.GetModelRepository().AddModel(addModel);

                var viewModel = new AddModel();
                viewModel.AdminMakes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
                viewModel.AdminModels = ModelRepositoryFactory.GetModelRepository().GetModels();
                return View(viewModel);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Users()
        {
            var context = new ApplicationDbContext();

            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      Email = user.Email,
                                      Role = (from userRole in user.Roles
                                              join role in context.Roles on userRole.RoleId
                                              equals role.Id
                                              select role.Name).ToList()
                                  }).ToList().Select(p => new UsersVM()

                                  {
                                      UserID = p.UserId,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      Email = p.Email,
                                      Role = string.Join(",", p.Role)
                                  });

            return View(usersWithRoles.ToList());

        }



        [HttpGet]
        public ActionResult AddUser()
        {
            var context = new ApplicationDbContext();
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UsersVM model)
        {
            var context = new ApplicationDbContext();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, model.Role);
                    return RedirectToAction("Users");
                }
                ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        public ActionResult EditUser(string UserID)
        {
            var context = new ApplicationDbContext();

            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      Email = user.Email,
                                      Role = (from userRole in user.Roles
                                              join role in context.Roles on userRole.RoleId
                                              equals role.Id
                                              select role.Name).ToList()
                                  }).ToList().Select(p => new UsersVM()

                                  {
                                      UserID = p.UserId,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      Email = p.Email,
                                      Role = string.Join(",", p.Role)
                                  });

            var userToEdit = usersWithRoles.FirstOrDefault(u => u.UserID == UserID);
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");

            return View(userToEdit);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UsersVM model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationDbContext();
                var user = await UserManager.FindByIdAsync(model.UserID);
                if (user != null)
                {
                    var deleteResult = await UserManager.DeleteAsync(user);
                    if (deleteResult.Succeeded)
                    {
                        // readd with updates
                        var updatedUser = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };

                        var updateResult = await UserManager.CreateAsync(user, model.Password);

                        if (updateResult.Succeeded)
                        {
                            await this.UserManager.AddToRoleAsync(user.Id, model.Role);
                            return RedirectToAction("Users");
                        }
                        else
                        {
                            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
                            AddErrors(updateResult);
                        }
                    }
                    else
                    {
                        ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
                        AddErrors(deleteResult);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // ----------------------------------------------------------------------------------------------- IDENTITY PIECES
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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