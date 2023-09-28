using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;

public class IncentiveCalculators : IIncentiveCalculators
{
    public decimal CalculateFixedCashAmount(Rebate rebate, Product product)
    {
        return product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) && rebate.Amount > 0
            ? rebate.Amount : 0m;
    }

    public decimal CalculateFixedRateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
            && (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            ? rebate.Amount += product.Price * rebate.Percentage * request.Volume : 0m;
    }

    public decimal CalculateAmountPerUom(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
            && (rebate.Amount == 0 || request.Volume == 0)
            ? rebate.Amount += rebate.Amount * request.Volume : 0m;
    }
}
