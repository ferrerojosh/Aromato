using System.Collections.Generic;

namespace Aromato.Domain.RoleAgg
{
    public class Permission : IEntity<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Permission()
        {
        }

        public long Id { get; set; }
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual IList<RolePermission> Roles { get; set; }
    }
}