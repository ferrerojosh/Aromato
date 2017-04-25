using System;
using System.Collections.Generic;
using Aromato.Domain.Aggregate;

namespace Aromato.Domain.Entity
{
    public class Role : IEntity
    {
        protected Role()
        {
            // required for Entity Framework
        }

        public Guid Id { get; set; }

        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public IEnumerable<Employee> Employees { get; protected set; }
    }
}