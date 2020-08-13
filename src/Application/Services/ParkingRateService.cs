using AutoMapper;
using AutoMapper.QueryableExtensions;
using Carpark.Register.Application.Common.Interfaces;
using Carpark.Register.Application.Constants;
using Carpark.Register.Application.Extensions;
using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using Carpark.Register.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Carpark.Register.Application.Services
{
    public class ParkingRateService : IParkingRateService
    {
        private readonly IRatesDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ParkingRateService> _logger;

        public ParkingRateService(IRatesDbContext context, IMapper mapper, ILogger<ParkingRateService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public GetParkingRateResponse GetCheapestSpecialRate(DateTime entered, DateTime exited)
        {
            var stayDuration = GetTimeSpan(entered, exited);

            _logger.LogDebug("Retrieving applicable special rates");
            // Get Special Rates if they match the time range of the parking, applied days
            // and that the duration of the special rate entry and exit conditions.
            var applicableSpecialRates = _context.SpecialRates
                .Where(x => entered.TimeBetween(x.EnterFrom, x.EnterTo))
                .Where(x => exited.TimeBetween(x.ExitFrom, x.ExitTo))
                .Where(x => x.ApplicableDaysOfWeek.Contains(entered.DayOfWeek))
                .Where(x => x.ApplicableDaysOfWeek.Contains(exited.DayOfWeek))
                .Where(x => stayDuration <= GetTimeSpan(x.EnterFrom, x.ExitTo));

            if (applicableSpecialRates.Any())
            {
                _logger.LogDebug("Special rate criteria met. Getting best priced rate");
                return LowestSpecialRate(applicableSpecialRates);
            }

            return null;
        }
        private GetParkingRateResponse LowestSpecialRate(IQueryable<SpecialRate> applicableSpecialRates)
        {
            var lowestPrice = applicableSpecialRates.Min(r => r.Rate);
            var lowestRate = applicableSpecialRates
                .Where(r => r.Rate == lowestPrice)
                .ProjectTo<GetParkingRateResponse>(_mapper.ConfigurationProvider)
                .First();

            return lowestRate;
        }

        public GetParkingRateResponse GetStandardRate(DateTime entered, DateTime exited)
        {
            var stayDuration = GetTimeSpan(entered, exited);
            
            _logger.LogDebug("Retrieving latest standard rate");
            var latestStandardRate = _context.StandardRates.LastOrDefault();
            var standardRate = _mapper.Map<GetParkingRateResponse>(latestStandardRate);

            var maximumParkingDurationHours = (latestStandardRate.MaximumRate - latestStandardRate.Rate) / latestStandardRate.Rate;

            _logger.LogDebug("Calculating Standard Rate Total price");
            if (stayDuration.Days >= ParkingRateConstants.OneDay)
            {
                standardRate.Total = Math.Ceiling(stayDuration.TotalDays) * latestStandardRate.MaximumRate;
            }
            else if (stayDuration.RoundedUpHours() >= maximumParkingDurationHours)
            {
                standardRate.Total = latestStandardRate.MaximumRate;
            }
            else
            {
                standardRate.Total = stayDuration.RoundedUpHours() * latestStandardRate.Rate;
            }

            return standardRate;
        }

        private TimeSpan GetTimeSpan(DateTime start, DateTime end)
        {
            var timespan = end.Subtract(start);
            return timespan;
        }
    }
}
