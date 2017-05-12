namespace Aromato.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        Inventory FindByName(string name);
    }
}