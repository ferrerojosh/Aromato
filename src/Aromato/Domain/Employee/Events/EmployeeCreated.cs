namespace Aromato.Domain.Employee.Events
{
    public class EmployeeCreated : IDomainEvent
    {
        public Employee Employee { get; set; }
    }
}
