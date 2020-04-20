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
    public class ModelRepositoryFactory
    {
        public static IModelRepository GetModelRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new QAModelRepository();
                case "PROD":
                    return new ModelRepositoryADO();
                default:
                    throw new Exception("Couldn't find valid Mode configuration value.");
            }
        }
    }
}
