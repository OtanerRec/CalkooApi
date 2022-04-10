using CalkooApi.DTOS;
using CalkooApi.Validations;
using FluentAssertions;
using Xunit;

namespace CalkooApi.UnitTests.Validations
{
    public class RequestValidatorTests
    {
        private readonly RequestValidator validator;

        public RequestValidatorTests()
        {
            this.validator = new RequestValidator();
        }

        [Fact]
        public void RequestValidator_NotAllowAmountLowerThanZero_Error()
        {   
            //Arrange
            var request = new PurchaseRequest()
            {
                Country = "Austria",
                Amount = -1,
                Rate = 20,
                Type = DTOS.Enums.AumontType.Net
            };

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            Assert.Contains(result.Errors, o => o.PropertyName == "Amount");
        }

        [Fact]
        public void RequestValidator_InvalidVatRate_Error()
        {
            //Arrange
            var request = new PurchaseRequest()
            {
                Country = "Austria",
                Amount = -1,
                Rate = 60,
                Type = DTOS.Enums.AumontType.Net
            };

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            Assert.Contains(result.Errors, o => o.PropertyName == "Rate");
        }

        [Fact]
        public void RequestValidator_ValidRequest_ValidationSuccess()
        {
            //Arrange
            var request = new PurchaseRequest()
            {
                Country = "Austria",
                Amount = 10,
                Rate = 20,
                Type = DTOS.Enums.AumontType.Net
            };

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
