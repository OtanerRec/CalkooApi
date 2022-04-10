using CalkooApi.DTOS.Enums;

namespace CalkooApi.DTOS
{
    /*TODO: When this API grows, it'll be necessary to create a domain model and 
     * map the properties from dto to domain in order to expose just relevant properties to the client*/
    public class PurchaseRequest  
    {
        public string? Country { get; set; }

        public decimal Amount { get; set; }

        public decimal Rate { get; set; }

        public AumontType Type { get; set; }
    }
}
