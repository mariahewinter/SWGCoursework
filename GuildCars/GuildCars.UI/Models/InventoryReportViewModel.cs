using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class InventoryReportViewModel
    {
        public List<InventoryReportVehicleGroup> NewVehicles { get; set; }
        public List<InventoryReportVehicleGroup> UsedVehicles { get; set; }
    }
}