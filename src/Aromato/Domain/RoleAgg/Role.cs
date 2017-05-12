using System.Collections.Generic;
using Aromato.Domain.EmployeeAgg;

namespace Aromato.Domain.RoleAgg
{
    public class Role : IAggregateRoot<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Role()
        {
        }

        public long Id { get; set; }
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual IList<RolePermission> Permissions { get; set; }
        public virtual IList<EmployeeRole> Employees { get; set; }
    }
}