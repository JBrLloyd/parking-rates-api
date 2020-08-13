using FluentValidation;
using System;

namespace Carpark.Register.Application.ParkingRates.Queries.GetParkingRate
{
    public class GetParkingRateQueryValidator : AbstractValidator<GetParkingRateQuery>
    {
        public GetParkingRateQueryValidator()
        {
            RuleFor(x => x.EntryDateTime)
               .Must(BeAValidDate).WithMessage("Entry date is required");

            RuleFor(x => x.ExitDateTime)
               .Must(BeAValidDate).WithMessage("Exit date is required")
               .GreaterThan(x => x.EntryDateTime).WithMessage("Exit Date/Time must be after Entry Date/Time");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
