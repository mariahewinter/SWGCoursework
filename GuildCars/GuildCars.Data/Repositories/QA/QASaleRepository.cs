using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QASaleRepository : ISaleRepository
    {
        public static List<Sale> _sales = new List<Sale>()
        {
            new Sale() { SaleID = 1, UserID = "62cb391a-507e-4793-8006-26336002d4e7", VinNumber = "TESTONEC8DH837877", PurchaseTypeID = 1, PurchaseDate = DateTime.Now, PurchasePrice = 9600.00m, Name = "Jon Winter", Email = "jwinter10581@hotmail.com", Phone = "507-527-2260", Address1 = "843 Devonshire Rd", City = "Richfield", State = "MN", Zipcode = "54321" },
            new Sale() { SaleID = 2, UserID = "00000000-0000-0000-0000-000000000000", VinNumber = "TESTTWOPGCM826138", PurchaseTypeID = 2, PurchaseDate = DateTime.Now, PurchasePrice = 10000.00m, Name = "Jon Winter", Email = "jwinter10581@hotmail.com", Phone = "507-527-2260", Address1 = "843 Devonshire Rd", City = "Richfield", State = "MN", Zipcode = "54321" }
        };

        public Sale Add(Sale sale)
        {
            var vehicle = VehicleRepositoryFactory.GetVehicleRepository().GetVehicleByVIN(sale.VinNumber);
            if ((sale.Phone == null && sale.Email == null) || sale.Zipcode.Length != 5 || sale.PurchasePrice > vehicle.MSRP || sale.PurchasePrice < ((vehicle.MSRP * 95)* .01m))
            {
                return null;
            }

            _sales.Add(sale);
            return sale;
        }

        public List<Sale> GetSales()
        {
            return _sales;
        }
    }
}
