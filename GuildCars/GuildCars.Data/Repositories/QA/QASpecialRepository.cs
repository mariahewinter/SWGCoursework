using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class QASpecialRepository : ISpecialRepository
    {
        public static List<Special> _specials = new List<Special>()
        {
            new Special() {SpecialID=1, SpecialTitle="Cupcake", SpecialDescription="Cupcake cotton candy apple pie bear claw icing sesame snaps bonbon cookie. Apple pie donut sugar plum tiramisu soufflé croissant bear claw. Marzipan gummies gummies tart gummi bears cheesecake." },
            new Special() {SpecialID=2, SpecialTitle="Pancake", SpecialDescription="Pancake halvah lollipop sesame snaps pastry sweet jelly beans lollipop danish cotton candy. Powder cupcake toffee brownie marzipan cheesecake carrot cake sweet oat cake. Pudding caramels gummi bears oat cake soufflé gingerbread powder. Sweet roll pastry liquorice cookie muffin candy cotton candy."},
            new Special() {SpecialID=3, SpecialTitle="Fruitcake", SpecialDescription="Fruit cake apple pie chupa chups lollipop cookie jelly beans liquorice dragée candy pudding. Oat cake marzipan pudding fruitcake. Wafer gummies candy tootsie roll bonbon caramels marshmallow. Danish pastry pudding powder marshmallow jelly beans lemon drops dessert muffin."},
            new Special() {SpecialID=4, SpecialTitle="Teacake", SpecialDescription="Teacake candy croissant jelly-o powder cheesecake pastry chocolate cake. Chocolate cake sweet donut tart liquorice dragée gingerbread sweet. Bonbon carrot cake liquorice bear claw cotton candy sweet roll pudding. Muffin cupcake tiramisu topping jelly jelly-o cake cookie ice cream."},
            new Special() {SpecialID=5, SpecialTitle="Beefcake", SpecialDescription="Beefcake Cake topping cupcake pastry halvah danish biscuit. Tart icing caramels danish caramels tiramisu macaroon tart. Chocolate cake gingerbread pie bear claw liquorice fruitcake jelly beans. Pudding tiramisu icing lollipop marzipan marshmallow apple pie."}
        };

        public Special Add(Special special)
        {
            if (special.SpecialID == 0)
            {
                special.SpecialID = _specials.Max(s => s.SpecialID) + 1;
            }

            if (!string.IsNullOrEmpty(special.SpecialTitle) && !string.IsNullOrEmpty(special.SpecialDescription))
            {
                _specials.Add(special);
                return special;
            }
            else
            {
                return null;
            }

        }

        public void Delete(Special special)
        {
            _specials.RemoveAll(s => s.SpecialID == special.SpecialID);
        }

        public List<Special> GetSpecials()
        {
            return _specials;
        }
    }
}
