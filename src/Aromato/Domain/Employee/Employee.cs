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
        }

        public static Employee Create(string uniqueId, string firstName, string lastName, string middleName, Gender gender, DateTime dateOfBirth)
        {
            return new Employee
            {
                Roles = new List<Role>(),
                DutySchedules = new List<DutySchedule>(),
                Punches = new List<Punch>(),
                UniqueId = uniqueId,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Gender = gender,
                DateOfBirth = dateOfBirth
            };
        }

        public long Id { get; set; }
        public virtual string UniqueId { get; protected set;}
        public virtual string FirstName { get; protected  set; }
        public virtual string LastName { get; protected  set;}
        public virtual string MiddleName { get; protected  set; }
        public virtual string Name => $"{LastName}, {FirstName} {MiddleName}";
        public virtual string ContactNo { get; private set; }
        public virtual string Email { get; private set; }
        public virtual Gender Gender { get; protected  set; }
        public virtual DateTime DateOfBirth { get; protected set; }
        public virtual IList<Role> Roles { get; protected  set; }
        public virtual IList<DutySchedule> DutySchedules { get; protected set; }
        public virtual IList<Punch> Punches { get; protected  set; }

        public virtual void AddRole(Role role)
        {
            Roles.Add(role);
        }

        public virtual void AddSchedule(DutySchedule dutySchedule)
        {
            DutySchedules.Add(dutySchedule);
        }

        public virtual void ChangeEmail(string newEmail)
        {
            if (!RegexUtilities.IsValidEmail(newEmail))
            {
                throw new ArgumentException($"{newEmail} is not a valid email");
            }
            Email = newEmail;
        }

        public virtual void ChangeContactNo(string newContactNo)
        {
            if (!RegexUtilities.IsValidContactNo(newContactNo))
            {
                throw new ArgumentException($"{newContactNo} is not a valid number");
            }
            ContactNo = newContactNo;
        }


    }
}