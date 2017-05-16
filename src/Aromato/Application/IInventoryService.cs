using System.Collections.Generic;

namespace Aromato.Application
{
    public interface IInventoryService : IReadService<long>
    {
        IEnumerable<IData> RetrieveItemsByName(long inventoryId, string itemName);
        IData RetrieveItemByUniqueId(long inventoryId, string uniqueId);
        void CreateInventory(IData data);
        void AddItemToInventory(long inventoryId, IData data);
        void DeleteInventoryItem(long inventoryId, long itemId);
        void DeleteInventoryItem(long inventoryId, string uniqueId);
    }
}