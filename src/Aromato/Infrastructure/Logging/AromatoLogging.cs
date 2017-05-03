using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Aromato.Infrastructure.Logging
{
    public class AromatoLogging
    {
        private static ILoggerFactory _factory;

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_factory != null) return _factory;
                _factory = new LoggerFactory().AddNLog();
                return _factory;
            }
            set => _factory = value;
        }
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    }
}
