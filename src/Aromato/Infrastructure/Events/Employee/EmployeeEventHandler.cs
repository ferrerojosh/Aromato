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
                "Employee with id {EmployeeId} email changed from {OldEmail} to {NewEmail}", 
                @event.EmployeeId, @event.OldEmail, @event.NewEmail
            );
        }
    }

    public class EmployeeContactNoChangedEventHandler : IDomainEventHandler<EmployeeContactNoChanged>
    {
        public void HandleEvent(EmployeeContactNoChanged @event)
        {
            var log = AromatoLogging.CreateLogger<EmployeeContactNoChangedEventHandler>();

            log.LogInformation(
                "Employee with id {EmployeeId} contact number changed from {OldContactNo} to {NewContactNo}",
                @event.EmployeeId, @event.OldContactNo, @event.NewContactNo);
        }
    }

    public class EmployeeCreatedEventHandler : IDomainEventHandler<EmployeeCreated>
    {
        public void HandleEvent(EmployeeCreated @event)
        {
            var log = AromatoLogging.CreateLogger<EmployeeCreatedEventHandler>();

            log.LogInformation(
                "Employee created, data as follows: {Employee}", @event.Employee);
        }
    }
}
