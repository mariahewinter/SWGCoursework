using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IMakeRepository
    {
        List<Make> GetMakes();
        Make AddMake(Make make);

    }
}
