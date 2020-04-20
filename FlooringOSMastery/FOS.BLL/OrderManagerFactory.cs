using FOS.Data;
using FOS.Data.ProductionRepositories;
using FOS.Data.SampleRepositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
             string mode = ConfigurationManager.AppSettings["Mode"].ToString();

             switch (mode)
             {
                 case "Test":
                    return new OrderManager(new SampleOrderRepository(), new SampleTaxRepository(), new SampleProductRepository());
                case "Prod":
                    return new OrderManager(new OrderRepository(), new TaxRepository(), new ProductRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");

             }

            throw new NotImplementedException();
        }

    }
}
