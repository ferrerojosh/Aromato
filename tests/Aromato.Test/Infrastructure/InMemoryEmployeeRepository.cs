using System.Collections.Generic;
using System.Linq;
using Aromato.Domain;
using Aromato.Domain.Employee;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Test.Infrastructure
{
    /// <summary>
    /// In-memory employee repository for the purpose of unit testing. This should not belong to the main assembly.
    /// </summary>
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly InMemoryUnitOfWork _unitOfWork;

        public InMemoryEmployeeRepository(InMemoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Employee FindById(long id)
        {
            return _unitOfWork.Employees.Find(id);
        }

        public IEnumerable<Employee> FindAll()
        {
            return _unitOfWork.Employees.ToList();
        }

        public IEnumerable<Employee> FindBySpec(ISpecification<long, Employee> specification)
        {
            return _unitOfWork.Employees
                .Where(specification.IsSatisified())
                .AsEnumerable();
        }

        public void Add(Employee aggregate)
        {
            _unitOfWork.Employees.Add(aggregate);
        }

        public void Modify(Employee aggregate)
        {
            _unitOfWork.Entry(aggregate).State = EntityState.Modified;
        }

        public void Remove(Employee aggregate)
        {
            _unitOfWork.Employees.Remove(aggregate);
        }

        public void RemoveById(long id)
        {
            _unitOfWork.Employees.Remove(FindById(id));
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;
    }
}