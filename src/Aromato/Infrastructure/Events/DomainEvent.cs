using Aromato.Domain;

namespace Aromato.Infrastructure.Events
{
    /// <summary>
    /// Raises domain events.
    /// </summary>
    public class DomainEvent
    {
        /// <summary>
        /// The domain event dispatcher.
        /// </summary>
        public static IEventDispatcher Dispatcher { get; set; }
        /// <summary>
        /// Raises a domain event.
        /// </summary>
        /// <param name="event">The event to raise.</param>
        /// <typeparam name="TEvent">Domain event type.</typeparam>
        public static void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            Dispatcher?.Dispatch(@event);
        }
    }
}
