using System.Collections.Generic;
using System.Linq;
using Aromato.Domain;
using Aromato.Domain.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Test.Infrastructure
{
    /// <summary>
    /// In-memory inventory repository for the purpose of unit testing. This should not belong to the main assembly.
    /// </summary>
    public class InMemoryInventoryRepository : IInventoryRepository
    {

        private readonly InMemoryUnitOfWork _unitOfWork;

        public InMemoryInventoryRepository(InMemoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Inventory FindById(long id)
        {
            return _unitOfWork.Inventories.Find(id);
        }

        public IEnumerable<Inventory> FindAll()
        {
            return _unitOfWork.Inventories.AsEnumerable();
        }

        public IEnumerable<Inventory> FindBySpec(ISpecification<long, Inventory> specification)
        {
            return _unitOfWork.Inventories
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
            throw new System.NotImplementedException();
        }
    }
}