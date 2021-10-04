using System;
using System.Collections.Generic;
using Application.Components.HotelRates.Requests;
using Application.Components.HotelRates.Validators;
using Autofac.Extras.Moq;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.UnitTests.Components.HotelRates.Validators
{
    public class HotelRatesFilterRequestValidatorTests
    {
        private readonly HotelRatesFilterRequestValidator _validator;

        public HotelRatesFilterRequestValidatorTests()
        {
            var mock = AutoMock.GetLoose();
            _validator = mock.Create<HotelRatesFilterRequestValidator>();
        }

        public static IEnumerable<object[]> ValidRequestPayloads = new List<object[]>
        {
            new object[] {new HotelRatesFilterRequest {ArrivalDate = null, HotelId = null}},
            new object[] {new HotelRatesFilterRequest {ArrivalDate = DateTime.Today, HotelId = 7256}},
        };

        [Theory]
        [MemberData(nameof(ValidRequestPayloads))]
        public void Should_Be_Valid_When_Request_Payload_Is_Valid(HotelRatesFilterRequest request)
        {
            _validator
                .TestValidate(request, opt => opt.IncludeAllRuleSets())
                .IsValid
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Should_Be_Invalid_When_Hotel_Does_Not_Exist()
        {
            var request = new HotelRatesFilterRequest
            {
                ArrivalDate = DateTime.Today,
                HotelId = -5
            };

            _validator
                .TestValidate(request, opt => opt.IncludeAllRuleSets())
                .ShouldHaveValidationErrorFor(x => x.HotelId)
                .WithErrorCode(ErrorCodes.HOTEL_DOES_NOT_EXIST.ToString());
        }
    }
}