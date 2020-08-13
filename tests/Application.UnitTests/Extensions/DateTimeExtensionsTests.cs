using Carpark.Register.Application.Extensions;
using System;
using Xunit;

namespace Application.UnitTests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void Between_TimeBetween_ShouldReturnTrue()
        {
            // Arrange
            var datetime = new DateTime(2000, 1, 3, 12, 0 ,0);
            var fromDatetime = new DateTime(2000, 1, 1, 8, 0 ,0);
            var toDatetime = new DateTime(2000, 1, 2, 16, 0, 0);

            // Act
            var result = datetime.TimeBetween(fromDatetime, toDatetime);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Between_TimeNotBetween_ShouldReturnFalse()
        {
            // Arrange
            var datetime = new DateTime(2000, 1, 3, 20, 0, 0);
            var fromDatetime = new DateTime(2000, 1, 1, 8, 0, 0);
            var toDatetime = new DateTime(2000, 1, 2, 16, 0, 0);

            // Act
            var result = datetime.TimeBetween(fromDatetime, toDatetime);

            // Assert
            Assert.False(result);
        }
    }
}
