namespace ShopBridge.Application.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShopBridge.Application.Data.ShopBridgeContext>
    {
        public System.Data.Entity.SqlServer.SqlProviderServices providerServices;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShopBridge.Application.Data.ShopBridgeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (context.Inventory.Any())
            {
                return;
            }

            context.Inventory.Add(new Data.Inventory()
            {
                Name = "Sample Inventory Item1",
                Description = "This is a sample inventory item added as seed data",
                CreateDateTime = DateTime.UtcNow
            });
            context.Inventory.Add(new Data.Inventory()
            {
                Name = "Sample Inventory Item2",
                Description = "This is a sample inventory item added as seed data",
                CreateDateTime = DateTime.UtcNow
            });
            context.Inventory.Add(new Data.Inventory()
            {
                Name = "Sample Inventory Item3",
                Description = "This is a sample inventory item added as seed data",
                CreateDateTime = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }
}
