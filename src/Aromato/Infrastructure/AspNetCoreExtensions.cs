using Aromato.Domain;
using Aromato.Infrastructure.Events;
using Aromato.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Aromato.Infrastructure
{
    public static class AspNetCoreExtensions
    {
        public static IApplicationBuilder UseAromato(this IApplicationBuilder app, ILoggerFactory factory, IEventDispatcher eventDispatcher)
        {
            AromatoLogging.LoggerFactory = factory;
            DomainEvent.Dispatcher = eventDispatcher;
            return app;
        }
    }
}