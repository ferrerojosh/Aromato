using System;

namespace Aromato.Domain.Employee
{
    public sealed class Employee : IAggregateRoot<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Employee()
        {

        }

        public Employee(string firstName, string lastName, string middleName, Gender gender, DateTime dateOfBirth,
            string email, string contactNo)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            ContactNo = contactNo;
        }

        public long Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public string Name => $"{LastName}, {FirstName} {MiddleName}";
        public string ContactNo { get; protected set; }
        public string Email { get; protected set; }
        public Gender Gender { get; }
        public DateTime DateOfBirth { get; }

        public void ChangeEmail(string newEmail)
        {
            Email = newEmail;
        }

        public void ChangeContactNo(string newContactNo)
        {
            ContactNo = newContactNo;
        }
    }
}