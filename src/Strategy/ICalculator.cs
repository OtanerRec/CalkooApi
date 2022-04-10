using CalkooApi.DTOS;

namespace CalkooApi.Services.Strategy
{
    public interface ICalculator
    {
        public CalculationResponse Operations(PurchaseRequest purchaseRequest);
    }
}
