namespace Aromato.Domain
{
    /// <summary>
    /// Interface for an entity. An entity is anything that can be defined within the domain and it
    /// will exist uniquely as it would.
    /// </summary>
    /// <typeparam name="TKey">The identifier type.</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// The identifier of this entity.
        /// </summary>
        TKey Id { get; set; }
    }
}