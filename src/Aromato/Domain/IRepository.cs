using System.Collections.Generic;

namespace Aromato.Domain
{
    /// <summary>
    /// Interface for the Repository pattern. This hides the implementation of common CRUD operations
    /// in an underlying database implementation. All repositories should only contain aggregates.
    /// Please see https://martinfowler.com/eaaCatalog/repository.html for more details.
    /// </summary>
    /// <typeparam name="TKey">The identifier type.</typeparam>
    /// <typeparam name="TAggregate">The aggregate type.</typeparam>
    public interface IRepository<TKey, TAggregate>
        where TAggregate : IAggregateRoot<TKey>
    {
        /// <summary>
        /// Returns an aggregate via their identifier.
        /// </summary>
        /// <param name="id">The identifier object.</param>
        /// <returns>The aggregate.</returns>
        TAggregate FindById(TKey id);
        /// <summary>
        /// Returns all aggregates in this repository.
        /// </summary>
        /// <returns>A list of aggregates.</returns>
        IEnumerable<TAggregate> FindAll();
        /// <summary>
        /// Returns a list of aggregates in this repository based on the specification given.
        /// </summary>
        /// <param name="specification">The specification required.</param>
        /// <returns>A list of aggregates based on the specification given.</returns>
        IEnumerable<TAggregate> FindBySpec(ISpecification<TKey, TAggregate> specification);
        /// <summary>
        /// Add an aggregate in this repository.
        /// </summary>
        /// <param name="aggregate">The aggregate.</param>
        void Add(TAggregate aggregate);
        /// <summary>
        /// Modify the state of an existing aggregate in this repository.
        /// </summary>
        /// <param name="aggregate">The modified aggregate.</param>
        void Modify(TAggregate aggregate);
        /// <summary>
        /// Remove an aggregate in this repository.
        /// </summary>
        /// <param name="aggregate">The aggregate to remove.</param>
        void Remove(TAggregate aggregate);
        /// <summary>
        /// Remove an aggregate via their identifier.
        /// </summary>
        /// <param name="id">The identifier object.</param>
        void RemoveById(TKey id);
        /// <summary>
        /// The Unit of Work associated with this repository.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}