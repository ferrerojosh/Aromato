namespace Aromato.Domain
{
    public interface IDomainEventHandler<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        void HandleEvent(TDomainEvent @event);
    }
}
