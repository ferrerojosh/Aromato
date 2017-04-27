using Aromato.Domain;

namespace Aromato.Application
{
    public interface IEmployeeService<TKey, TData, TEntity> : IReadService<TKey, TEntity>
        where TEntity : IEntity<TKey>
        where TData : IData<TKey, TEntity>
    {
        void CreateEmployee(TData employeeData);
    }
}