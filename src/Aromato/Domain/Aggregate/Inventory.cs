using System;
using System.Collections.Generic;
using Aromato.Domain.Entity;

namespace Aromato.Domain.Aggregate
{
    public class Inventory : IAggregateRoot
    {
        protected Inventory()
        {
            // required for Entity Framework
        }

        public Inventory(string name)
        {
            Name = name;
            LastUpdated = DateTime.Now;
            Items = new List<Item>();
        }

        public Guid Id { get; set; }

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