using AutoMapper;
using Carpark.Register.Domain.Enums;

namespace Carpark.Register.Application.ParkingRates.Queries.GetParkingRate
{
    public class GetParkingRateResponse
    {
        public string Name { get; set; }
        public RateType RateType { get; set; }
        public double Rate { get; set; }
        public double Total { get; set; }
    }
}
