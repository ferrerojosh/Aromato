using Aromato.Domain;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Entity;
using Aromato.Domain.Repository;

namespace Aromato.Application.Service
{
    public class InventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateInventory(string name)
        {
            var inventory = new Inventory(name);
            _inventoryRepository.Add(inventory);
            _unitOfWork.Commit();
        }

        public void AddItemToInventory(string inventoryName, Item item)
        {
            var inventory = _inventoryRepository.FindByName(inventoryName);
            inventory.AddItem(item);
            _unitOfWork.Commit();
        }
    }
}