using AutoMapper;
using Carpark.Register.Application.Common.Interfaces;
using Carpark.Register.Application.Common.Mappings;
using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using Carpark.Register.Application.Services;
using Carpark.Register.Domain.Entities;
using Carpark.Register.Domain.Enums;
using Carpark.Register.Infrastructure.Persistence;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTests.Services
{
    public class ParkingRateServiceTests
    {
        private readonly DbContextOptions<RatesDbContext> options;
        private readonly Mapper _mapper;
        private readonly Mock<ILogger<ParkingRateService>> _mockedLogger;

        public ParkingRateServiceTests()
        {
            options = new DbContextOptionsBuilder<RatesDbContext>()
                .UseInMemoryDatabase(databaseName: "Carpark.RegisterDb")
                .Options;

            _mockedLogger = new Mock<ILogger<ParkingRateService>>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new ParkingRateProfile())));
        }

        [Fact]
        public void GetCheapestSpecialRate_SaturdayToSunday_ReturnNightRate()
        {
            // Arrange
            var entered = new DateTime(2020, 8, 8, 8, 0, 0);
            var exited = new DateTime(2020, 8, 9, 17, 0, 0);

            using (var context = new RatesDbContext(options))
            {
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
                context.SaveChanges();

                var _mockedParkingRateService = new ParkingRateService(context, _mapper, _mockedLogger.Object);

                // Act
                var response = _mockedParkingRateService.GetCheapestSpecialRate(entered, exited);

                // Assert
                Assert.NotNull(response);
                Assert.IsType<GetParkingRateResponse>(response);
                Assert.Equal(10.00, response.Total);
            }
        }
        [Fact]
        public void GetCheapestSpecialRate_NonSpecialRequest_ReturnNoApplicableRates()
        {
            // Arrange
            var entered = new DateTime(2020, 8, 10, 8, 0, 0);
            var exited = new DateTime(2020, 8, 10, 17, 0, 0);

            using (var context = new RatesDbContext(options))
            {
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
                context.SaveChanges();

                var _mockedParkingRateService = new ParkingRateService(context, _mapper, _mockedLogger.Object);

                // Act
                var response = _mockedParkingRateService.GetCheapestSpecialRate(entered, exited);

                // Assert
                Assert.Null(response);
            }
        }
        [Fact]
        public void GetStandardRate_9HrDuration_ReturnMaxRate()
        {
            // Arrange
            var entered = new DateTime(2020, 8, 8, 8, 0, 0);
            var exited = new DateTime(2020, 8, 8, 17, 0, 0);

            using (var context = new RatesDbContext(options))
            {
                context.StandardRates.Add(new StandardRate
                {
                    Name = "Standard Rate",
                    RateType = RateType.Hourly,
                    Rate = 5.00,
                    MaximumRate = 20.00
                });
                context.SaveChanges();

                var _mockedParkingRateService = new ParkingRateService(context, _mapper, _mockedLogger.Object);

                // Act
                var response = _mockedParkingRateService.GetStandardRate(entered, exited);

                // Assert
                Assert.NotNull(response);
                Assert.IsType<GetParkingRateResponse>(response);
                Assert.Equal(20.00, response.Total);
            }
        }
        [Fact]
        public void GetStandardRate_2HrDuration_ReturnStandardRate()
        {
            // Arrange
            var entered = new DateTime(2020, 8, 8, 8, 0, 0);
            var exited = new DateTime(2020, 8, 8, 10, 0, 0);

            using (var context = new RatesDbContext(options))
            {
                context.StandardRates.Add(new StandardRate
                {
                    Name = "Standard Rate",
                    RateType = RateType.Hourly,
                    Rate = 5.00,
                    MaximumRate = 20.00
                });
                context.SaveChanges();

                var _mockedParkingRateService = new ParkingRateService(context, _mapper, _mockedLogger.Object);

                // Act
                var response = _mockedParkingRateService.GetStandardRate(entered, exited);

                // Assert
                Assert.NotNull(response);
                Assert.IsType<GetParkingRateResponse>(response);
                Assert.Equal(10.00, response.Total);
            }
        }
        [Fact]
        public void GetStandardRate_NoContextDbData_ThrowsNullException()
        {
            // Arrange
            var entered = new DateTime(2020, 8, 8, 8, 0, 0);
            var exited = new DateTime(2020, 8, 8, 10, 0, 0);

            using (var context = new RatesDbContext(options))
            {
                var _mockedParkingRateService = new ParkingRateService(context, _mapper, _mockedLogger.Object);

                // Act & Assert
                Assert.Throws<NullReferenceException>(() => _mockedParkingRateService.GetStandardRate(entered, exited));
            }
        }
    }
}
