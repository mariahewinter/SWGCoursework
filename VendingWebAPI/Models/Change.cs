using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingWebAPI.Models
{
    public class Change
    {
        public int quarters { get; set; }
        public int dimes { get; set; }
        public int nickels { get; set; }
        public int pennies { get; set; }
    }
}