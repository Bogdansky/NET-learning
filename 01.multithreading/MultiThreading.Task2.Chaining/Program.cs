/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static Random random;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code
            random = new Random();

            var task1 = Task.Factory.StartNew(GenerateRandomArray);
            var continuation = task1.ContinueWith((arrTask) => 
            {
                Console.WriteLine("Multiply with random integer:");

                MultiplyWithRandom(arrTask.Result); 
                return arrTask.Result;
            })
            .ContinueWith(arrTask =>
            {
                Console.WriteLine("Sort array:");

                Array.Sort(arrTask.Result);
                OutputArray(arrTask.Result);

                return arrTask.Result;
            })
            .ContinueWith(arrTask =>
            {
                Console.WriteLine("Average:");
                Console.WriteLine(arrTask.Result.Average());
            });
            continuation.Wait();
            Console.ReadLine();
        }

        static int[] GenerateRandomArray()
        {
            Console.WriteLine("Random array:");
            var array = new int[10];

            for(var i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, 255);
            }
            OutputArray(array);

            return array;
        }

        static void MultiplyWithRandom(int[] origin)
        {
            var randomInteger = random.Next(0, 255);

            Console.WriteLine("Random integer is {0}", randomInteger);

            for(var i = 0; i < origin.Length; i++)
            {
                origin[i] *= randomInteger;
            }
            OutputArray(origin);
        }

        static void OutputArray(int[] array)
        {
            Array.ForEach(array, element => Console.Write("{0} ", element));
            Console.WriteLine();
        }
    }
}
