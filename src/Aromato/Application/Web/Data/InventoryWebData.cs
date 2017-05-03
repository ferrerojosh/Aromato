using System.Collections.Generic;

namespace Aromato.Application.Web.Data
{
    public class InventoryWebData : IData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ItemWebData> Items { get; set; }
    }
}
