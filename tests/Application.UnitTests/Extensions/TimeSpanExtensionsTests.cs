using Carpark.Register.Application.Extensions;
using System;
using Xunit;

namespace Application.UnitTests.Extensions
{
    public class TimeSpanExtensionsTests
    {
        [Fact]
        public void RoundedUpHours_Initalized_ReturnsZero()
        {
            // Arrange
            var timeSpan = new TimeSpan();

            // Act
            var result = timeSpan.RoundedUpHours();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void RoundedUpHours_NegativeTicks_ReturnsZero()
        {
            // Arrange
            var timeSpan = new TimeSpan(-20);

            // Act
            var result = timeSpan.RoundedUpHours();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void RoundedUpHours_HourHalf_ReturnsTwoHours()
        {
            // Arrange
            var timeSpan = new TimeSpan(0, 1, 30, 0, 0);

            // Act
            var result = timeSpan.RoundedUpHours();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void RoundedUpHours_OneDayHourHalf_ReturnsTwoHours()
        {
            // Arrange
            var timeSpan = new TimeSpan(1, 1, 30, 0, 0);

            // Act
            var result = timeSpan.RoundedUpHours();

            // Assert
            Assert.Equal(24 + 2, result);
        }
    }
}
