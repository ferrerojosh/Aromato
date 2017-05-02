using System.Collections.Generic;
using System.Linq;
using Aromato.Domain;
using Aromato.Domain.Employee;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.PostgreSQL
{
    public class PostgresEmployeeRepository : IEmployeeRepository
    {

        private readonly PostgresUnitOfWork _unitOfWork;

        public PostgresEmployeeRepository(PostgresUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Employee FindById(long id)
        {
            return _unitOfWork.Employees
                .Include(e => e.Punches)
                .First(e => e.Id == id);
        }

        public IEnumerable<Employee> FindAll()
        {
            return _unitOfWork.Employees.AsEnumerable();
        }

        public IEnumerable<Employee> FindBySpec(ISpecification<long, Employee> specification)
        {
            return _unitOfWork.Employees
                .Where(specification.IsSatisified)
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
            _unitOfWork.Remove(aggregate);
        }

        public void RemoveById(long id)
        {
            Remove(FindById(id));
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public Employee FindByUniqueId(string uniqueId)
        {
            return _unitOfWork.Employees.First(e => e.UniqueId == uniqueId);
        }
    }
}