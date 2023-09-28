using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{

    public class RebateServiceTests
    {
        [Fact]
        public void Calculate_WhenRebateIsNull_ReturnsResultWithFalseSuccess()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();
            var incentiveCalculatorsMock = new Mock<IIncentiveCalculators>();

            var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, incentiveCalculatorsMock.Object);

            rebateDataStoreMock.Setup(rds => rds.GetRebate(It.IsAny<string>())).Returns((Rebate)null);

            // Act
            var request = new CalculateRebateRequest();
            var result = rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_FixedCashAmount_ReturnsSuccess()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();
            var incentiveCalculatorsMock = new IncentiveCalculators();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "1",
                ProductIdentifier = "A",
                Volume = 500
            };
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 10m
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };
            var result = new CalculateRebateResult();
            rebateDataStoreMock.Setup(r => r.GetRebate(request.RebateIdentifier)).Returns(rebate);
            productDataStoreMock.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(product);
            var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, incentiveCalculatorsMock);

            // Act
            var calculationResult = rebateService.Calculate(request);

            // Assert
            Assert.True(calculationResult.Success);
        }

        [Fact]
        public void Calculate_FixedRateRebate_ReturnsSuccess()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();
            var incentiveCalculatorsMock = new IncentiveCalculators();

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "1",
                ProductIdentifier = "A",
                Volume = 500
            };
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 10m,
                Percentage = 0.05m
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 5
            };
            var result = new CalculateRebateResult();
            rebateDataStoreMock.Setup(r => r.GetRebate(request.RebateIdentifier)).Returns(rebate);
            productDataStoreMock.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(product);
            var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, incentiveCalculatorsMock);

            // Act
            var calculationResult = rebateService.Calculate(request);

            // Assert
            Assert.True(calculationResult.Success);
        }

        [Fact]
        public void Calculate_AmountPerUom_ReturnsSuccess()
        {
            // Arrange
            var rebateDataStoreMock = new Mock<IRebateDataStore>();
            var productDataStoreMock = new Mock<IProductDataStore>();
            var incentiveCalculatorsMock = new IncentiveCalculators();


            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "1",
                ProductIdentifier = "A",
                Volume = 500
            };
            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 10m
            };

            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            };
            var result = new CalculateRebateResult();
            rebateDataStoreMock.Setup(r => r.GetRebate(request.RebateIdentifier)).Returns(rebate);
            productDataStoreMock.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(product);
            var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, incentiveCalculatorsMock);

            // Act
            var calculationResult = rebateService.Calculate(request);

            // Assert
            Assert.True(calculationResult.Success);
        }

    }
}
