using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddModel
    {
        public List<Make> AdminMakes { get; set; }
        public List<Model> AdminModels { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public int MakeID { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserID { get; set; }


    }
}