using Aromato.Domain;
using Aromato.Domain.Employee.Events;
using Aromato.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace Aromato.Infrastructure.Events.Employee
{
    public class EmployeeEmailChangedEventHandler : IDomainEventHandler<EmployeeEmailChanged>
    {
        public void HandleEvent(EmployeeEmailChanged @event)
        {
            var log = AromatoLogging.CreateLogger<EmployeeEmailChangedEventHandler>();

            log.LogInformation(
                LoggingEvent.EmployeeEmailChange, 
                "Employee email changed. [id: {id}, oldEmail: {oldEmail}, newEmail: {newEmail}]", 
                @event.EmployeeId, @event.OldEmail, @event.NewEmail
            );
        }
    }
}
