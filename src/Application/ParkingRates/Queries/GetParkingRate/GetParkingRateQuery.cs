using Carpark.Register.Application.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Carpark.Register.Application.ParkingRates.Queries.GetParkingRate
{
    public class GetParkingRateQuery : IRequest<GetParkingRateResponse>
    {
        /// <summary>
        /// Datetime of park entry
        /// </summary>
        /// <example>2020-08-12T06:46:58.437Z</example>
        public DateTime EntryDateTime { get; set; }
        /// <summary>
        /// Datetime of park exit
        /// </summary>
        /// <example>2020-08-12T15:46:58.437Z</example>
        public DateTime ExitDateTime { get; set; }
    }

    public class GetParkingRateQueryHandler : IRequestHandler<GetParkingRateQuery, GetParkingRateResponse>
    {
        private readonly IParkingRateService _parkingRateService;

        public GetParkingRateQueryHandler(IParkingRateService parkingRateService)
        {
            _parkingRateService = parkingRateService;
        }

        public async Task<GetParkingRateResponse> Handle(GetParkingRateQuery request, CancellationToken cancellationToken)
        {
            var specialRate = _parkingRateService.GetCheapestSpecialRate(request.EntryDateTime, request.ExitDateTime);

            if (specialRate != null)
            {
                return specialRate;
            }

            var standardRate = _parkingRateService.GetStandardRate(request.EntryDateTime, request.ExitDateTime);

            return standardRate;
        }
    }
}
