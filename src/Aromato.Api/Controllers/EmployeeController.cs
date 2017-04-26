using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aromato.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aromato.Infrastructure;
using Aromato.Application.Service;
using Aromato.Infrastructure.Repository;
using Aromato.Domain.Enumeration;

namespace Aromato.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {

        [HttpGet("{id}")]
        public Domain.Aggregate.Employee Get(Guid id)
        {
            using (var unitOfWork = new EfUnitOfWork())
            {
                var employeeRepository = new EfEmployeeRepository(unitOfWork);

                return employeeRepository.FindById(id);
            }
        }

        [HttpGet]
        public IEnumerable<Domain.Aggregate.Employee> Get()
        {
            using (var unitOfWork = new EfUnitOfWork())
            {
                var employeeRepository = new EfEmployeeRepository(unitOfWork);

                return employeeRepository.FindAll();
            }
        }

        [HttpPut("{id}/email")]
        public void Put(Guid id, [FromBody] dynamic data)
        {
            string email = data.email;
            using (var unitOfWork = new EfUnitOfWork())
            {
                var employeeRepository = new EfEmployeeRepository(unitOfWork);
                var employee = employeeRepository.FindById(id);

                employee.ChangeEmail(email);
                unitOfWork.Commit();
            }
        }

        // POST
        [HttpPost]
        public void Post([FromBody] dynamic employee)
        {
            string firstName = employee.firstName;
            string lastName = employee.lastName;
            string middleName = employee.middleName;
            string dateOfBirthStr = employee.dateOfBirthStr;
            string genderStr = employee.genderStr;

            var dateOfBirth = DateTime.Parse(dateOfBirthStr);
            var gender = (Gender)Enum.Parse(typeof(Gender), genderStr);

            using (var unitOfWork = new EfUnitOfWork())
            {
                var employeeRepository = new EfEmployeeRepository(unitOfWork);
                var employeeService = new EmployeeService(employeeRepository, unitOfWork);

                employeeService.CreateEmployee(firstName, lastName, middleName, gender, dateOfBirth);
            }
        }

    }
}