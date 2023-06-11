using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Calculators;
using AlteringBehavior.Interfaces;
using AlteringBehavior.Services;

namespace AlteringBehavior
{
    public class CalculatorFactory : ICalculatorFactory
    {
        public ICalculator CreateCalculator()
        {
            var currencyService = new KzCurrencyService();
            var repositoryService = new TemporaryTripRepository();

            return new InsurancePaymentCalculator(currencyService, repositoryService);
        }

        public ICalculator CreateCachedCalculator()
        {
            var calculator = CreateCalculator();
            var cache = new CacheService();

            return new CachedPaymentCalculator(calculator, cache);
        }

        public ICalculator CreateLoggingPaymentCalculator()
        {
            var calculator = CreateCalculator();
            var logger = new LoggingService();

            return new LoggingPaymentCalculator(logger, calculator);
        }

        public ICalculator CreateRoundingPaymentCalculator()
        {
            var calculator = CreateCalculator();

            return new RoundingPaymentCalculator(calculator);
        }

        public ICalculator CreateCachedCalculator(ICalculator calculator)
        {
            var cache = new CacheService();

            return new CachedPaymentCalculator(calculator, cache);
        }

        public ICalculator CreateLoggingPaymentCalculator(ICalculator calculator)
        {
            var logger = new LoggingService();

            return new LoggingPaymentCalculator(logger, calculator);
        }

        public ICalculator CreateRoundingPaymentCalculator(ICalculator calculator)
        {
            return new RoundingPaymentCalculator(calculator);
        }

        public ICalculator CreateDynamicCalculator(bool isRounding, bool isLogging, bool isCachedLogging)
        {
            var calculator = CreateCalculator();

            if (isRounding)
            {
                calculator = CreateRoundingPaymentCalculator(calculator);
            }
            if (isLogging)
            {
                calculator = CreateLoggingPaymentCalculator(calculator);
            }
            if (isCachedLogging)
            {
                calculator = CreateCachedCalculator(calculator);
            }

            return calculator;
        }
    }
}
