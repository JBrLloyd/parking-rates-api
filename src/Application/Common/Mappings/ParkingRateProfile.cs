using AutoMapper;
using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using Carpark.Register.Domain.Entities;

namespace Carpark.Register.Application.Common.Mappings
{
    public class ParkingRateProfile : Profile
    {
        public ParkingRateProfile()
        {
            CreateMap<StandardRate, GetParkingRateResponse>();
            CreateMap<SpecialRate, GetParkingRateResponse>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Rate));
        }
    }
}
