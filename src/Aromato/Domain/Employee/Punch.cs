﻿using System;

namespace Aromato.Domain.Employee
{
    public class Punch : IEntity<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Punch()
        {
            DateAdded = DateTime.Now;
        }

        public long Id { get; set; }
        public PunchType Type { get; protected set; }
        public DateTime DateAdded { get; protected set; }

    }
}