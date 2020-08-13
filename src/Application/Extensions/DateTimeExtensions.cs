using System;

namespace Carpark.Register.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool TimeBetween(this DateTime dateTime, DateTime from, DateTime to)
        {
            var fromTime = TimeSpan.Parse(from.ToString("HH:mm:ss.fff"));
            var toTime = TimeSpan.Parse(to.ToString("HH:mm:ss.fff"));
            var timeToCheck = TimeSpan.Parse(dateTime.ToString("HH:mm:ss.fff"));

            var isBetween = (timeToCheck >= fromTime) && (timeToCheck < toTime);
            return isBetween;
        }
    }
}
