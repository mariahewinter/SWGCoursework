using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddMake
    {
        public List<Make> AdminMakes { get; set; }
        public int MakeID { get; set; }
        public string MakeName { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserID { get; set; }
    }
}