namespace Aromato.Domain.RoleAgg
{
    public class RolePermission
    {
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}