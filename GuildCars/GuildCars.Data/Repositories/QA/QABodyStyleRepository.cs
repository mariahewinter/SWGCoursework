using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QABodyStyleRepository : IBodyStyleRepository
    {
        public static List<BodyStyle> _bodyStyles = new List<BodyStyle>()
        {
            new BodyStyle() {BodyStyleID=1, BodyStyleName="Car"},
            new BodyStyle() {BodyStyleID=2, BodyStyleName="SUV"},
            new BodyStyle() {BodyStyleID=3, BodyStyleName="Truck"},
            new BodyStyle() {BodyStyleID=4, BodyStyleName="Van"}
        };

        public List<BodyStyle> GetBodyStyles()
        {
            return _bodyStyles;
        }
    }
}
