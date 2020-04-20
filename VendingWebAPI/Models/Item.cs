using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingWebAPI.Models.EF;

namespace VendingWebAPI.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}