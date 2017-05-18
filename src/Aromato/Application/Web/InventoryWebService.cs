using System.Collections.Generic;
using System.Linq;
using Aromato.Application.Web.Data;
using Aromato.Domain.InventoryAgg;
using Aromato.Infrastructure.Crosscutting.Extension;

namespace Aromato.Application.Web
{
    public class InventoryWebService : IInventoryService
    {

        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryItemRepository _inventoryItemRepository;

        public InventoryWebService(IInventoryRepository inventoryRepository, IInventoryItemRepository inventoryItemRepository)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryItemRepository = inventoryItemRepository;
        }

        public IData RetrieveById(long id)
        {
            return _inventoryRepository.FindById(id).AsData<InventoryWebData>();
        }

        public IEnumerable<IData> RetrieveAll()
        {
            return _inventoryRepository.FindAll().AsEnumerableData<InventoryWebData>();
        }

        public IEnumerable<IData> RetrieveItemsByName(long inventoryId, string itemName)
        {
            return _inventoryRepository.FindById(inventoryId)
                .Items.Where(i => i.Item.Name == itemName)
                .AsEnumerableData<ItemWebData>();
        }

        public IData RetrieveItemByUniqueId(long inventoryId, string uniqueId)
        {
            return _inventoryRepository.FindById(inventoryId)
                .Items.First(i => i.UniqueId == uniqueId).AsData<ItemWebData>();
        }

        public void CreateInventory(IData data)
        {
            var inventoryData = data as InventoryWebData;
            var inventory = Inventory.Create(
                inventoryData?.Name,
                inventoryData?.Description
            );
            _inventoryRepository.Add(inventory);
            _inventoryRepository.UnitOfWork.Commit();
        }

        public void AddItemToInventory(long inventoryId, IData data)
        {
            var itemData = data as ItemWebData;
            var inventory = _inventoryRepository.FindById(inventoryId);
            var item = inventory.Items.FirstOrDefault(i => i.Item.Name == itemData?.Name)?.Item
                       ?? Item.Create(itemData?.Name, itemData?.Description);

            var inventoryItem = InventoryItem.Create(itemData?.UniqueId, item);
            inventory.Items.Add(inventoryItem);

            _inventoryRepository.UnitOfWork.Commit();
        }

        public void DeleteInventoryItem(long inventoryId, long itemId)
        {
            var inventory = _inventoryRepository.FindById(inventoryId);
            var inventoryItem = inventory.Items.FirstOrDefault(i => i.Id == itemId);
            _inventoryItemRepository.Remove(inventoryItem);
            _inventoryItemRepository.UnitOfWork.Commit();
        }

        public void DeleteInventoryItem(long inventoryId, string uniqueId)
        {
            var inventory = _inventoryRepository.FindById(inventoryId);
            var inventoryItem = inventory.Items.FirstOrDefault(i => i.UniqueId == uniqueId);
            _inventoryItemRepository.Remove(inventoryItem);
            _inventoryRepository.UnitOfWork.Commit();
        }
    }
}