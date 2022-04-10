using AutoFixture;
using CalkooApi.DTOS;
using CalkooApi.Services.Implementations;
using CalkooApi.Services.Strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CalkooApi.UnitTests.Strategy
{
    public class CalculationServiceTests
    {
        private readonly Fixture fixture;
        private readonly CalculationService calculationService;
        private readonly Mock<ICalculator> calculatorMock;

        public CalculationServiceTests()
        {
            this.fixture = new Fixture();
            this.calculatorMock = new Mock<ICalculator>();

            this.calculationService = new CalculationService(calculatorMock.Object);
        }

        [Fact]
        public void Calculate_BasedOnPriceIncludedVat_ReturnExceptedResult()
        {
            //Arrange 
            var request = new PurchaseRequest()
            {
                Amount = 100,
                Country = "Austria",
                Rate = 20,
                Type = DTOS.Enums.AumontType.Gross
            };

            var response = new CalculationResponse()
            {
                PriceIncludedVat = null,
                PriceWithoutVat = 83.33m,
                ValueAddedTax = 16.67m
            };

            this.calculatorMock.Setup(x => x.Operations(It.IsAny<PurchaseRequest>())).Returns(response);    

            //Act
            var result = this.calculationService.Calculate(request);

            //Assert
            Assert.AreEqual(response, result);
            this.calculatorMock.Verify(x => x.Operations(It.IsAny<PurchaseRequest>()), Times.Once);
        }

        [Fact]
        public void Calculate_BasedOnPriceWithoutVat_ReturnExceptedResult()
        {
            //Arrange 
            var request = new PurchaseRequest()
            {
                Amount = 100,
                Country = "Austria",
                Rate = 20,
                Type = DTOS.Enums.AumontType.Net
            };

            var response = new CalculationResponse()
            {
                PriceIncludedVat = 120.00m,
                PriceWithoutVat = null,
                ValueAddedTax = 20m
            };

            this.calculatorMock.Setup(x => x.Operations(It.IsAny<PurchaseRequest>())).Returns(response);

            //Act
            var result = this.calculationService.Calculate(request);

            //Assert
            Assert.AreEqual(response, result);
            this.calculatorMock.Verify(x => x.Operations(It.IsAny<PurchaseRequest>()), Times.Once);
        }

        [Fact]
        public void Calculate_BasedOnValueTax_ReturnExceptedResult()
        {
            //Arrange 
            var request = new PurchaseRequest()
            {
                Amount = 100,
                Country = "Austria",
                Rate = 20,
                Type = DTOS.Enums.AumontType.Vat
            };

            var response = new CalculationResponse()
            {
                PriceIncludedVat = 600.00m,
                PriceWithoutVat = null,
                ValueAddedTax = 500.00m
            };

            this.calculatorMock.Setup(x => x.Operations(It.IsAny<PurchaseRequest>())).Returns(response);

            //Act
            var result = this.calculationService.Calculate(request);

            //Assert
            Assert.AreEqual(response, result);
            this.calculatorMock.Verify(x => x.Operations(It.IsAny<PurchaseRequest>()), Times.Once);
        }
    }
}
