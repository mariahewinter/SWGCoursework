using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VendingWebAPI.Models.EF
{
    public class VendingItemsEntities : DbContext
    {
        public VendingItemsEntities()
            : base("VendingItems")
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}