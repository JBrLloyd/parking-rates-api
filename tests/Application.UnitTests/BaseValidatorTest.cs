using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests
{
    public abstract class BaseValidatorTest
    {
        protected abstract IValidator Validator { get; set; }

        protected async Task ValidateInvalidPropertyAsync<T>(T request, string propertyName, Action verifications = null)
        {
            // Act
            var validationResult = await Validator.ValidateAsync(request);

            // Assert
            Assert.False(validationResult.IsValid);

            // Act
            var propertyError = validationResult.Errors.FirstOrDefault(
                x => x.PropertyName.Split(".").LastOrDefault() == propertyName || x.PropertyName.Contains($"{propertyName}["));

            // Assert
            verifications?.Invoke();
            Assert.NotNull(propertyError);
        }

        protected async Task ValidateValidObjectAsync<T>(T request, Action verifications = null)
        {
            // Act
            var validationResult = await Validator.ValidateAsync(request);

            // Assert
            verifications?.Invoke();
            Assert.True(validationResult.IsValid);
        }
    }
}
