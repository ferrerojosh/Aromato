using System;
using System.Linq;
using Aromato.Domain.Inventory;
using Xunit;

namespace Aromato.Test.Domain.Inventory
{
    public class InventoryTest
    {

        public Aromato.Domain.Inventory.Inventory TestInventory()
        {
            return Aromato.Domain.Inventory.Inventory.Create("Sample Inventory", "This is a sample inventory.");
        }

        [Fact]
        public void CanAddItemToInventory()
        {
            var inventory = TestInventory();
            var item = Item.Create("TESTITEM-0XD", "Test item", "Test description");

            inventory.AddItemToInventory(item);

            Assert.NotNull(inventory.Items.First(i => i == item));
        }

        public void CanRemoveItemFromInventory()
        {
            var inventory = TestInventory();
            var item = Item.Create("TESTITEM-REMOVAL", "Test item2", "Test description2");

            inventory.AddItemToInventory(item);
            Assert.NotNull(inventory.Items.First(i => i == item));
            inventory.RemoveItemFromInventory(item);
            Assert.Throws<InvalidOperationException>(() => inventory.Items.First(i => i == item));
        }
    }
}