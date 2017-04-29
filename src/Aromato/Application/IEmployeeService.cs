using Aromato.Domain;

namespace Aromato.Application
{
    public interface IEmployeeService<TKey, TEntity> : IReadService<TKey, TEntity>
        where TEntity : IEntity<TKey>
    {
        void CreateEmployee(IData<TKey, TEntity> employeeData);
        void ChangeEmail(TKey id, string email);
        void ChangeContactNo(TKey id, string contactNo);
    }
}