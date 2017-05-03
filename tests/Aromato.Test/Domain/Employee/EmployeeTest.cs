using System;
using System.Collections.Immutable;
using Aromato.Domain.Employee;
using Aromato.Infrastructure.Events;
using Xunit;

namespace Aromato.Test.Domain.Employee
{
    public class EmployeeTest
    {
        public Aromato.Domain.Employee.Employee TestEmployee()
        {
            var uniqueId = "15102013";
            var firstName = "Employee";
            var lastName = "Sweat";
            var middleName = "Super";
            var gender = Gender.Male;
            var dateOfBirth = DateTime.Parse("02/02/1990");
            var email = "hello@live.com";
            var contactNo = "09223334444";

            return Aromato.Domain.Employee.Employee.Create(uniqueId, firstName, lastName, middleName, gender, dateOfBirth, email, contactNo);
        }

        [Fact]
        public void CanAcceptValidEmail()
        {
            var validEmails = ImmutableList.Create(
                "email@example.com",
                "firstname.lastname@example.com",
                "email@subdomain.example.com",
                "firstname+lastname@example.com",
                "email@123.123.123.123",
                "email@[123.123.123.123]",
                @"""email""@example.com",
                "1234567890@example.com",
                "email@example-one.com",
                "email@example.name",
                "email@example.museum",
                "email@example.co.jp",
                "firstname-lastname@example.com"
            );

            var employee = TestEmployee();

            validEmails.ForEach(email => employee.ChangeEmail(email));
        }

        [Fact]
        public void CanThrowExceptionOnInvalidEmail()
        {
            var invalidEmails = ImmutableList.Create(
                "plainaddress",
                "#@%^%#$@#$@#.com",
                "@example.com",
                "Joe Smith <email@example.com>",
                "email.example.com",
                "email@example@example.com",
                ".email@example.com",
                "email.@example.com",
                "email..email@example.com",
                "あいうえお@example.com",
                "email@example.com (Joe Smith)",
                "email@example",
                "email@-example.com",
                "email@example..com",
                "Abc..123@example.com"
            );

            var employee = TestEmployee();

            invalidEmails.ForEach(
                invalidEmail => Assert.Throws<ArgumentException>(() => employee.ChangeEmail(invalidEmail))
            );
        }

        [Fact]
        public void CanThrowExceptionOnInvalidMobile()
        {
            var invalidMobileNumbers = ImmutableList.Create(
                "++51 874645",
                "+71  84364356",
                "+91 808 75 74 678",
                "+91 808-75-74-678",
                "+91-846363",
                "80873",
                "0000000000",
                "+91 0000000"
            );

            var employee = TestEmployee();

            invalidMobileNumbers.ForEach(
                invalidMobile => Assert.Throws<ArgumentException>(() => employee.ChangeContactNo(invalidMobile))
            );
        }

        [Fact]
        public void CanAcceptValidMobileNumbers()
        {
            var validMobileNumbers = ImmutableList.Create(
                "+1 8087339090",
                "+91 8087339090",
                "+9128087339090",
                "+1-808-733-9090",
                "+91-8087339090",
                "+912-8087339090",
                "+918087677876",
                "+9108087735454",
                "09223537173",
                "+63-922-353-7173"
            );

            var employee = TestEmployee();

            validMobileNumbers.ForEach(mobileNo => employee.ChangeContactNo(mobileNo));
        }
    }
}