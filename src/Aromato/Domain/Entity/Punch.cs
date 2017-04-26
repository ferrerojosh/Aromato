using System;
using Aromato.Domain.Enumeration;

namespace Aromato.Domain.Entity
{
    public class Punch : IEntity
    {
        protected Punch()
        {
            // Required by Entity Framework
        }

        public Punch(PunchType punchType)
        {
            Type = punchType;
            DateTime = DateTime.Now;
        }

        public Guid Id { get; set; }

        public PunchType Type { get; protected set; }
        public DateTime DateTime { get; protected set; }
    }
}