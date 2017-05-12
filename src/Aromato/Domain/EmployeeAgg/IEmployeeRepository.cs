namespace Aromato.Domain.EmployeeAgg
{
    public interface IEmployeeRepository : IRepository<long, Employee>
    {
        Employee FindByUniqueId(string uniqueId);
    }
}