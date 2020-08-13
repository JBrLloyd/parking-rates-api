using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using System;

namespace Carpark.Register.Application.Services
{
    public interface IParkingRateService
    {
        GetParkingRateResponse GetCheapestSpecialRate(DateTime entered, DateTime exited);
        GetParkingRateResponse GetStandardRate(DateTime entered, DateTime exited);
    }
}