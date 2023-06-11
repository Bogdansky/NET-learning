using NUnit.Framework;
using AlteringBehavior.Calculators;
using AlteringBehavior.Enums;
using AlteringBehavior.Models;
using AlteringBehavior.Strategies;

namespace GOF.Altering_behavior._Tests
{
    internal class StrategyTesting
    {
        [Test]
        public void FedExStrategyTest()
        {
            var fedExStrategy = new FedExCalculationStrategy();
            var calculator = new ShipmentCalculator(fedExStrategy);
            var order = new Order(ShipmentOptions.FedEx, ProductType.Electronic, 400);

            Assert.IsTrue(fedExStrategy.Calculate(order) == calculator.CalculatePrice(order));
        }

        [Test]
        public void SeveralStrategiesTest()
        {
            var orders = new Dictionary<ShipmentOptions, Order>()
            {
                {ShipmentOptions.FedEx, new Order(ShipmentOptions.FedEx, ProductType.Electronic, 400) },
                {ShipmentOptions.UPS, new Order(ShipmentOptions.UPS, ProductType.Book, 150) },
                {ShipmentOptions.USPS, new Order(ShipmentOptions.USPS, ProductType.Book, 23) }
            };

            var fedExStrategy = new FedExCalculationStrategy();
            var calculator = new ShipmentCalculator(fedExStrategy);

            Assert.IsTrue(fedExStrategy.Calculate(orders[ShipmentOptions.FedEx]) == calculator.CalculatePrice(orders[ShipmentOptions.FedEx]));

            var upsStrategy = new UPSCalculationStrategy();
            calculator.SetStrategy(upsStrategy);

            Assert.IsTrue(upsStrategy.Calculate(orders[ShipmentOptions.UPS]) == calculator.CalculatePrice(orders[ShipmentOptions.UPS]));

            var uspsStrategy = new USPSCalculationStrategy();
            calculator.SetStrategy(uspsStrategy);

            Assert.IsTrue(uspsStrategy.Calculate(orders[ShipmentOptions.USPS]) == calculator.CalculatePrice(orders[ShipmentOptions.USPS]));
        }
    }
}
