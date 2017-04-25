using System;
using System.Linq;
using Aromato.Application.Service;
using Aromato.Domain.Enumeration;
using Aromato.Infrastructure;
using Aromato.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Aromato.Test.Application.Test
{
    public class EmployeeServiceTest
    {
        public EmployeeServiceTest()
        {
        }

        [Fact]
        public void IsEmployeeAdded()
        {
            var firstName = "John Joshua";
            var middleName = "Remonde";
            var lastName = "Ferrer";
            var gender = Gender.Male;
            var dateOfBirth = DateTime.Parse("02/09/1996");

            using (var unitOfWork = new EfUnitOfWork())
            {
                var employeeRepository = new EfEmployeeRepository(unitOfWork);
                var employeeService = new EmployeeService(employeeRepository, unitOfWork);

                employeeService.CreateEmployee(firstName, lastName, middleName, gender, dateOfBirth);
            }

            using (var unitOfWork = new EfUnitOfWork())
            {
                var employeeRepository = new EfEmployeeRepository(unitOfWork);

                var employees = employeeRepository.FindAll();

                var employee = employees.First();

                Assert.Equal(firstName, employee.FirstName);
                Assert.Equal(middleName, employee.MiddleName);
                Assert.Equal(lastName, employee.LastName);
                Assert.Equal(gender, employee.Gender);
                Assert.Equal(dateOfBirth, employee.DateOfBirth);

                employeeRepository.Remove(employee);
                unitOfWork.Commit();

                Assert.Equal(EntityState.Deleted, unitOfWork.Context.Entry(employee).State);
            }

        }
    }
}