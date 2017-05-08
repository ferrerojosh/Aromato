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
    }
}