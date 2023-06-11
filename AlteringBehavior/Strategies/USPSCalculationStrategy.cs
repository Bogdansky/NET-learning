using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Enums;
using AlteringBehavior.Interfaces;
using AlteringBehavior.Models;

namespace AlteringBehavior.Strategies
{
    public class USPSCalculationStrategy : ICalculationStrategy
    {
        public double Calculate(Order order)
        {
            return order.Product == ProductType.Book ? 3.00d * 0.9 : 3.00d;
        }
    }
}
