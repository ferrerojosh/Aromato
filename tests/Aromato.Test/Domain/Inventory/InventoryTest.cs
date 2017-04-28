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
            return new Aromato.Domain.Inventory.Inventory("Sample Inventory", "This is a sample inventory.");
        }

        [Fact]
        public void CanAddItemToInventory()
        {
            var inventory = TestInventory();
            var item = new Item("TESTITEM-0XD");

            inventory.AddItemToInventory(item);

            Assert.NotNull(inventory.Items.First(i => i == item));
        }

        public void CanRemoveItemFromInventory()
        {
            var inventory = TestInventory();
            var item = new Item("TESTITEM-REMOVAL");

            inventory.AddItemToInventory(item);
            Assert.NotNull(inventory.Items.First(i => i == item));
            inventory.RemoveItemFromInventory(item);
            Assert.Throws<InvalidOperationException>(() => inventory.Items.First(i => i == item));
        }
    }
}