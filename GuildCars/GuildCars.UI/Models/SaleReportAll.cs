using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SaleReportAll
    {
        public List<SaleReportByUser> SalesByUser { get; set; }
        public List<UsersVM> UsersForDropDown { get; set; }
        public string UserID { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}