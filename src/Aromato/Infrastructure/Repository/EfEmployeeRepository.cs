using System;
using System.Collections.Generic;
using System.Linq;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.Repository
{
    public class EfEmployeeRepository : IEmployeeRepository
    {

        private readonly EfUnitOfWork _unitOfWork;

        public EfEmployeeRepository(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Employee FindById(Guid id)
        {
            return _unitOfWork.Context.Employees.Find(id);
        }

        public IEnumerable<Employee> FindAll()
        {
            return _unitOfWork.Context.Employees.ToList();
        }

        public void Add(Employee entity)
        {
            _unitOfWork.Context.Employees.Add(entity);
        }

        public void Modify(Employee entity)
        {
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(Employee entity)
        {
            _unitOfWork.Context.Employees.Remove(entity);
        }

        public void SaveChanges()
        {
            _unitOfWork.Context.SaveChanges();
            _unitOfWork.Commit();
        }
    }
}