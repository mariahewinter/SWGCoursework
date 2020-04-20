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
    public class TaxRepository : ITaxRepository
    {
        private string _filePath = @"C:\Repos\net-mpls-0120-classwork-mariahewinter\FlooringOSMastery\Taxes.txt";

        private Tax MapLineToTaxList(string line)
        {
            string[] columns = line.Split(',');
            Tax tax = new Tax();
            tax.StateAbbreviation = columns[0];
            tax.StateName = columns[1];
            tax.TaxRate = Convert.ToDecimal(columns[2]);

            return tax;
        }

        public List<Tax> LoadAllTax()
        {
            List<Tax> taxList = new List<Tax>();
            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                reader.ReadLine(); // skip header when reading in
                while ((line = reader.ReadLine()) != null)
                {
                    taxList.Add(MapLineToTaxList(line));
                }
                return taxList;
            }
        }

    }
}
