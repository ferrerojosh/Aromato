namespace Aromato.Domain.EmployeeAgg.Events
{
    public class EmployeeCreated : IDomainEvent
    {
        public Employee Employee { get; set; }
    }
}
