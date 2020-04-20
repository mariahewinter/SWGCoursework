using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.PROD;
using GuildCars.Data.Repositories.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factories
{
    public static class VehicleRepositoryFactory
    {
        public static IVehicleRepository GetVehicleRepository()
        {
            switch(Settings.GetRepositoryType())
            {
                case "QA":
                    return new QAVehicleRepository();
                case "PROD":
                    return new VehicleRepositoryADO();
                default:
                    throw new Exception("Couldn't find valid Mode configuration value.");
            }
        }
    }
}
