using System.Text;

namespace AlteringBehavior
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new CalculatorFactory();
            var calculator = factory.CreateDynamicCalculator(true, true, true);
            calculator.CalculatePayment("Nick");
        }
    }
}