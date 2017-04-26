using System;
using System.Collections.Generic;

namespace Aromato.Domain
{
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        TEntity FindById(Guid id);
        IEnumerable<TEntity> FindAll();
        void Add(TEntity entity);
        void Modify(TEntity entity);
        void Remove(TEntity entity);
    }
}