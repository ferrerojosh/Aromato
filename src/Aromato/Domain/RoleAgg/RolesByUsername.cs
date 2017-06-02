using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aromato.Domain.RoleAgg
{
    public class RolesByUsername : ISpecification<long, Role>
    {
        private readonly string _username;
        
        public RolesByUsername(string username)
        {
            _username = username;
        }

        public Expression<Func<Role, bool>> IsSatisified
        {
            get
            {
                return role => role.Employees
                    .Select(e => e.Employee)
                    .Select(e => e.Credential)
                    .Any(e => e.Username == _username);
            }
        }
    }
}