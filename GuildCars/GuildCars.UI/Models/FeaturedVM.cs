using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class FeaturedVM
    {
        public List<Vehicle> FeaturedVehicles { get; set; }
        public List<Make> FeaturedMakes { get; set; }
        public List<Model> FeaturedModels { get; set; }
        public List<Special> Specials { get; set; }
    }
}