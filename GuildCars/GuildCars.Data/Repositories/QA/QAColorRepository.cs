using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QAColorRepository : IColorRepository
    {
        public static List<Color> _colors = new List<Color>()
        {
            new Color() {ColorID=1, ColorName="Red"},
            new Color() {ColorID=2, ColorName="Green"},
            new Color() {ColorID=3, ColorName="Blue"},
            new Color() {ColorID=4, ColorName="Black"},
            new Color() {ColorID=5, ColorName="White"}
        };

        public List<Color> GetColors()
        {
            return _colors;
        }
    }
}
