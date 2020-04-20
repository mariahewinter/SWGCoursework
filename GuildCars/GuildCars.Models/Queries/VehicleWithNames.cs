using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleWithNames
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
