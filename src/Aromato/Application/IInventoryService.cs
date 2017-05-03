using System.Collections.Generic;

namespace Aromato.Application
{
    public interface IInventoryService : IReadService<long>
    {
        IEnumerable<IData> RetrieveItemsByName(long inventoryId, string itemName);
        IData RetrieveItemByUniqueId(long inventoryId, string uniqueId);
        void CreateInventory(IData inventoryData);
        void AddItemToInventory(long inventoryId, IData itemData);
    }
}