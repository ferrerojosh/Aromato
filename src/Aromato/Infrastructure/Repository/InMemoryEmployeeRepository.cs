using System.Collections.Generic;
using System.Linq;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.Repository
{
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
            return _unitOfWork.Employees
                .Include(e => e.Roles)
                .Include(e => e.Punches)
                .ToList();
        }

        public void Add(Employee entity)
        {
            _unitOfWork.Employees.Add(entity);
        }

        public void Modify(Employee entity)
        {
            _unitOfWork.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Employee entity)
        {
            _unitOfWork.Employees.Remove(entity);
        }

    }
}