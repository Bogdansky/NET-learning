using NUnit.Framework;

namespace AlteringBehavior.Tests
{
    public class Tests
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private CalculatorFactory calculatorFactory;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        [SetUp]
        public void Setup()
        {
            calculatorFactory = new CalculatorFactory();
        }

        [Test]
        public void Test1()
        {
            var calculator = calculatorFactory.CreateDynamicCalculator(true, true, true);
            _ = calculator.CalculatePayment("Nick");
            Assert.Pass();
        }
    }
}