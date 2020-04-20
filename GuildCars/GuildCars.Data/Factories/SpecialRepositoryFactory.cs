using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.QA;
using GuildCars.Data.Repositories.PROD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factories
{
    public class SpecialRepositoryFactory
    {
        public static ISpecialRepository GetSpecialRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new QASpecialRepository();
                case "PROD":
                    return new SpecialRepositoryADO();
                default:
                    throw new Exception("Couldn't find valid Mode configuration value.");
            }
        }
    }
}
