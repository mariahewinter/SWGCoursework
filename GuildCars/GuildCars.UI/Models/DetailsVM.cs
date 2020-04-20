using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class DetailsVM
    {
        public List<BodyStyle> BodyStyles { get; set; }
        public List<Color> Colors { get; set; }
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<Transmission> Transmission { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}