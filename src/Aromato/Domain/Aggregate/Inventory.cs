﻿using System;
using System.Collections;
using Aromato.Domain.Entity;

namespace Aromato.Domain.Aggregate
{
    public class Inventory : IAggregateRoot
    {
        public Guid Id { get; set; }

        public string Name { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public IList Items { get; protected set; }

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