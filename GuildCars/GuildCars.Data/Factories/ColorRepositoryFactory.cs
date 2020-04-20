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
    public class ColorRepositoryFactory
    {
        public static IColorRepository GetColorRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new QAColorRepository();
                case "PROD":
                    return new ColorRepositoryADO();
                default:
                    throw new Exception("Couldn't find valid Mode configuration value.");
            }
        }
    }
}
