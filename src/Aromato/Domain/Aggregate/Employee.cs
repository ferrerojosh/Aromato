using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Aromato.Domain.Entity;
using Aromato.Domain.Enumeration;
using Aromato.Domain.Helper;

namespace Aromato.Domain.Aggregate
{
    public class Employee : IAggregateRoot
    {
        public Employee(string firstName, string lastName, string middleName, Gender gender, DateTime dateOfBirth)
        {
            Contract.Requires(dateOfBirth.Year < DateTime.Now.Year);

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public Guid Id { get; set; }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string MiddleName { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public Gender Gender { get; protected set; }

        public string ContactNo { get; protected set; }
        public string Email { get; protected set; }

        public IEnumerable<Role> Roles { get; protected set; }

        #region Calculated Properties

        public string Name => LastName + ", " + FirstName;
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        #endregion

        public void ChangeEmail(string email)
        {
            Contract.Requires(EmailValidator.IsValidEmail(email));
            Email = email;
        }

        public void ChangeContactNo(string newNumber)
        {
            Contract.Requires(newNumber.StartsWith("09") && newNumber.Length == 11);
            ContactNo = newNumber;
        }
    }
}