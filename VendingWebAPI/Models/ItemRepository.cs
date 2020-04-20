using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendingWebAPI.Models.EF;

namespace VendingWebAPI.Models
{
    public class ItemRepository : IItemRepository
    {
        VendingItemsEntities _items = new VendingItemsEntities();

        public  List<Item> GetAll()
        {
            return _items.Items.ToList();
        }

        public Item Get(int itemId)
        {
            return _items.Items.ToList().FirstOrDefault(m => m.id == itemId);
        }

        public void Add(Item item)
        {
            item.id = _items.Items.ToList().Max(m => m.id) + 1;
            _items.Items.ToList().Add(item);
        }

        public void Update(Item item)
        {
            var found = _items.Items.ToList().FirstOrDefault(m => m.id == item.id);

            if (found != null)
                found = item;
        }

        public void Delete(int itemId)
        {
            _items.Items.ToList().RemoveAll(m => m.id == itemId);
        }

    }
}