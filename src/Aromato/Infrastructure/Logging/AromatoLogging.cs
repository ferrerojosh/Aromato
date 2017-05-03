using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Aromato.Infrastructure.Logging
{
    public class AromatoLogging
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory().AddNLog(new NLogProviderOptions());
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    }
}
