using System;
using System.Collections.Generic;
using Aromato.Application.Web.Data;
using Aromato.Domain.Employee;
using Aromato.Infrastructure.Crosscutting.Extension;
using Aromato.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace Aromato.Application.Web
{
    public class EmployeeWebService : IEmployeeService
    {
        private readonly ILogger _logger = AromatoLogging.CreateLogger<EmployeeWebService>();
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeWebService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IData RetrieveById(long id)
        {
            var employee = _employeeRepository.FindById(id);
            return employee.AsData<EmployeeWebData>();
        }

        public IEnumerable<IData> RetrieveAll()
        {
            var employees = _employeeRepository.FindAll();
            return employees.AsEnumerableData<EmployeeWebData>();
        }

        public IData Punch(long id)
        {
            _logger.LogInformation("New punch for employee id: {id}", id);
            var employee = _employeeRepository.FindById(id);
            var punch = employee.DoPunch();
            _employeeRepository.UnitOfWork.Commit();
            return punch.AsData<EmployeeWebData>();
        }

        public void CreateEmployee(IData employeeData)
        {
            var employeeWebData = (EmployeeWebData) employeeData;

            var employee = employeeWebData.AsEntity<long, Employee>();

            employee.ChangeEmail(employeeWebData.Email);
            employee.ChangeContactNo(employeeWebData.ContactNo);

            _employeeRepository.Add(employee);
            _employeeRepository.UnitOfWork.Commit();
        }

        public void ChangeEmail(long id, string email)
        {
            var employee = _employeeRepository.FindById(id);
            employee.ChangeEmail(email);
            _employeeRepository.UnitOfWork.Commit();
        }

        public void ChangeContactNo(long id, string contactNo)
        {
            var employee = _employeeRepository.FindById(id);
            employee.ChangeContactNo(contactNo);
            _employeeRepository.UnitOfWork.Commit();

        }
    }
}