namespace VendingWebAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VendingWebAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VendingWebAPI.Models.EF.VendingItemsEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VendingWebAPI.Models.EF.VendingItemsEntities context)
        {
            context.Items.AddOrUpdate(

            new Item { id = 1, name = "Stuffed Bat", price = 0.99M, quantity = 5 },
            new Item { id = 2, name = "Spider Plushie", price = 1.25M, quantity = 3 },
            new Item { id = 3, name = "Creepy Doll", price = 5.25M, quantity = 1 }
            ); 
        }
    }
}
