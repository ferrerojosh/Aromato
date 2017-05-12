using System;

namespace Aromato.Domain.InventoryAgg
{
    public class InventoryItem : IEntity<long>
    {
        private DateTime? _dateAdded;
        private DateTime? _lastUpdated;

        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected InventoryItem()
        {
        }

        public static InventoryItem Create(string uniqueId, Item item)
        {
            return new InventoryItem
            {
                UniqueId = uniqueId,
                Item = item,
            };
        }

        public long Id { get; set; }
        public virtual Item Item { get; private set; }
        public virtual string UniqueId { get; private set; }
        public virtual ItemStatus Status { get; protected set; }

        public virtual DateTime? DateAdded
        {
            get => _dateAdded ?? (_dateAdded = DateTime.Now);
            protected set => _dateAdded = value;
        }

        public virtual DateTime? LastUpdated
        {
            get => _lastUpdated ?? (_lastUpdated = DateTime.Now);
            protected set => _lastUpdated = value;
        }
    }
}