using System.Collections.Generic;
using System.Linq;
using Aromato.Domain;
using Aromato.Domain.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.PostgreSQL
{
    public class PostgresInventoryRepository : IInventoryRepository
    {
        private readonly PostgresUnitOfWork _unitOfWork;

        public PostgresInventoryRepository(PostgresUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Inventory FindById(long id)
        {
            return _unitOfWork.Inventories
                .Include(i => i.Items)
                .Include("Items.Item")
                .FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Inventory> FindAll()
        {
            return _unitOfWork.Inventories
                .Include(i => i.Items)
                .Include("Items.Item")
                .AsEnumerable();
        }

        public IEnumerable<Inventory> FindBySpec(ISpecification<long, Inventory> specification)
        {
            return _unitOfWork.Inventories
                .Include(i => i.Items)
                .Include("Items.Item")
                .Where(specification.IsSatisified)
                .AsEnumerable();
        }

        public void Add(Inventory aggregate)
        {
            _unitOfWork.Inventories.Add(aggregate);
        }

        public void Modify(Inventory aggregate)
        {
            _unitOfWork.Entry(aggregate).State = EntityState.Modified;
        }

        public void Remove(Inventory aggregate)
        {
            _unitOfWork.Inventories.Remove(aggregate);
        }

        public void RemoveById(long id)
        {
            Remove(FindById(id));
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public Inventory FindByName(string name)
        {
            return _unitOfWork.Inventories.First(i => i.Name == name);
        }
    }
}