using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;

namespace AlteringBehavior.Calculators
{
    public class RoundingPaymentCalculator : ICalculator
    {
        private ICalculator calculator;

        public RoundingPaymentCalculator(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            var result = calculator.CalculatePayment(touristName);

            return Math.Round(result);
        }
    }
}
