using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;

namespace AlteringBehavior.Calculators
{
    public class LoggingPaymentCalculator : ICalculator
    {
        private ICalculator calculator;
        private ILogger logger;

        public LoggingPaymentCalculator(ILogger logger, ICalculator calculator)
        {
            this.logger = logger;
            this.calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            logger.Log("Start");

            var result = calculator.CalculatePayment(touristName);

            logger.Log("End");

            return result;
        }
    }
}
