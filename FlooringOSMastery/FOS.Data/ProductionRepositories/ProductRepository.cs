using FOS.Models;
using FOS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Data.ProductionRepositories
{
    public class ProductRepository : IProductRepository
    {
        private string _filePath = @"C:\Repos\net-mpls-0120-classwork-mariahewinter\FlooringOSMastery\Products.txt";

        private Product MapLineToProductList(string line)
        {
            string[] columns = line.Split(',');
            Product product = new Product();
            product.ProductType = columns[0];
            product.CostPerSquareFoot = Convert.ToDecimal(columns[1]);
            product.LaborCostPerSquareFoot = Convert.ToDecimal(columns[2]);

            return product;
        }

        public List<Product> LoadAllProducts()
        {
            List<Product> productList = new List<Product>();
            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                reader.ReadLine(); // skip header when reading in
                while ((line = reader.ReadLine()) != null)
                {
                    productList.Add(MapLineToProductList(line));
                }
                return productList;
            }
        }


    }
}
