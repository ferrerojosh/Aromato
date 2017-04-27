namespace Aromato.Domain
{
    /// <summary>
    /// Interface for an aggregate root. An aggregate root is an entity which has a
    /// "has-a" relationship to any other entity. This includes a collection of entities.
    /// Please see https://martinfowler.com/bliki/DDD_Aggregate.html for more details.
    /// </summary>
    /// <remarks>
    /// An aggregate root is at the same time, an entity. This is why it implements
    /// the <see cref="IEntity{TKey}"/> interface.
    /// </remarks>
    /// <typeparam name="TKey">The identifier type.</typeparam>
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {

    }
}