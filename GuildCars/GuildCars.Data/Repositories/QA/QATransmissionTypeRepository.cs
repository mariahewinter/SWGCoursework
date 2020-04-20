using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QATransmissionTypeRepository : ITransmissionTypeRepository
    {
        public static List<Transmission> _transmissions = new List<Transmission>()
        {
            new Transmission() {TransmissionID=1, TransmissionName="Automatic"},
            new Transmission() {TransmissionID=2, TransmissionName="Manual"}
        };

        public List<Transmission> GetTransmissions()
        {
            return _transmissions;
        }
    }
}
