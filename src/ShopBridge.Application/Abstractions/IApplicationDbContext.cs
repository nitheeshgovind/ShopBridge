using ShopBridge.Application.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Abstractions
{
    internal interface IApplicationDbContext
    {

        public DbSet<Inventory> Inventory { get; set; }

        Task<int> SaveChangesAsync();
    }
}
