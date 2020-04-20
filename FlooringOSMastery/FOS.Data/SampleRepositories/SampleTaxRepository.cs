using FOS.Models;
using FOS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Data.SampleRepositories
{
    public class SampleTaxRepository : ITaxRepository
    {
        List<Tax> _sampleTax = new List<Tax>();

        public SampleTaxRepository()
        {
            _sampleTax.Add(new Tax() { StateAbbreviation = "OH", StateName = "Ohio", TaxRate = 6.25M });
        }

        public List<Tax> LoadAllTax()
        {
            return _sampleTax;
        }

        public Tax LoadTaxInfo(string stateAbbreviation)
        {
            List<Tax> allTax = _sampleTax.Where(state => state.StateAbbreviation == stateAbbreviation.ToUpper()).ToList();
            Tax requestedTax = allTax[0];
            return requestedTax;
        }


    }
}
