using System;

namespace Aromato.Domain.Employee
{
    public class DutySchedule : IEntity<long>
    {
        public long Id { get; set; }

        public virtual string DayOfWeek { get; protected set; }
        public virtual DateTime StartTime { get; protected set; }
        public virtual DateTime EndTime { get; protected set; }
    }
}