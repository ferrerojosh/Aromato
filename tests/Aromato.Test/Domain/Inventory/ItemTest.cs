using Aromato.Domain.Inventory;
using Xunit;

namespace Aromato.Test.Domain.Inventory
{
    public class ItemTest
    {

        [Fact]
        public void UniqueIdEqualityTest()
        {
            var item = new Item("Sample-UNIQUEID");
            var item2 = new Item("Sample-UNIQUEID");

            Assert.True(item == item2);
            Assert.True(item.Equals(item2));
        }

    }
}