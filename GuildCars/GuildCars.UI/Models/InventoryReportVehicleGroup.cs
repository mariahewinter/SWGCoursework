using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class InventoryReportVehicleGroup
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal StockValue { get; set; }
        public int Count { get; set; }
    }
}