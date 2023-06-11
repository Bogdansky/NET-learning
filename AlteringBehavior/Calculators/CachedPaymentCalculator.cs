using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;
using AlteringBehavior.Services;

namespace AlteringBehavior.Calculators
{
    public class CachedPaymentCalculator : ICalculator
    {
        private readonly ICalculator calculator;
        private readonly CacheService cache;

        public CachedPaymentCalculator(ICalculator calculator, CacheService cache)
        {
            this.calculator = calculator;
            this.cache = cache;
        }

        public decimal CalculatePayment(string touristName)
        {
            var isInCache = cache.Get(touristName, out var result);
            if (isInCache) return result;

            result = calculator.CalculatePayment(touristName);
            cache.AddToCache(touristName, result);

            return result;
        }
    }
}
