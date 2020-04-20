using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Vehicle
    {
        public string VinNumber { get; set; }
        public int ModelID { get; set; }
        public int MakeID { get; set; }
        public int BodyStyleID { get; set; }
        public int TransmissionID { get; set; }
        public int ExteriorColor { get; set; }
        public int InteriorColor { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsPurchased { get; set; }
    }
}
