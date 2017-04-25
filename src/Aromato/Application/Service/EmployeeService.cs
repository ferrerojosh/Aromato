using System;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Enumeration;
using Aromato.Domain.Repository;

namespace Aromato.Application.Service
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void RemoveEmployee(Employee employee)
        {
            _employeeRepository.Remove(employee);
            _employeeRepository.SaveChanges();
        }

        public void CreateEmployee(string firstName, string lastName, string middleName, Gender gender, DateTime dateOfBirth)
        {
            var employee = new Employee(firstName, lastName, middleName, gender, dateOfBirth);

            _employeeRepository.Add(employee);
            _employeeRepository.SaveChanges();
        }
    }
}