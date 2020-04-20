using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QAPurchaseTypeRepository : IPurchaseTypeRepository
    {
        public static List<PurchaseType> _purchaseTypes = new List<PurchaseType>()
        {
            new PurchaseType() {PurchaseTypeID=1, PurchaseTypeName="Bank Finance"},
            new PurchaseType() {PurchaseTypeID=2, PurchaseTypeName="Cash"},
            new PurchaseType() {PurchaseTypeID=3, PurchaseTypeName="Dealer Finance"},
        };

        public List<PurchaseType> GetPurchaseTypes()
        {
            return _purchaseTypes;
        }
    }
}
