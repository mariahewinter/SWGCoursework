using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingWebAPI.Models
{
    public interface IItemRepository
    {
        List<Item> GetAll();
        Item Get(int itemId);
        void Add(Item item);
        void Update(Item item);
        void Delete(int itemId);

    }
}

