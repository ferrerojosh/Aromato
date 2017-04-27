using System;
using System.Collections.Generic;
using Aromato.Domain.Entity;

namespace Aromato.Domain.Aggregate
{
    public class Inventory : IAggregateRoot
    {
        protected Inventory()
        {
            // Required for Entity Framework
        }

        public Inventory(string name)
        {
            if (name.Length <= 0)
            {
                throw new ArgumentException("the inventory must have a name!");
            }

            Name = name;
            LastUpdated = DateTime.Now;
            Items = new List<Item>();
        }

        public long Id { get; set; }

        public string Name { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public IList<Item> Items { get; protected set; }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }
}