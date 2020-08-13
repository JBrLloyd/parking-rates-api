using Carpark.Register.Application.ParkingRates.Queries.GetParkingRate;
using FluentValidation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.ParkingRates.Queries.GetParkingRate
{
    public class GetParkingRateQueryValidatorTests : BaseValidatorTest
    {
        protected override IValidator Validator { get; set; }

        public GetParkingRateQueryValidatorTests()
        {
            Validator = new GetParkingRateQueryValidator();
        }

        [Fact]
        public async Task Validator_ExitAfterEntry_SuccessfulValidation()
        {
            // Arrange
            var request = new GetParkingRateQuery()
            {
                EntryDateTime = new DateTime(2000, 1, 1, 11, 0, 0),
                ExitDateTime = new DateTime(2000, 1, 2, 12, 0, 0)
            };

            // Act & Assert
            await ValidateValidObjectAsync(request);
        }
        [Fact]
        public async Task Validator_OneHourBefore_InvalidValidation()
        {
            // Arrange
            var request = new GetParkingRateQuery()
            {
                EntryDateTime = new DateTime(2000, 1, 1, 12, 0, 0),
                ExitDateTime = new DateTime(2000, 1, 1, 11, 0, 0)
            };

            // Act & Assert
            await ValidateInvalidPropertyAsync(request, nameof(GetParkingRateQuery.ExitDateTime));
        }
        [Fact]
        public async Task Validator_SameTime_InvalidValidation()
        {
            // Arrange
            var request = new GetParkingRateQuery()
            {
                EntryDateTime = new DateTime(2000, 1, 2, 12, 0, 0),
                ExitDateTime = new DateTime(2000, 1, 1, 12, 0, 0)
            };

            // Act & Assert
            await ValidateInvalidPropertyAsync(request, nameof(GetParkingRateQuery.ExitDateTime));
        }
        [Fact]
        public async Task Validator_DefaultEntryDate_InvalidValidation()
        {
            // Arrange
            var request = new GetParkingRateQuery()
            {
                EntryDateTime = new DateTime(),
                ExitDateTime = new DateTime(2000, 1, 1, 12, 0, 0)
            };

            // Act & Assert
            await ValidateInvalidPropertyAsync(request, nameof(GetParkingRateQuery.EntryDateTime));
        }

        [Fact]
        public async Task Validator__DefaultExitDate_InvalidValidation()
        {
            // Arrange
            var request = new GetParkingRateQuery()
            {
                EntryDateTime = new DateTime(2000, 1, 1, 12, 0, 0),
                ExitDateTime = new DateTime()
            };

            // Act & Assert
            await ValidateInvalidPropertyAsync(request, nameof(GetParkingRateQuery.ExitDateTime));
        }
    }
}
