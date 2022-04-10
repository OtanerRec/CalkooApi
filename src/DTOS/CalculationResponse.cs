namespace CalkooApi.DTOS
{
    public class CalculationResponse
    {
        public decimal? PriceWithoutVat { get; set; }

        public decimal? ValueAddedTax { get; set; }

        public decimal? PriceIncludedVat { get; set; }
    }
}
