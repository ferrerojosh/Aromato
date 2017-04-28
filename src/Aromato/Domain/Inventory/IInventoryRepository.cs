namespace Aromato.Domain.Inventory
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        Inventory FindByName(string name);
    }
}