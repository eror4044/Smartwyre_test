using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public Product GetProduct(string productIdentifier)
    {
        switch (productIdentifier)
        {
            case "A":
                return new Product
                {
                    SupportedIncentives = SupportedIncentiveType.FixedCashAmount
                };
            case "B":
                return new Product
                {
                    SupportedIncentives = SupportedIncentiveType.AmountPerUom
                };
            case "C":
                return new Product 
                {
                    SupportedIncentives = SupportedIncentiveType.FixedRateRebate 
                };

            default:
                return new Product();
        }
    }
}
