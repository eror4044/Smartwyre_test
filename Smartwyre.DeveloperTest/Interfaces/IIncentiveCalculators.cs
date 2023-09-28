using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Interfaces
{
    public interface IIncentiveCalculators
    {
        decimal CalculateFixedCashAmount(Rebate rebate, Product product);
        decimal CalculateFixedRateRebate(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal CalculateAmountPerUom(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
