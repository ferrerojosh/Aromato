using System;
using Aromato.Domain;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Enumeration;
using Aromato.Domain.Repository;

namespace Aromato.Application.Service
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public void RemoveEmployee(Employee employee)
        {
            _employeeRepository.Remove(employee);
            _unitOfWork.Commit();
        }

        public void CreateEmployee(string firstName, string lastName, string middleName, Gender gender, DateTime dateOfBirth)
        {
            var employee = new Employee(firstName, lastName, middleName, gender, dateOfBirth);

            _employeeRepository.Add(employee);
            _unitOfWork.Commit();
        }
    }
}