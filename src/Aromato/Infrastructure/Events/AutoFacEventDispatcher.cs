using System;
using Aromato.Domain;
using Aromato.Domain.Employee.Events;
using Aromato.Infrastructure.Events.Employee;
using Autofac;

namespace Aromato.Infrastructure.Events
{
    public class AutoFacEventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _container;

        public AutoFacEventDispatcher()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance<IDomainEventHandler<EmployeeEmailChanged>>(new EmployeeEmailChangedEventHandler());

            _container = builder.Build();
        }

        public void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            var handler = _container.Resolve<IDomainEventHandler<TEvent>>();
            
            handler.HandleEvent(eventToDispatch);
        }
    }
}