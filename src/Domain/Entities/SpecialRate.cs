using Carpark.Register.Domain.Common;
using Carpark.Register.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Carpark.Register.Domain.Entities
{
    public class SpecialRate : BaseRate
    {
        public DateTime EnterFrom { get; set; }
        public DateTime EnterTo { get; set; }
        public DateTime ExitFrom { get; set; }
        public DateTime ExitTo { get; set; }
        public ICollection<DayOfWeek> ApplicableDaysOfWeek { get; set; }
    }
}
