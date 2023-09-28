using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IIncentiveCalculators _incentiveCalculators;
    public RebateService(
        IRebateDataStore rebateDataStore,
        IProductDataStore productDataStore,
        IIncentiveCalculators incentiveCalculators
        )
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _incentiveCalculators = incentiveCalculators;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        decimal rebateAmount = 0m;
        if (rebate == null)
        {
            result.Success = false;
            return result;
        }
        switch (rebate.Incentive)
        {
            case IncentiveType.FixedCashAmount:
                rebateAmount = _incentiveCalculators.CalculateFixedCashAmount(rebate, product);
                break;

            case IncentiveType.FixedRateRebate:
                rebateAmount = _incentiveCalculators.CalculateFixedRateRebate(rebate, product, request);
                break;

            case IncentiveType.AmountPerUom:
                rebateAmount = _incentiveCalculators.CalculateAmountPerUom(rebate, product, request);
                break;

            default:
                result.Success = false;
                break;
        }

        if (rebateAmount > 0)
        {
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
            result.RebateAmount = rebateAmount;

            result.Success = true;
        }

        return result;
    }
}
