using NUnit.Framework;

namespace AlteringBehavior.Tests
{
    public class Tests
    {
#pragma warning disable CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
        private CalculatorFactory calculatorFactory;
#pragma warning restore CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
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