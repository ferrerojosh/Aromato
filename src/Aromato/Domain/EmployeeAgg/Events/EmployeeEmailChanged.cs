namespace Aromato.Domain.EmployeeAgg.Events
{
    public class EmployeeEmailChanged : IDomainEvent
    {
        public long EmployeeId { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }


    }
}
