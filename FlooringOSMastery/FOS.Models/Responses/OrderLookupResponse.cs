using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models
{
    public class OrderLookupResponse : Response
    {
        public Order Order { get; set; }
    }
}
