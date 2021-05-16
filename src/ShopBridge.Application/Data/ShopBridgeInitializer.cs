using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopBridge.Application.Data
{
    internal class ShopBridgeInitializer : DropCreateDatabaseIfModelChanges<ShopBridgeContext>
    {
        protected override void Seed(ShopBridgeContext dbContext)
        {
            if (dbContext.Inventory.Any())
                return;

            List<Inventory> inventories = new List<Inventory>()
            {
                new Inventory()
                {
                    Name = "Simple Inventory Item",
                    Description = "This is a simple inventory item added as seed data", 
                    CreateDateTime = DateTime.UtcNow
                },
                new Inventory()
                {
                    Name = "Simple Inventory Item2",
                    Description = "This is a simple inventory item added as seed data",
                    CreateDateTime = DateTime.UtcNow
                },
                new Inventory()
                {
                    Name = "Simple Inventory Item3",
                    Description = "This is a simple inventory item added as seed data",
                    CreateDateTime = DateTime.UtcNow
                }
            };

        }
    }
}
