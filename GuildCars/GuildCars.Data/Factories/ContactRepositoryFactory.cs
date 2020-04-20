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
    public class ContactRepositoryFactory
    {
        public static IContactRepository GetContactRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new QAContactRepository();
                case "PROD":
                    return new ContactRepositoryADO();
                default:
                    throw new Exception("Couldn't find valid Mode configuration value.");
            }
        }
    }
}
