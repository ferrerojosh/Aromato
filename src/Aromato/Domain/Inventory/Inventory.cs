using System.Collections;
using System.Collections.Generic;

namespace Aromato.Domain.Inventory
{
    public class Inventory : IAggregateRoot<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Inventory()
        {
        }

        public static Inventory Create(string name, string description)
        {
            return new Inventory
            {
                Name = name,
                Description = description,
            };
        }

        public long Id { get; set; }

        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public virtual List<InventoryItem> Items { get; } = new List<InventoryItem>();
    }
}