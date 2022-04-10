using CalkooApi.DTOS;

namespace CalkooApi.Services.Strategy
{
    public class CalculationThroughPriceWithoutVat : ICalculator
    {
        public CalculationResponse Operations(PurchaseRequest purchaseRequest)
        {
            var valueTaxAdded = purchaseRequest.Amount * (purchaseRequest.Rate / 100);
            var priceIncVat = purchaseRequest.Amount + valueTaxAdded;

            return new CalculationResponse()
            {
                PriceIncludedVat = Math.Round(priceIncVat, 2),
                ValueAddedTax = Math.Round(valueTaxAdded, 2)
            };
        }
    }
}
