using Aromato.Domain;

namespace Aromato.Infrastructure.Events
{
    public class DomainEvent
    {
        public static IEventDispatcher Dispatcher { get; set; }

        public static void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            Dispatcher.Dispatch(@event);
        }
    }
}
