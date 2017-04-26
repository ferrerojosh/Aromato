using Aromato.Domain.Aggregate;

namespace Aromato.Domain.Repository
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Inventory FindByName(string name);
    }
}