using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 

        //just for test
        switch (rebateIdentifier)
        {
            case "1":
                return new Rebate()
                {
                    Incentive = IncentiveType.FixedCashAmount,
                    Amount = 10m
                };
            case "2":
                return new Rebate()
                {
                    Incentive = IncentiveType.AmountPerUom,
                    Amount = 10m
                };
            case "3":
                return new Rebate()
                {
                    Incentive = IncentiveType.FixedRateRebate,
                    Amount = 10m
                };

            default:
                return new Rebate();
        }
        
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }
}
