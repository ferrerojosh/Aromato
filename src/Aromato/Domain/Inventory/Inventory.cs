using System.Collections.Generic;
using System.Linq;

namespace Aromato.Domain.Inventory
{
    public class Inventory : IAggregateRoot<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Inventory()
        {
            Items = new List<Item>();
        }

        public Inventory(string name, string description) : this()
        {
            Name = name;
            Description = description;
        }

        public long Id { get; set; }

        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public List<Item> Items { get; protected set; }

        public virtual void AddItemToInventory(Item item)
        {
            Items.Add(item);
        }

        public virtual void RemoveItemFromInventory(Item item)
        {
            Items.Remove(item);
        }

        public virtual int RetrieveItemQuantity(string itemName)
        {
            return Items.Count(item => item.Name == itemName);
        }

        public virtual IEnumerable<Item> RetrieveItemsWithStatus(ItemStatus status)
        {
            return Items.Where(item => item.Status == status).AsEnumerable();
        }
    }
}