using System.Collections.Generic;
using System.Linq;
using Aromato.Domain;
using Aromato.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.PostgreSQL
{
    public class PostgresInventoryItemRepository: IInventoryItemRepository
    {
        private readonly PostgresUnitOfWork _unitOfWork;

        public PostgresInventoryItemRepository(PostgresUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public InventoryItem FindById(long id)
        {
            return _unitOfWork.InventoryItems.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<InventoryItem> FindAll()
        {
            return _unitOfWork.InventoryItems
                .Include(i => i.Item)
                .AsEnumerable();
        }

        public IEnumerable<InventoryItem> FindBySpec(ISpecification<long, InventoryItem> specification)
        {
            return _unitOfWork.InventoryItems
                .Where(specification.IsSatisified)
                .AsEnumerable();
        }

        public void Add(InventoryItem aggregate)
        {
            _unitOfWork.InventoryItems.Add(aggregate);
        }

        public void Remove(InventoryItem aggregate)
        {
            _unitOfWork.InventoryItems.Remove(aggregate);
        }

        public void RemoveById(long id)
        {
            Remove(FindById(id));
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;
    }
}