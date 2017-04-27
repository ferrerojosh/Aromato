namespace Aromato.Domain.Inventory
{
    public class Inventory : IAggregateRoot<long>
    {
        public long Id { get; set; }
    }
}