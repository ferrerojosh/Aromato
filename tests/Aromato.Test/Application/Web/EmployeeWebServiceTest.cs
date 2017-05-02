using System.Linq;
using Aromato.Application.Web;
using Aromato.Application.Web.Data;
using Aromato.Test.Infrastructure;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Aromato.Test.Application.Web
{
    public class EmployeeWebServiceTest
    {
        [Fact]
        public void CanCreateEmployee()
        {
            var uniqueId = "15102013";
            var firstName = "Employee";
            var lastName = "Sweat";
            var middleName = "Super";
            var gender = "Male";
            var dateOfBirth = "02/02/1990";
            var email = "hello@live.com";
            var contactNo = "0922222222222";

            var loggerFactory = new LoggerFactory()
                .AddConsole();

            var logger = loggerFactory.CreateLogger<EmployeeWebService>();

            using (var unitOfWork = new InMemoryUnitOfWork())
            {
                var employeeRepository = new InMemoryEmployeeRepository(unitOfWork);
                var employeeService = new EmployeeWebService(employeeRepository);

                var employeeWebData = new EmployeeWebData
                {
                    UniqueId = uniqueId,
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    Gender = gender,
                    DateOfBirth = dateOfBirth,
                    Email = email,
                    ContactNo = contactNo
                };

                employeeService.CreateEmployee(employeeWebData);

                var retrieveWebData = (EmployeeWebData) employeeService.RetrieveAll().First();

                Assert.Equal(firstName, retrieveWebData.FirstName);
                Assert.Equal(lastName, retrieveWebData.LastName);
                Assert.Equal(middleName, retrieveWebData.MiddleName);
                Assert.Equal(gender, retrieveWebData.Gender);
                Assert.Equal(dateOfBirth, retrieveWebData.DateOfBirth);
                Assert.Equal(email, retrieveWebData.Email);
                Assert.Equal(contactNo, retrieveWebData.ContactNo);
            }
        }
    }
}