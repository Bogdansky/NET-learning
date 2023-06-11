using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;
using AlteringBehavior.Models;
using AlteringBehavior.Strategies;

namespace AlteringBehavior.Calculators
{
    public class ShipmentCalculator
    {
        private ICalculationStrategy? calculationStrategy;

        public ShipmentCalculator(ICalculationStrategy calculationStrategy)
        {
            SetStrategy(calculationStrategy);
        }

        public void SetStrategy(ICalculationStrategy calculationStrategy)
        {
            this.calculationStrategy = calculationStrategy;
        }

        public double CalculatePrice(Order order)
        {
            if (calculationStrategy is null)
            {
                throw new FieldAccessException($"The {nameof(calculationStrategy)} is null.");
            }

            return calculationStrategy.Calculate(order);
        }
    }
}
