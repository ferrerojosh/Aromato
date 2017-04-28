namespace Aromato.Domain.Employee
{
    public interface IEmployeeRepository : IRepository<long, Employee>
    {
        Employee FindByUniqueId(string uniqueId);
    }
}