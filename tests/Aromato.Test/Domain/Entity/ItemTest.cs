using Aromato.Domain.Entity;
using Aromato.Domain.Enumeration;
using Xunit;

namespace Aromato.Test.Domain.Entity
{
    public class ItemTest
    {

        [Fact]
        public void CanChangeStatus()
        {
            var uniqueId = "SAMPLE-ITEMXD";
            var name = "Sample Item";
            var description = "Sample description";

            var item = new Item(uniqueId, name, description);

            Assert.Equal(ItemStatus.Available, item.Status);

            item.ChangeStatus(ItemStatus.Missing);

            Assert.Equal(ItemStatus.Missing, item.Status);
        }

    }
}