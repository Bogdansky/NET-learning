using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;
using AlteringBehavior.Models;

namespace AlteringBehavior.Strategies
{
    public class UPSCalculationStrategy : ICalculationStrategy
    {
        public double Calculate(Order order)
        {
            return order.Weight > 400 ? 4.25d * 1.1 : 4.25d;
        }
    }
}
