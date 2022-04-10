using CalkooApi.DTOS;

namespace CalkooApi.Services.Strategy
{
    public class CalculationThroughPriceIncludedVat : ICalculator
    {
        public CalculationResponse Operations(PurchaseRequest purchaseRequest)
        {
            var vat = this.GetVatBasedOnRate(purchaseRequest.Rate);
            var valueTaxAdded = purchaseRequest.Amount * vat;
            var priceWithoutVat = purchaseRequest.Amount - valueTaxAdded;

            return new CalculationResponse()
            {
                PriceWithoutVat = Math.Round(priceWithoutVat, 2),
                ValueAddedTax = Math.Round(valueTaxAdded, 2)
            };
        }

        private decimal GetVatBasedOnRate(decimal vatRate)
        {
            return vatRate switch
            {
                10.0m => 0.090909m,
                13.0m => 0.115044m,
                20.0m => 0.166667m,
                _ => 0.0m,
            };
        }
    }
}
