using System;
using System.Collections.Generic;
using System.Linq;
using Aromato.Domain.Entity;
using Aromato.Domain.Enumeration;
using Aromato.Domain.Helper;

namespace Aromato.Domain.Aggregate
{
    public class Employee : IAggregateRoot
    {

        protected Employee()
        {
            // required for Entity Framework
        }

        public Employee(string firstName, string lastName, string middleName, Gender gender, DateTime dateOfBirth)
        {
            if (dateOfBirth.Year > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(dateOfBirth));
            }

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
            Gender = gender;

            Roles = new List<Role>();
            Punches = new List<Punch>();
        }

        public Guid Id { get; set; }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string MiddleName { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public Gender Gender { get; protected set; }

        public string ContactNo { get; protected set; }
        public string Email { get; protected set; }

        public IList<Role> Roles { get; protected set; }
        public IList<Punch> Punches { get; protected set; }

        #region Calculated Properties

        public string Name => LastName + ", " + FirstName;
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        #endregion

        public void ChangeEmail(string email)
        {
            if (!EmailValidator.IsValidEmail(email))
            {
                 throw new ArgumentException("invalid email");
            }
            Email = email;
        }

        public void Punch()
        {
            var today = DateTime.Now.Date;
            var lastPunch = Punches.Count > 0 ? Punches.Last() : null;

            // check if first punch for the employee
            if (Punches.Count == 0 || lastPunch == null)
            {
                Punches.Add(new Punch(PunchType.In));
                return;
            }

            // punch for the day
            if (today.Equals(lastPunch.DateTime.Date))
            {
                Punches.Add(lastPunch.Type == PunchType.In ? new Punch(PunchType.Out) : new Punch(PunchType.In));
            }
            else
            {
                Punches.Add(new Punch(PunchType.In));
            }
        }

        public void ChangeContactNo(string newNumber)
        {
            if (!newNumber.StartsWith("09") || newNumber.Length != 11)
            {
                 throw new ArgumentException("invalid contact number");
            }
            ContactNo = newNumber;
        }
    }
}