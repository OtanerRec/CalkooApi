using CalkooApi.DTOS;

namespace CalkooApi.Services.Interfaces
{
    public interface ICalculationService
    {
        public CalculationResponse Calculate(PurchaseRequest purchaseRequest);
    }
}
