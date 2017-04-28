using System;
using System.Collections.Generic;
using Aromato.Helpers;

namespace Aromato.Domain.Employee
{
    public class Employee : IAggregateRoot<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Employee()
        {
            Roles = new List<Role>();
            DutySchedules = new List<DutySchedule>();
            Punches = new List<Punch>();
        }

        public Employee(string uniqueId, string firstName, string lastName, string middleName, Gender gender, DateTime dateOfBirth)
        {
            UniqueId = uniqueId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }

        public long Id { get; set; }
        public virtual string UniqueId { get; private set;}
        public virtual string FirstName { get; private set; }
        public virtual string LastName { get; private set;}
        public virtual string MiddleName { get; private set; }
        public virtual string Name => $"{LastName}, {FirstName} {MiddleName}";
        public virtual string ContactNo { get; private set; }
        public virtual string Email { get; private set; }
        public virtual Gender Gender { get; private set; }
        public virtual DateTime DateOfBirth { get; private set; }
        public IList<Role> Roles { get; private set; }
        public IList<DutySchedule> DutySchedules { get; private set; }
        public IList<Punch> Punches { get; private set; }

        public void AddRole(Role role)
        {
            Roles.Add(role);
        }

        public void AddSchedule(DutySchedule dutySchedule)
        {
            DutySchedules.Add(dutySchedule);
        }

        public void ChangeEmail(string newEmail)
        {
            if (!RegexUtilities.IsValidEmail(newEmail))
            {
                throw new ArgumentException($"{newEmail} is not a valid email");
            }
            Email = newEmail;
        }

        public void ChangeContactNo(string newContactNo)
        {
            if (!RegexUtilities.IsValidContactNo(newContactNo))
            {
                throw new ArgumentException($"{newContactNo} is not a valid number");
            }
            ContactNo = newContactNo;
        }
    }
}