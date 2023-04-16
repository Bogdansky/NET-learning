/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        const int InitialNumber = 10;
        static Semaphore _semaphore;

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
            Console.WriteLine("Choose the option.");
            var key = Console.ReadLine();

            if (key == "a")
            {
                ThreadClassApproach();
            }
            else if (key == "b")
            {
                ThreadPoolApproach();
            }
            Console.WriteLine("Threads work finished");
        }

        #region Thread class approach
        static void ThreadClassApproach()
        {
            Console.WriteLine("Initial number is {0}", InitialNumber);

            var firstThread = new Thread(DecrementNumberWithJoin);

            firstThread.Start(InitialNumber);
            firstThread.Join();
        }

        static void DecrementNumberWithJoin(object obj)
        {
            var number = (int)obj - 1;

            if (number == -1)
            {
                return;
            }

            Console.WriteLine(number);

            var thread = new Thread(DecrementNumberWithJoin);
            thread.Start(number);
            thread.Join();
        }
        #endregion

        #region Thread pool approach
        static void ThreadPoolApproach()
        {
            _semaphore = new Semaphore(1, 1);

            ThreadPool.QueueUserWorkItem(DecrementNumberWithSemaphore, InitialNumber);
            Thread.Sleep(100);
        }

        static void DecrementNumberWithSemaphore(object state)
        {
            _semaphore.WaitOne();
            var number = (int)state - 1;

            if (number == -1)
            {
                return;
            }

            Console.WriteLine(number);

            _semaphore.Release();

            ThreadPool.QueueUserWorkItem(DecrementNumberWithSemaphore, number);
        }
        #endregion
    }
}
