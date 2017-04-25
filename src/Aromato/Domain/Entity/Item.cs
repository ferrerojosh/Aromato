using System;
using Aromato.Domain.Enumeration;

namespace Aromato.Domain.Entity
{
    public class Item : IEntity
    {
        public Guid Id { get; set; }

        public Item()
        {
            DateAdded = DateTime.Now;
        }

        public string UniqueId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public ItemStatus Status { get; protected set; }

        public DateTime DateAdded { get; }

        public void ChangeStatus(ItemStatus status)
        {
            Status = status;
        }
    }
}