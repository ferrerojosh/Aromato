using System.Collections.Generic;
using Aromato.Domain;

namespace Aromato.Application
{
    /// <summary>
    /// Interface for an application service which requires basic read operations. All methods return a data
    /// transfer object.
    /// </summary>
    /// <remarks>
    /// The interface might look like the same as a repository definition but this is applicable for
    /// application services that will require repository-like interfaces.
    /// </remarks>
    /// <typeparam name="TKey">The identifier type.</typeparam>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IReadService<TKey, TEntity>
        where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Retrieves an aggregate via its identifier.
        /// </summary>
        /// <param name="id">The identifier object.</param>
        /// <returns>The aggregate object.</returns>
        IData<TKey, TEntity> RetrieveById(TKey id);
        /// <summary>
        /// Retrieves all aggregates.
        /// </summary>
        /// <returns>A list of aggregates.</returns>
        IEnumerable<IData<TKey, TEntity>> RetrieveAll();
    }
}