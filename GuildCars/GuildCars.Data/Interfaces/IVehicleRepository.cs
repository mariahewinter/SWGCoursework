using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetVehicles();
        Vehicle GetVehicleByVIN(string vinNumber);
        List<Vehicle> GetVehiclesBySearchParameters(string searchTerm, decimal priceMin, decimal priceMax, int yearMin, int yearMax, int mileage);
        List<Vehicle> GetFeaturedVehicles();
        Vehicle Add(Vehicle vehicle);
        Vehicle Edit(Vehicle vehicle);
        void Delete(string vinNumber);

    }
}
