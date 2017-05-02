using System;
using System.Collections.Generic;
using System.Linq;
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
        public virtual IList<Role> Roles { get; } = new List<Role>();
        public virtual IList<DutySchedule> DutySchedules { get; } = new List<DutySchedule>();
        public virtual IList<Punch> Punches { get; } = new List<Punch>();

        public virtual void AddRole(Role role)
        {
            Roles.Add(role);
        }

        public virtual void AddSchedule(DutySchedule dutySchedule)
        {
            DutySchedules.Add(dutySchedule);
        }

        public virtual Punch DoPunch()
        {
            Punch punch;
            if (Punches.Count == 0)
            {
                punch = Punch.Create(PunchType.In);
            }
            else
            {
                var lastPunch = Punches.Last(p => p.DateAdded.Date == DateTime.Now.Date);

                if (lastPunch.DateAdded.Minute == DateTime.Now.Minute)
                {
                    throw new InvalidOperationException("Cannot punch in the same minute");
                }

                punch = Punch.Create(lastPunch.Type == PunchType.In ? PunchType.Out : PunchType.In);
            }

            Punches.Add(punch);
            return punch;
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