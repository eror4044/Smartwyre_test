using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter rebate details:");
        string rebateId = Console.ReadLine();
        string productId = Console.ReadLine();

        // Створення інстанції RebateDataStore і ProductDataStore (можливо, вам потрібно використовувати реальні реалізації цих інтерфейсів)
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();
        var iniciativeCalculator = new IncentiveCalculators();
        CalculateRebateRequest request = new CalculateRebateRequest()
        {
            RebateIdentifier = rebateId,
            ProductIdentifier = productId,
            Volume = 500
        };

        // Передача інстанцій rebateDataStore і productDataStore в конструктор RebateService
        RebateService rebateService = new RebateService(rebateDataStore, productDataStore, iniciativeCalculator);

        var rebateAmount = rebateService.Calculate(request);

        Console.WriteLine($"Rebate Amount: {rebateAmount.RebateAmount}");
    }
}
