using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IModelRepository
    {
        List<Model> GetModels();
        List<Model> GetModelsByMakeID(int makeID);
        void AddModel(Model model);

    }
}
