using System.Reflection;
using Aromato.Domain;
using Autofac;
using Autofac.Features.Variance;

namespace Aromato.Infrastructure.Events
{
    public class AutoFacEventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _container;

        public AutoFacEventDispatcher()
        {
            var builder = new ContainerBuilder();
            var assembly = typeof(AutoFacEventDispatcher).GetTypeInfo().Assembly;

            builder.RegisterSource(new ContravariantRegistrationSource());

            builder
                .RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IDomainEventHandler<>))
                .AsImplementedInterfaces();

            _container = builder.Build();
        }

        public void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            var handler = _container.Resolve<IDomainEventHandler<TEvent>>();
            
            handler.HandleEvent(eventToDispatch);
        }
    }
}