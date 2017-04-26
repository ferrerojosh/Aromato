using System;
using System.Linq;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Entity;
using Xunit;

namespace Aromato.Test.Domain.Aggregate
{

    public class InventoryTest
    {
        private const string InventoryName = "MyInventory";
        private const string UniqueId = "SampleItem-01";
        private const string Name = "Sample";
        private const string Description = "This is a sample item";

        [Fact]
        public void CanAddItemToInventory()
        {
            var inventory = new Inventory(InventoryName);
            inventory.AddItem(new Item(UniqueId, Name, Description));

            Assert.NotNull(inventory.Items.Last(x => x.UniqueId == UniqueId));
        }

        [Fact]
        public void CanRemoveItemFromInventory()
        {
            var inventory = new Inventory(InventoryName);

            inventory.AddItem(new Item(UniqueId, Name, Description));

            var item = inventory.Items.Last(x => x.UniqueId == UniqueId);

            inventory.RemoveItem(item);
            Assert.Throws<InvalidOperationException>(() => inventory.Items.Last(x => x.UniqueId == UniqueId));
        }
    }
}