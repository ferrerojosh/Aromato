using System;

namespace Aromato.Domain
{
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        TEntity FindById(Guid id);
        void Add(TEntity entity);
        void Modify(TEntity entity);
    }
}