using System;

namespace Carpark.Register.Application.Extensions
{
    public static class TimeSpanExtensions
    {
        public static int RoundedUpHours(this TimeSpan timeSpan)
        {
            return (int)Math.Ceiling(timeSpan.TotalHours);
        }
    }
}
