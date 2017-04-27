using System.Collections.Generic;
using Aromato.Domain;
using Aromato.Domain.Inventory;

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
            throw new System.NotImplementedException();
        }

        public IEnumerable<Inventory> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Inventory> FindBySpec(ISpecification<long, Inventory> specification)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Inventory aggregate)
        {
            throw new System.NotImplementedException();
        }

        public void Modify(Inventory aggregate)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Inventory aggregate)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveById(long id)
        {
            throw new System.NotImplementedException();
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;
    }
}