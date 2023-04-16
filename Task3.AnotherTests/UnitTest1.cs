using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;
using Xunit;

namespace Task3.AnotherTests
{
    public class UnitTest1
    {
        [Fact]
        public void MultiplyMatrix3On3Test()
        {
            TestMatrix3On3(new MatricesMultiplier());
            TestMatrix3On3(new MatricesMultiplierParallel());
        }

        [Fact]
        public void ParallelEfficiencyTest()
        {
            var matrixSizes = new long[] { 5, 10, 100, 150, 200, 1000 };
            var measures = new Dictionary<long, (long Regular, long Parallel)>()
            {
                { 5, (0, 0) },
                { 10, (0, 0) },
                { 100, (0, 0) },
                { 150, (0, 0) },
                { 200, (0, 0) },
                { 1000, (0, 0) }
            };

            var regularMultiplier = new MatricesMultiplier();
            var parallelMultiplier = new MatricesMultiplierParallel();

            var stopwatch = new Stopwatch();
            foreach (var size in matrixSizes)
            {
                var m1 = new Matrix(size, size, true);
                var m2 = new Matrix(size, size, true);
                var currentMeasures = measures[size];

                stopwatch.Start();
                regularMultiplier.Multiply(m1, m2);
                stopwatch.Stop();
                currentMeasures.Regular = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                parallelMultiplier.Multiply(m1, m2);
                stopwatch.Stop();
                currentMeasures.Parallel = stopwatch.ElapsedMilliseconds;

                stopwatch.Reset();
            }

            (double regular, double parallel) = GetAverages(measures);

            Assert.True(parallel > regular, "Parallel multiplying is less efficient than regular one");
        }

        #region private methods

        void TestMatrix3On3(IMatricesMultiplier matrixMultiplier)
        {
            if (matrixMultiplier == null)
            {
                throw new ArgumentNullException(nameof(matrixMultiplier));
            }

            var m1 = new Matrix(3, 3);
            m1.SetElement(0, 0, 34);
            m1.SetElement(0, 1, 2);
            m1.SetElement(0, 2, 6);

            m1.SetElement(1, 0, 5);
            m1.SetElement(1, 1, 4);
            m1.SetElement(1, 2, 54);

            m1.SetElement(2, 0, 2);
            m1.SetElement(2, 1, 9);
            m1.SetElement(2, 2, 8);

            var m2 = new Matrix(3, 3);
            m2.SetElement(0, 0, 12);
            m2.SetElement(0, 1, 52);
            m2.SetElement(0, 2, 85);

            m2.SetElement(1, 0, 5);
            m2.SetElement(1, 1, 5);
            m2.SetElement(1, 2, 54);

            m2.SetElement(2, 0, 5);
            m2.SetElement(2, 1, 8);
            m2.SetElement(2, 2, 9);

            var multiplied = matrixMultiplier.Multiply(m1, m2);
            Assert.Equal(448, multiplied.GetElement(0, 0));
            Assert.Equal(1826, multiplied.GetElement(0, 1));
            Assert.Equal(3052, multiplied.GetElement(0, 2));

            Assert.Equal(350, multiplied.GetElement(1, 0));
            Assert.Equal(712, multiplied.GetElement(1, 1));
            Assert.Equal(1127, multiplied.GetElement(1, 2));

            Assert.Equal(109, multiplied.GetElement(2, 0));
            Assert.Equal(213, multiplied.GetElement(2, 1));
            Assert.Equal(728, multiplied.GetElement(2, 2));
        }

        (double RegularAverage, double ParallelAverage) GetAverages(Dictionary<long, (long Regular, long Parallel)> measures)
        {
            var regularMeasures = new List<long>();
            var parallelMeasures = new List<long>();

            foreach (var measure in measures.Values)
            {
                regularMeasures.Add(measure.Regular);
                parallelMeasures.Add(measure.Parallel);
            }

            return (regularMeasures.Average(), parallelMeasures.Average());
        }
        #endregion
    }
}