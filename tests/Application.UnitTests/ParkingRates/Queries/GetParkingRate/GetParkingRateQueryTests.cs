using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using Carpark.Register.Application.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.ParkingRates.Queries.GetParkingRate
{
    public class GetParkingRateQueryTests
    {

        [Fact]
        public async Task Handle_ValidRequest_ReturnsStandardRate()
        {
            // Arrange
            var request = new GetParkingRateQuery()
            {
                EntryDateTime = new DateTime(2000, 1, 1, 11, 0, 0),
                ExitDateTime = new DateTime(2000, 1, 2, 12, 0, 0)
            };

            var standardParkingRateResponse = new GetParkingRateResponse
            {
                Rate = 10.00,
                Total = 10.00
            };

            var parkingRateService = new Mock<IParkingRateService>();
            parkingRateService.Setup(x => x.GetCheapestSpecialRate(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<GetParkingRateResponse>(null);
            parkingRateService.Setup(x => x.GetStandardRate(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(standardParkingRateResponse);

            var GetParkingRateQueryHandler = new GetParkingRateQueryHandler(parkingRateService.Object);

            var response = await GetParkingRateQueryHandler.Handle(request, default);

            Assert.Equal(standardParkingRateResponse.Total, response.Total);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSpecialRate()
        {
            // Arrange
            var request = new GetParkingRateQuery()
            {
                EntryDateTime = new DateTime(2000, 1, 1, 11, 0, 0),
                ExitDateTime = new DateTime(2000, 1, 2, 12, 0, 0)
            };

            var speacialParkingRateResponse = new GetParkingRateResponse
            {
                Rate = 13.00,
                Total = 13.00
            };

            var parkingRateService = new Mock<IParkingRateService>();
            parkingRateService.Setup(x => x.GetCheapestSpecialRate(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(speacialParkingRateResponse);

            var GetParkingRateQueryHandler = new GetParkingRateQueryHandler(parkingRateService.Object);

            var response = await GetParkingRateQueryHandler.Handle(request, default);

            Assert.Equal(speacialParkingRateResponse.Total, response.Total);
        }
    }
}
