using ShopBridge.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Data
{
    internal class ShopBridgeContext : DbContext, IApplicationDbContext
    {
        public ShopBridgeContext() : base("DefaultConnection")
        {

        }

        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Inventory>().HasKey(t => t.Id);
            modelBuilder.Entity<Inventory>()
                .Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Inventory>().Property(x => x.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Inventory>().Property(x => x.Description).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
