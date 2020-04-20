using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QAMakeRepository : IMakeRepository
    {
        public static List<Make> _makes = new List<Make>()
        {
            new Make() {MakeID=1, MakeName="Toyota", DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com" },
            new Make() {MakeID=2, MakeName="Nissan", DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"},
            new Make() {MakeID=3, MakeName="Honda", DateAdded= new DateTime(2020, 04, 10), UserID="test@test.com"}
        };

        public List<Make> GetMakes()
        {
            return _makes;
        }

        public Make AddMake(Make make)
        {
            if(make.MakeID == 0)
            {
                make.MakeID = _makes.Max(m => m.MakeID) + 1;
            }
            else if (_makes.Contains(make))
            {
                _makes.RemoveAll(m => m.MakeID == make.MakeID);
            }

            _makes.Add(make);
            return make;
        }
    }
}
