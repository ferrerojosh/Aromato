using System;

namespace Aromato.Domain.InventoryAgg
{
    public class Item : IEntity<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Item()
        {
        }

        public static Item Create(string name, string description)
        {
            return new Item
            {
                Name = name,
                Description = description
            };
        }

        public long Id { get; set; }
        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
    }
}