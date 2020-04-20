using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SearchVM
    {
        public List<BodyStyle> SearchBodyStyles { get; set; }
        public List<Color> SearchColors { get; set; }
        public List<Make> SearchMakes { get; set; }
        public List<Model> SearchModels { get; set; }
        public List<Transmission> SearchTransmission { get; set; }
        public List<Vehicle> SearchVehicles { get; set; }

    }
}