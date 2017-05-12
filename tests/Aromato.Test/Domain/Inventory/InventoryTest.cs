using System;
using System.Linq;
using Aromato.Domain.InventoryAgg;
using Xunit;

namespace Aromato.Test.Domain.Inventory
{
    public class InventoryTest
    {

        public Aromato.Domain.InventoryAgg.Inventory TestInventory()
        {
            return Aromato.Domain.InventoryAgg.Inventory.Create("Sample Inventory", "This is a sample inventory.");
        }
    }
}