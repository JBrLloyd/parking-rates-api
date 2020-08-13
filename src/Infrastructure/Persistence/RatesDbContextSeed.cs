using Carpark.Register.Domain.Common;
using Carpark.Register.Domain.Entities;
using Carpark.Register.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpark.Register.Infrastructure.Persistence
{
    public static class RatesDbContextSeed
    {
        public static async Task SeedSampleSpecialRatesDataAsync(RatesDbContext context)
        {
            if (!context.SpecialRates.Any())
            {
                context.SpecialRates.Add(new SpecialRate
                {
                    Name = "Early Bird",
                    RateType = RateType.Flat,
                    Rate = 13.00,
                    EnterFrom = new DateTime(2000, 1, 1, 6, 0, 0),
                    EnterTo = new DateTime(2000, 1, 1, 9, 0, 0),
                    ExitFrom = new DateTime(2000, 1, 1, 15, 30, 0),
                    ExitTo = new DateTime(2000, 1, 1, 23, 30, 0),
                    ApplicableDaysOfWeek = new List<DayOfWeek>
                    {
                        DayOfWeek.Monday,
                        DayOfWeek.Tuesday,
                        DayOfWeek.Wednesday,
                        DayOfWeek.Thursday,
                        DayOfWeek.Friday,
                        DayOfWeek.Saturday,
                        DayOfWeek.Sunday
                    }
                });
                context.SpecialRates.Add(new SpecialRate
                {
                    Name = "Night Rate",
                    RateType = RateType.Flat,
                    Rate = 6.50,
                    EnterFrom = new DateTime(2000, 1, 1, 18, 0, 0),
                    EnterTo = new DateTime(2000, 1, 2, 0, 0, 0),
                    ExitFrom = new DateTime(2000, 1, 2, 15, 30, 0),
                    ExitTo = new DateTime(2000, 1, 1, 23, 30, 0),
                    ApplicableDaysOfWeek = new List<DayOfWeek>
                    {
                        DayOfWeek.Monday,
                        DayOfWeek.Tuesday,
                        DayOfWeek.Wednesday,
                        DayOfWeek.Thursday,
                        DayOfWeek.Friday,
                        DayOfWeek.Saturday,
                        DayOfWeek.Sunday
                    }
                });
                context.SpecialRates.Add(new SpecialRate
                {
                    Name = "Weekend Rate",
                    RateType = RateType.Flat,
                    Rate = 10.00,
                    EnterFrom = new DateTime(2000, 1, 1, 0, 0, 0),
                    EnterTo = new DateTime(2000, 1, 2, 23, 59, 59),
                    ExitFrom = new DateTime(2000, 1, 1, 0, 0, 0),
                    ExitTo = new DateTime(2000, 1, 2, 23, 59, 59),
                    ApplicableDaysOfWeek = new List<DayOfWeek>
                    {
                        DayOfWeek.Saturday,
                        DayOfWeek.Sunday
                    }
                });

                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedStandardRatesDataAsync(RatesDbContext context)
        {
            if (!context.StandardRates.Any())
            {
                context.StandardRates.Add(new StandardRate
                {
                    Name = "Standard Rate",
                    RateType = RateType.Hourly,
                    Rate = 5.00,
                    MaximumRate = 20.00
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
