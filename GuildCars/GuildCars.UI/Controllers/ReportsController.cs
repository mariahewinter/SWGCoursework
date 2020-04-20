using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
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
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Inventory()
        {

            var bodystyles = BodyStyleRepositoryFactory.GetBodyStyleRepository().GetBodyStyles();
            var colors = ColorRepositoryFactory.GetColorRepository().GetColors();
            var makes = MakeRepositoryFactory.GetMakeRepository().GetMakes();
            var models = ModelRepositoryFactory.GetModelRepository().GetModels();
            var transmission = TransmissionTypeRepositoryFactory.GetTransmissionTypeRepository().GetTransmissions();
            var vehicles = VehicleRepositoryFactory.GetVehicleRepository().GetVehicles();

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

            //vehiclesToReturn = vehiclesToReturn.OrderBy(vehicle => vehicle.Vehicle.Year).ThenBy(vehicle => vehicle.Make.MakeName).ThenBy(vehicle => vehicle.Model.ModelName).ToList();

            var usedVehicles = vehiclesToReturn.Where(v => v.Vehicle.Mileage > 1000).ToList();
            var newVehicles = vehiclesToReturn.Where(v => v.Vehicle.Mileage <= 1000).ToList();


            List<InventoryReportVehicleGroup> usedVehicleGroupList = VehicleList(usedVehicles);
            List<InventoryReportVehicleGroup> newVehicleGroupList = VehicleList(newVehicles);

            InventoryReportViewModel viewModel = new InventoryReportViewModel()
            {
                NewVehicles = newVehicleGroupList,
                UsedVehicles = usedVehicleGroupList
            };

            return View(viewModel);


        }
        /// <summary>
        /// Group the vehicles by year, make, model -- then get the count of each group and sum the MSRP, and save it into an InventoryReportVehicleGroup, basically what will be a single row on the InventoryReport. 
        /// </summary>
        private static List<InventoryReportVehicleGroup> VehicleList(List<VehicleDisplayVM> vehicles)
        {
            var vehicleGroupList = new List<InventoryReportVehicleGroup>();

            var vehiclesGroupedByYear = vehicles.GroupBy(v => v.Vehicle.Year);
            foreach (var yearGroup in vehiclesGroupedByYear)
            {
                // grouped by year
                var yearMakeGroups = yearGroup.ToList().GroupBy(v => v.Make.MakeName);

                foreach (var yearMakeGroup in yearMakeGroups)
                {
                    // grouped by make 
                    var yearMakeModelGroups = yearMakeGroup.ToList().GroupBy(v => v.Model.ModelName);

                    // grouped by model
                    foreach (var yearMakeModelGroup in yearMakeModelGroups)
                    {
                        var vehicleCount = yearMakeModelGroup.Count();

                        decimal stockValue = 0;
                        foreach (var vehicle in yearMakeModelGroup)
                        {
                            stockValue += vehicle.Vehicle.MSRP;
                        }

                        var vehicleGroup = new InventoryReportVehicleGroup()
                        {
                            Year = yearGroup.Key,
                            Make = yearMakeGroup.Key,
                            Model = yearMakeModelGroup.Key,
                            Count = vehicleCount,
                            StockValue = stockValue
                        };

                        vehicleGroupList.Add(vehicleGroup);
                    }
                }
            }

            return vehicleGroupList;
        }

        [HttpGet]
        public ActionResult Sales()
        {

            var context = new ApplicationDbContext();
            var allSales = SaleRepositoryFactory.GetSaleRepository().GetSales();
            var saleReport = new SaleReportAll();

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
                                  }).ToList();


            var salesGroupedByUser = allSales.GroupBy(s => s.UserID);

            var listOfSaleReportsByUser = new List<SaleReportByUser>();

            foreach (var saleGroup in salesGroupedByUser)
            {
                var user = UserManager.FindById(saleGroup.Key).FirstName + " " + UserManager.FindById(saleGroup.Key).LastName;


                int totalVehicles = 0;
                decimal totalSales = 0;
                foreach (var sale in saleGroup)
                {
                    totalSales += sale.PurchasePrice;
                    totalVehicles++;
                }

                var row = new SaleReportByUser() { User = user, TotalSales = totalSales, TotalVehicles = totalVehicles };

                listOfSaleReportsByUser.Add(row);
            }


            saleReport.SalesByUser = listOfSaleReportsByUser;
            saleReport.UsersForDropDown = usersWithRoles;


            return View(saleReport);

        }

        [HttpPost]
        public ActionResult Sales(string userID, DateTime? fromDate, DateTime? toDate)
        {
            if(fromDate == null)
            {
                fromDate = new DateTime(2000, 01, 01);
            }

            if(toDate == null)
            {
                toDate = new DateTime(2021, 01, 01);
            }

            var context = new ApplicationDbContext();
            var allSales = SaleRepositoryFactory.GetSaleRepository().GetSales();
            var saleReport = new SaleReportAll();

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
                                  }).ToList();



            if (userID != "All")
            {
                var salesByUser = allSales.Where(s => s.UserID == userID).ToList();
                var listOfSaleReports = new List<SaleReportByUser>();


                int totalVehicles = 0;
                foreach (var sale in salesByUser)
                {
                    var user = UserManager.FindById(userID).FirstName + " " + UserManager.FindById(userID).LastName;
                    decimal totalSales = 0;
                    if (sale.PurchaseDate < toDate && sale.PurchaseDate > fromDate)
                    {
                        totalSales += sale.PurchasePrice;
                        totalVehicles++;
                    }

                    var saleReportForUser = new SaleReportByUser() { User = user, TotalSales = totalSales, TotalVehicles = totalVehicles };

                    listOfSaleReports.Add(saleReportForUser);
                }

                saleReport.SalesByUser = listOfSaleReports;
                saleReport.UsersForDropDown = usersWithRoles;
                saleReport.UserID = "All";
                saleReport.fromDate = DateTime.MinValue;
                saleReport.toDate = DateTime.MaxValue;
            }
            else
            {
                var salesGroupedByUser = allSales.GroupBy(s => s.UserID);

                var listOfSaleReportsByUser = new List<SaleReportByUser>();

                foreach (var saleGroup in salesGroupedByUser)
                {
                    var user = UserManager.FindById(saleGroup.Key).FirstName + " " + UserManager.FindById(saleGroup.Key).LastName;


                    int totalVehicles = 0;
                    decimal totalSales = 0;
                    foreach (var sale in saleGroup)
                    {
                        if (sale.PurchaseDate < toDate && sale.PurchaseDate > fromDate)
                        {
                            totalSales += sale.PurchasePrice;
                            totalVehicles++;
                        }
                    }

                    var row = new SaleReportByUser() { User = user, TotalSales = totalSales, TotalVehicles = totalVehicles };

                    listOfSaleReportsByUser.Add(row);
                }


                saleReport.SalesByUser = listOfSaleReportsByUser;
                saleReport.UsersForDropDown = usersWithRoles;
                saleReport.UserID = "All";
                saleReport.fromDate = DateTime.MinValue;
                saleReport.toDate = DateTime.MaxValue;

                return View(saleReport);
            }

            return View(saleReport);
        }

        //[HttpPost]
        //public ActionResult Sales(string userID, DateTime fromDate, DateTime toDate)
        //{
        //    List<Sale> sales = SaleRepositoryFactory.GetSaleRepository().GetSales();
        //    List<Sale> salesByUser = new List<Sale>();

        //    if (userID == "All" && fromDate == DateTime.MinValue && toDate == DateTime.MaxValue)
        //    {
        //        return RedirectToAction("Sales");
        //    }
        //    else if (userID != "All")
        //    {
        //        salesByUser = sales.Where(s => s.UserID == userID).ToList();
        //    }

        //    salesByUser.RemoveAll(v => v.PurchaseDate < fromDate);
        //    salesByUser.RemoveAll(s => s.PurchaseDate > toDate);

        //    var user = UserManager.FindById(userID);
        //    decimal totalSales = 0;
        //    int totalVehicles = 0;
        //    var saleGroup = new SaleReportByUser()
        //    {
        //        User = user.FirstName + user.LastName,
        //        TotalSales = totalSales,
        //        TotalVehicles = totalVehicles
        //    };

        //    foreach (var sale in salesByUser)
        //    {
        //        totalSales += sale.PurchasePrice;
        //        totalVehicles++;
        //    }

        //    return View(saleGroup);
        //}

        //-----------------------------------------------------------------------------------------------------------------------------------
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ReportsController()
        {
        }

        public ReportsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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