using System;
using System.Collections.Generic;
using System.Linq;
using Aromato.Application.Web.Data;
using Aromato.Domain.Employee;

namespace Aromato.Application.Web
{
    public class EmployeeWebService : IEmployeeService<long, Employee>
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

        public void CreateEmployee(IData<long, Employee> employeeData)
        {
            var employeeWebData = (EmployeeWebData) employeeData;

            var employee = new Employee(
                employeeWebData.UniqueId,
                employeeWebData.FirstName,
                employeeWebData.LastName,
                employeeWebData.MiddleName,
                (Gender)Enum.Parse(typeof(Gender), employeeWebData.Gender),
                DateTime.Parse(employeeWebData.DateOfBirth)
            );

            employee.ChangeEmail(employeeWebData.Email);
            employee.ChangeContactNo(employeeWebData.ContactNo);

            _employeeRepository.Add(employee);
            _employeeRepository.UnitOfWork.Commit();
        }
    }
}