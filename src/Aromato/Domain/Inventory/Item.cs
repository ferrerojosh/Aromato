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
            DateAdded = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        public Item(string uniqueId) : this()
        {
            UniqueId = uniqueId;
        }

        public long Id { get; set; }

        public virtual string UniqueId { get; private set; }
        public virtual string Name { get; private set; }
        public virtual ItemStatus Status { get; protected set; }
        public DateTime DateAdded { get; protected set; }
        public DateTime LastUpdated { get; protected set; }

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