/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static ConcurrentBag<int> _sharedCollection;
        static Semaphore _semaphore;
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            _sharedCollection = new ConcurrentBag<int>();

            _semaphore = new Semaphore(1, 1);

            ThreadPool.QueueUserWorkItem(obj =>
            {
                _semaphore.WaitOne();

                foreach (var item in Enumerable.Range(0, (int)obj))
                {
                    _sharedCollection.Add(item);
                }

                _semaphore.Release();
            }, 10);

            Thread.Sleep(1);

            ThreadPool.QueueUserWorkItem(obj =>
            {
                _semaphore.WaitOne();

                foreach (var item in _sharedCollection)
                {
                    Console.WriteLine(item);
                }

                _semaphore.Release();
            });

            Console.ReadLine();
        }
    }
}
