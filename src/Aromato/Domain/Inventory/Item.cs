using System;

namespace Aromato.Domain.Inventory
{
    public class Item : IEntity<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Item()
        {
        }

        public static Item Create(string uniqueId, string name, string description)
        {
            return new Item
            {
                Name = name,
                Description = description,
                DateAdded = DateTime.Now,
                LastUpdated = DateTime.Now,
                UniqueId = uniqueId
            };
        }

        public long Id { get; set; }

        public virtual string UniqueId { get; private set; }
        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public virtual ItemStatus Status { get; protected set; }
        public virtual DateTime DateAdded { get; protected set; }
        public virtual DateTime LastUpdated { get; protected set; }

        protected bool Equals(Item other)
        {
            return string.Equals(UniqueId, other.UniqueId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Item) obj);
        }

        public override int GetHashCode()
        {
            return (UniqueId != null ? UniqueId.GetHashCode() : 0);
        }

        public static bool operator ==(Item left, Item right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Item left, Item right)
        {
            return !Equals(left, right);
        }
    }
}