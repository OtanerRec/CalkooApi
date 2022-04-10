using CalkooApi.DTOS;
using CalkooApi.Services.Strategy;
using CalkooApi.Services.Interfaces;

namespace CalkooApi.Services.Implementations
{
    public class CalculationService : ICalculationService
    {
        private readonly ICalculator calculator;

        public CalculationService(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public CalculationResponse Calculate(PurchaseRequest purchaseRequest)
        {
            return calculator.Operations(purchaseRequest);  
        }
    }
}
