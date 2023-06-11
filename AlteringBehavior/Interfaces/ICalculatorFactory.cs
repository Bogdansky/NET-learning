using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlteringBehavior.Interfaces
{
    public interface ICalculatorFactory
    {
        ICalculator CreateCalculator();
        ICalculator CreateCachedCalculator();
        ICalculator CreateLoggingPaymentCalculator();
        ICalculator CreateRoundingPaymentCalculator();
        ICalculator CreateDynamicCalculator(bool isRounding, bool isLogging, bool isCachedLogging);
    }
}
