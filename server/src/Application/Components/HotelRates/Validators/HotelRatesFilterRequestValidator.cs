using Application.Components.HotelRates.Requests;
using Autofac.Extras.RegistrationAttributes.RegistrationAttributes;
using FluentValidation;

namespace Application.Components.HotelRates.Validators
{
    [As(typeof(IValidator<HotelRatesFilterRequest>))]
    public class HotelRatesFilterRequestValidator : AbstractValidator<HotelRatesFilterRequest>
    {
        public HotelRatesFilterRequestValidator()
        {
            When(r => r.HotelId.HasValue, () =>
            {
                RuleFor(r => r.HotelId)
                    .GreaterThan(0)
                    .WithErrorCode(ErrorCodes.HOTEL_DOES_NOT_EXIST);
            });
        }
    }
}