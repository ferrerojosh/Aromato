using System;

namespace Aromato.Domain.EmployeeAgg
{
    public class DutySchedule : IEntity<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected DutySchedule()
        {
        }

        public static DutySchedule Create(string dayOfWeek, DateTime startTime, DateTime endTime)
        {
            return new DutySchedule
            {
                DayOfWeek = dayOfWeek,
                StartTime = startTime,
                EndTime = endTime
            };
        }

        public long Id { get; set; }
        public virtual string DayOfWeek { get; protected set; }
        public virtual DateTime StartTime { get; protected set; }
        public virtual DateTime EndTime { get; protected set; }
    }
}