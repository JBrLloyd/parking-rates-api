using AutoMapper;
using Carpark.Register.Application.Common.Mappings;
using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using Carpark.Register.Domain.Entities;
using System;
using Xunit;

namespace Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}
