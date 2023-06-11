using AlteringBehavior.Models;

namespace AlteringBehavior.Interfaces
{
    public interface ICalculationStrategy
    {
        double Calculate(Order order);
    }
}
