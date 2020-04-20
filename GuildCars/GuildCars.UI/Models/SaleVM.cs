using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SaleVM
    {
        public Sale Sale { get; set; }
        public DetailsVM Details { get; set; }
    }
}