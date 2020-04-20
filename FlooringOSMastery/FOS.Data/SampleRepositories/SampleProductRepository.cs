using FOS.Models;
using FOS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Data.SampleRepositories
{
    public class SampleProductRepository : IProductRepository
    {
        List<Product> _sampleProducts = new List<Product>();

        public SampleProductRepository()
        {
            _sampleProducts.Add(new Product() { ProductType = "Tile", CostPerSquareFoot = 3.50M, LaborCostPerSquareFoot = 4.15M });
        }

        public List<Product> LoadAllProducts()
        {
            return _sampleProducts;
        }

    }
}
