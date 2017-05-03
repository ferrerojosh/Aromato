using Aromato.Domain.Inventory;
using Xunit;

namespace Aromato.Test.Domain.Inventory
{
    public class ItemTest
    {

        [Fact]
        public void UniqueIdEqualityTest()
        {
            var item = Item.Create("Sample-UNIQUEID", "MyItem", "Desc");
            var item2 = Item.Create("Sample-UNIQUEID", "MyItem", "Desc");

            Assert.True(item == item2);
            Assert.True(item.Equals(item2));
        }

    }
}