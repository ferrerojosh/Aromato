using System;
using System.Linq.Expressions;

namespace Aromato.Domain
{
    /// <summary>
    /// Interface for the Specification pattern. A specification is an abstraction of a WHERE clause in a
    /// relational type of database. In other underlying databases, this is a filter for an item inside a
    /// document store or file.
    /// For more information please see http://martinfowler.com/apsupp/spec.pdf
    /// </summary>
    /// <remarks>
    /// This is an implementation using LINQ. This can be easily done using lambda expressions which are
    /// built into C# for easier specification creation.
    /// </remarks>
    /// <typeparam name="TKey">The identifier type.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface ISpecification<TKey, TEntity>
        where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Checks if the specification defined is satisifed by the passed lambda expression.
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> IsSatisified();
    }
}