﻿namespace Aromato.Domain.Employee.Events
{
    public class EmployeeContactNoChanged : IDomainEvent
    {
        public long EmployeeId { get; set; }
        public string OldContactNo { get; set; }
        public string NewContactNo { get; set; }
    }
}