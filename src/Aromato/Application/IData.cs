using Aromato.Domain;

namespace Aromato.Application
{
    /// <summary>
    /// Interface for enforcing an object as a data transfer object. This is not necessary in most-cases
    /// but for compile-time assurance, we require this. Filling the data transfer object can be done through
    /// the new operator or through the <see cref="Fill"/> method. Essentially, the object can be instantiated
    /// even with null values unless enforced.
    /// </summary>
    public interface IData<TKey, in TEntity>
        where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Fills the data-transfer object with its related entity.
        /// </summary>
        /// <param name="entity">The entity to be filled.</param>
        /// <returns>The data-transfer object.</returns>
        IData<TKey, TEntity> Fill(TEntity entity);
    }
}