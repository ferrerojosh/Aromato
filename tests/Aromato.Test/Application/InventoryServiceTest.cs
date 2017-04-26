using System.Linq;
using Aromato.Application.Service;
using Aromato.Domain.Entity;
using Aromato.Infrastructure;
using Aromato.Infrastructure.Repository;
using Aromato.Test.Order;
using Xunit;

namespace Aromato.Test.Application
{
    [TestCaseOrderer("Aromato.Test.Order.PriorityOrderer", "Aromato.Application.Test")]
    public class InventoryServiceTest
    {
        [Fact, TestPriority(0)]
        public void CanCreateInventory()
        {
            var inventoryName = "Instruments";

            using (var unitOfWork = new EfUnitOfWork())
            {
                var inventoryRepository = new EfInventoryRepository(unitOfWork);
                var inventoryService = new InventoryService(inventoryRepository, unitOfWork);

                inventoryService.CreateInventory(inventoryName);

                var inventories = inventoryRepository.FindAll();

                Assert.Equal(true, inventories.Any(inventory => inventory.Name == inventoryName));
            }

        }

        [Fact, TestPriority(1)]
        public void CanAddItemToInventory()
        {
            var inventoryName = "Instruments";
            var itemUniqueId = "DML-A01";
            var itemName = "Trainer";
            var itemDescription = "Hardware stuff";

            using (var unitOfWork = new EfUnitOfWork())
            {
                var inventoryRepository = new EfInventoryRepository(unitOfWork);
                var inventoryService = new InventoryService(inventoryRepository, unitOfWork);

                var item = new Item(itemUniqueId, itemName, itemDescription);

                inventoryService.AddItemToInventory(inventoryName, item);

                var inventory = inventoryRepository.FindByName(inventoryName);

                var retrievedItem = inventory.Items.First(i => i.UniqueId == itemUniqueId);

                Assert.Equal(itemUniqueId, retrievedItem.UniqueId);
                Assert.Equal(itemName, retrievedItem.Name);
                Assert.Equal(itemDescription, retrievedItem.Description);

                Assert.Equal(inventoryName, inventory.Name);
            }
        }
    }
}