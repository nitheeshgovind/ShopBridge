using ShopBridge.Application.Abstractions;
using ShopBridge.Application.Common.Exceptions;
using ShopBridge.Application.Data;
using ShopBridge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Repository
{
    internal class InventoryRepository : IInventoryRepository
    {
        private readonly IApplicationDbContext dbContext;

        public InventoryRepository(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<InventoryModel>> GetInventoryAsync()
        {
            var inventory = await dbContext.Inventory.AsNoTracking().ToListAsync();
            return (from i in inventory
                    select new InventoryModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        Name = i.Name,
                        Price = i.Price,
                        QuantityInStock = i.QuantityInStock
                    }).ToList();
        }

        public async Task<InventoryModel> GetInventoryAsync(int id)
        {
            var inventory = await dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == id);

            if (inventory == null)
                throw new NotFoundException(nameof(inventory), id);

            return new InventoryModel
            {
                Id = inventory.Id,
                Description = inventory.Description,
                Name = inventory.Name,
                Price = inventory.Price,
                QuantityInStock = inventory.QuantityInStock
            };
        }

        public async Task<int> AddInventoryAsync(InventoryModel inventory)
        {
            var _inventory = new Inventory()
            {
                Description = inventory.Description,
                Name = inventory.Name,
                Price = inventory.Price,
                QuantityInStock = inventory.QuantityInStock,
                CreateDateTime = DateTime.UtcNow
            };

            dbContext.Inventory.Add(_inventory);

            await dbContext.SaveChangesAsync();

            return _inventory.Id;
        }

        public async Task<bool> UpdateInventoryAsync(InventoryModel inventory)
        {

            var _inventory = await dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == inventory.Id);

            if (inventory == null)
                throw new NotFoundException(nameof(inventory), inventory.Id);

            _inventory.Description = inventory.Description;
            _inventory.Name = inventory.Name;
            _inventory.Price = inventory.Price;
            _inventory.QuantityInStock = inventory.QuantityInStock;

            dbContext.Inventory.Add(_inventory);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            var inventory = await dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == id);

            if (inventory == null)
                throw new NotFoundException(nameof(Inventory), id);

            dbContext.Inventory.Remove(inventory);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
