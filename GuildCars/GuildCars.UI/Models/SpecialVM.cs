using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SpecialVM
    {
        public List<Special> AdminSpecials { get; set; }
        public int SpecialID { get; set; }
        public string SpecialTitle { get; set; }
        public string SpecialDescription { get; set; }

    }
}