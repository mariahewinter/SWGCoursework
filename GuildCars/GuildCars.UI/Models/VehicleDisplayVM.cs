using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class VehicleDisplayVM
    {
        public Vehicle Vehicle { get; set; }

        public Model Model { get; set; }
        public Make Make { get; set; }
        public BodyStyle BodyStyle { get; set; }
        public Color ExteriorColor { get; set; }
        public Color InteriorColor { get; set; }
        public Transmission Transmission { get; set; }
    }
}