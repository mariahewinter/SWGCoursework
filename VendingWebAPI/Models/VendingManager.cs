using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingWebAPI.Models
{
    public class VendingManager
    {
        private IItemRepository _itemRepository;

        public VendingManager(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

    }
}