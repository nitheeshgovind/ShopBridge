using ShopBridge.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Application.Abstractions
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryModel>> GetInventoryAsync();

        Task<InventoryModel> GetInventoryAsync(int id);

        Task<int> AddInventoryAsync(InventoryModel inventory);

        Task<bool> UpdateInventoryAsync(InventoryModel inventory);

        Task<bool> DeleteInventoryAsync(int id);
    }
}
