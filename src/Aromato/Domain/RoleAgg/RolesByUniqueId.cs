using System;
using System.Linq;
using System.Linq.Expressions;
using Aromato.Domain.EmployeeAgg;

namespace Aromato.Domain.RoleAgg
{
    public class RolesByUniqueId : ISpecification<long, Role>
    {
        private readonly string _uniqueId;

        public RolesByUniqueId(string uniqueId)
        {
            _uniqueId = uniqueId;
        }

        public Expression<Func<Role, bool>> IsSatisified
        {
            get { return role => role.Employees.Any(e => e.Employee.UniqueId == _uniqueId); }
        }
    }
}