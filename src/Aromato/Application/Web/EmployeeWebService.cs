using System;
using System.Collections.Generic;
using Aromato.Application.Web.Data;
using Aromato.Domain.EmployeeAgg;
using Aromato.Infrastructure.Crosscutting.Extension;

namespace Aromato.Application.Web
{
    public class EmployeeWebService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeWebService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IData RetrieveById(long id)
        {
            var employee = FindByIdOrFail(id);
            return employee.AsData<EmployeeWebData>();
        }

        public IEnumerable<IData> RetrieveAll()
        {
            var employees = _employeeRepository.FindAll();
            return employees.AsEnumerableData<EmployeeWebData>();
        }

        public IData Punch(string uniqueId)
        {
            var employee = _employeeRepository.FindByUniqueId(uniqueId);
            var punch = employee.DoPunch();
            _employeeRepository.UnitOfWork.Commit();
            return punch.AsData<PunchWebData>();
        }

        public void CreateEmployee(IData employeeData)
        {
            var employeeWebData = (EmployeeWebData) employeeData;

            var gender = (Gender) Enum.Parse(typeof(Gender), employeeWebData.Gender);
            var dateOfBirth = DateTime.Parse(employeeWebData.DateOfBirth);

            var employee = Employee.Create(
                employeeWebData.UniqueId,
                employeeWebData.FirstName,
                employeeWebData.LastName,
                employeeWebData.MiddleName,
                gender,
                dateOfBirth,
                employeeWebData.Email,
                employeeWebData.ContactNo
            );

            _employeeRepository.Add(employee);
            _employeeRepository.UnitOfWork.Commit();
        }

        public void ChangeEmail(long id, string email)
        {
            var employee = FindByIdOrFail(id);
            employee.ChangeEmail(email);
            _employeeRepository.UnitOfWork.Commit();
        }

        public void ChangeContactNo(long id, string contactNo)
        {
            var employee = _employeeRepository.FindById(id);
            employee.ChangeContactNo(contactNo);
            _employeeRepository.UnitOfWork.Commit();
        }

        public IData RetrieveByUniqueId(string uniqueId)
        {
            var employee = _employeeRepository.FindByUniqueId(uniqueId);
            if(employee == null)
                throw new InvalidOperationException($"Employee with unique id {uniqueId} does not exist.");

            return employee.AsData<EmployeeWebData>();
        }

        private Employee FindByIdOrFail(long id)
        {
            var employee = _employeeRepository.FindById(id);
            if (employee == null)
            {
                throw new InvalidOperationException($"Employee with id {id} does not exist.");
            }
            return employee;
        }
    }
}