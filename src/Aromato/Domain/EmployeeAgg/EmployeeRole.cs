using Aromato.Domain.RoleAgg;

namespace Aromato.Domain.EmployeeAgg
{
    public class EmployeeRole
    {
        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}