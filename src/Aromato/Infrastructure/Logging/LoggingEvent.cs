using Microsoft.Extensions.Logging;

namespace Aromato.Infrastructure.Logging
{
    public class LoggingEvent
    {

        #region Employee Events

        public static EventId EmployeeCreated = 1000;
        public static EventId EmployeeEmailChange = 1001;
        public static EventId EmployeeContactNumberChange = 1002;
        public static EventId EmployeePunch = 1003;

        #endregion
    }
}
