using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingWebAPI.Models
{
    public class SampleRepository : IItemRepository
    {
        List<Item> _items = new List<Item>()
        {
        new Item
            { id = 1, name = "Stuffed Bat", price = 0.99M, quantity = 5 },
        new Item 
            { id = 2, name = "Spider Plushie", price = 1.25M, quantity = 3 },
        new Item 
            { id = 3, name = "Creepy Doll", price = 5.25M, quantity = 1 }
        };

        public List<Item> GetAll()
        {
            return _items;
        }

        public Item Get(int itemId)
        {
            return _items.FirstOrDefault(m => m.id == itemId);
        }

        public void Add(Item item)
        {
            item.id = _items.Max(m => m.id) + 1;
            _items.Add(item);
        }

        public void Update(Item item)
        {
            var found = _items.FirstOrDefault(m => m.id == item.id);

            if (found != null)
                found = item;
        }

        public void Delete(int itemId)
        {
            _items.RemoveAll(m => m.id == itemId);
        }
    }
}