using CalkooApi.DTOS;

namespace CalkooApi.Services.Strategy
{
    public class CalculationThroughValueTax : ICalculator
    {
        public CalculationResponse Operations(PurchaseRequest purchaseRequest)
        {
            var priceWithoutVat = purchaseRequest.Amount / (purchaseRequest.Rate / 100);
            var priceIncVat = purchaseRequest.Amount + priceWithoutVat;

            return new CalculationResponse()
            {
                PriceIncludedVat = Math.Round(priceIncVat, 2),
                PriceWithoutVat = Math.Round(priceWithoutVat, 2)
            };
        }
    }
}
