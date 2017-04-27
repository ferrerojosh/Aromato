using System;
using System.Collections.Generic;
using System.Linq;
using Aromato.Application.Web.Data;
using Aromato.Domain.Employee;

namespace Aromato.Application.Web
{
    public class EmployeeWebService : IEmployeeService<long, EmployeeWebData, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeWebService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IData<long, Employee> RetrieveById(long id)
        {
            var employee = _employeeRepository.FindById(id);
            return new EmployeeWebData().Fill(employee);
        }

        public IEnumerable<IData<long, Employee>> RetrieveAll()
        {
            var employees = _employeeRepository.FindAll();
            return employees.Select(employee => new EmployeeWebData().Fill(employee));
        }

        public void CreateEmployee(EmployeeWebData employeeData)
        {
            var employee = new Employee(
                employeeData.FirstName,
                employeeData.LastName,
                employeeData.MiddleName,
                (Gender)Enum.Parse(typeof(Gender), employeeData.Gender),
                DateTime.Parse(employeeData.DateOfBirth),
                employeeData.Email,
                employeeData.ContactNo
            );
            _employeeRepository.Add(employee);
            _employeeRepository.UnitOfWork.Commit();
        }
    }
}