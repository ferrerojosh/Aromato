using System;

namespace Aromato.Domain.EmployeeAgg
{
    public class Punch : IEntity<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Punch()
        {
        }

        public static Punch Create(PunchType type)
        {
            return new Punch
            {
                Type = type,
                DateAdded = DateTime.Now
            };
        }

        public long Id { get; set; }
        public PunchType Type { get; protected set; }
        public DateTime DateAdded { get; protected set; }

    }
}