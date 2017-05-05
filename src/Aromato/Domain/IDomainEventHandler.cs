namespace Aromato.Domain
{
    public interface IDomainEventHandler<in TEvent>
        where TEvent : IDomainEvent
    {
        void HandleEvent(TEvent @event);
    }
}
