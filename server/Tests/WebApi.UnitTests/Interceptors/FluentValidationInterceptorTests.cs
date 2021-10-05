using System.Collections.Generic;
using Autofac.Extras.Moq;
using FluentAssertions;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using WebApi.Exceptions;
using WebApi.Interceptors;
using Xunit;

namespace WebApi.UnitTests.Interceptors
{
    public class FluentValidationInterceptorTests
    {
        private readonly IValidatorInterceptor _interceptor;

        public FluentValidationInterceptorTests()
        {
            var mock = AutoMock.GetLoose();
            _interceptor = mock.Create<FluentValidationInterceptor>();
        }

        [Fact]
        public void Should_Throw_FluentValidationException_After_Validation_Made()
        {
            var failures = new List<ValidationFailure>
            {
                new("testProp", "should not be less than 0, maybe")
            };

            _interceptor
                .Invoking(interceptor => interceptor.AfterAspNetValidation(null, null, new ValidationResult(failures)))
                .Should()
                .ThrowExactly<FluentValidationException>()
                .And
                .Errors
                .Should()
                .BeEquivalentTo(failures);
        }

        [Fact]
        public void Should_Return_ValidationResult_After_Validation_Made()
        {
            var result = new ValidationResult();

            _interceptor
                .AfterAspNetValidation(null, null, result)
                .Should()
                .BeEquivalentTo(result);
        }
    }
}