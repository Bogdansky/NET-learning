/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code
            Console.WriteLine("Choose an option.");
            var key = Console.ReadLine();
            var currentMinute = DateTime.Now.Minute;

            switch (key)
            {
                case "a":
                    FirstCase(currentMinute); break;
                case "b":
                    SecondCase(currentMinute); break;
                case "c":
                    ThirdCase(); break;
                case "d":
                    ForthCase(); break;
                default: break;
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Continuation task should be executed regardless of the result of the parent task.
        /// </summary>
        static void FirstCase(int number)
        {
            var task = Task.Factory.StartNew(state =>
            {
                if (number % 2 == 0)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Fail");
                    throw new Exception();
                }
            }, number)
            .ContinueWith(antecedent =>
            {
                var previousTaskResult = antecedent.IsFaulted ? "failed" : "completed";
                Console.WriteLine("Continuation is called and previous task was {0}.", previousTaskResult);
            });

            task.Wait();
        }

        /// <summary>
        /// Continuation task should be executed when the parent task finished without success.
        /// </summary>
        static void SecondCase(int number)
        {
            try
            {
                var task = Task.Factory.StartNew(state =>
                {
                    if ((int)state % 2 == 0)
                    {
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("Fail");
                        throw new Exception();
                    }
                }, number)
                .ContinueWith(antecedent =>
                {
                    Console.WriteLine("Continuation is called when parent task was faulted");
                }, TaskContinuationOptions.OnlyOnFaulted);

                task.Wait();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.
        /// </summary>
        static void ThirdCase()
        {
            var task = CreateTaskForThirdCase();

            task.ConfigureAwait(false);

            task.ContinueWith(antecedent =>
            {
                Console.WriteLine("Thread name is {0}", Thread.CurrentThread.Name);
                Console.WriteLine("Parent finished with fail");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously)
            .Wait();
        }

        static Task CreateTaskForThirdCase()
        {
            Thread.Sleep(100);
            return Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.Name = "Thread1";
                Console.WriteLine("Thread name is {0}", Thread.CurrentThread.Name);
                throw new Exception();
            });
        }

        /// <summary>
        /// Continuation task should be executed outside of the thread pool when the parent task would be cancelled.
        /// </summary>
        static async void ForthCase()
        {
            var taskSource = new CancellationTokenSource();
            var cancellationToken = taskSource.Token;

            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Thread id is {0}", Thread.CurrentThread.ManagedThreadId);
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, cancellationToken);

            var continuation = task.ContinueWith(antecedent =>
            {
                Console.WriteLine("Thread id is {0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Continuation is called when parent task was cancelled");
            }, TaskContinuationOptions.OnlyOnCanceled);

            taskSource.Cancel();

            await continuation;
        }
    }
}
