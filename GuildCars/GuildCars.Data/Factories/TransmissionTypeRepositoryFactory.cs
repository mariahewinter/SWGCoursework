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
    public class TransmissionTypeRepositoryFactory
    {
        public static ITransmissionTypeRepository GetTransmissionTypeRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new QATransmissionTypeRepository();
                case "PROD":
                    return new TransmissionTypeRepositoryADO();
                default:
                    throw new Exception("Couldn't find valid Mode configuration value.");
            }
        }
    }
}
