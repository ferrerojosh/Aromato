using System;
using System.Linq;
using Aromato.Domain.Aggregate;
using Aromato.Domain.Enumeration;
using Xunit;

namespace Aromato.Test.Domain.Aggregate
{
    public class EmployeeTest
    {
        private const string FirstName = "Sample";
        private const string LastName = "Ample";
        private const string MiddleName = "Amp";
        private const Gender Gender = Aromato.Domain.Enumeration.Gender.Male;
        private static readonly DateTime DateOfBirth = DateTime.Parse("22/04/1990");

        [Fact]
        public void CanThrowOutOfRangeOnNewInstance()
        {
            var illegalDateOfBirth = DateTime.Parse("01/01/2100");
            Assert.Throws<ArgumentOutOfRangeException>(() => new Employee(
                FirstName,
                LastName,
                MiddleName,
                Gender,
                illegalDateOfBirth
            ));
        }

        [Fact]
        public void CanChangeEmail()
        {
            var employee = new Employee(FirstName, LastName, MiddleName, Gender, DateOfBirth);
            var email = "hello@world.com";

            employee.ChangeEmail(email);
            Assert.Equal(email, employee.Email);
        }

        [Fact]
        public void CanThrowArgumentExceptionOnInvalidEmail()
        {
            var employee = new Employee(FirstName, LastName, MiddleName, Gender, DateOfBirth);
            var invalidEmail = "dankestmemes@@$$$";

            Assert.Throws<ArgumentException>(() => employee.ChangeEmail(invalidEmail));
        }

        [Fact]
        public void CanChangeContactNo()
        {
            var employee = new Employee(FirstName, LastName, MiddleName, Gender, DateOfBirth);
            var contact = "09221111111";

            employee.ChangeContactNo(contact);
            Assert.Equal(contact, employee.ContactNo);
        }

        [Fact]
        public void CanThrowArgumentExceptionOnInvalidContactNo()
        {
            var employee = new Employee(FirstName, LastName, MiddleName, Gender, DateOfBirth);
            var illegalContact = "0931231231122312";
            // checks for more than 11 characters
            Assert.Throws<ArgumentException>(() => employee.ChangeContactNo(illegalContact));
            // checks if starts with 09
            var illegalContact2 = "12312312123121233";
            Assert.Throws<ArgumentException>(() => employee.ChangeContactNo(illegalContact2));
        }

        [Fact]
        public void CanPunchIn()
        {
            var employee = new Employee(FirstName, LastName, MiddleName, Gender, DateOfBirth);

            employee.Punch();
            Assert.NotNull(employee.Punches.Last(punch => punch.Type == PunchType.In));
        }

        [Fact]
        public void CanPunchOut()
        {
            var employee = new Employee(FirstName, LastName, MiddleName, Gender, DateOfBirth);

            employee.Punch();
            employee.Punch();
            Assert.NotNull(employee.Punches.Last(punch => punch.Type == PunchType.Out));
        }
    }
}