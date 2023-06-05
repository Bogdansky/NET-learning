/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 2;
        const int MaxIterationsCount = 10;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();
            
            HundredTasks();

            Console.ReadLine();
        }

        static void HundredTasks()
        {
            // feel free to add your code here
            var tasks = new Task[TaskAmount];
            for(var index = 1; index < TaskAmount + 1; index++)
            {
                var task = Task.Factory.StartNew((object obj) =>
                {
                    var currentIndex = (obj as TaskData).Number;
                    for(var iteration = 1; iteration < MaxIterationsCount + 1; iteration++)
                    {
                        Output(currentIndex, iteration);
                    }
                }, new TaskData(index));

                tasks[index - 1] = task;
            }

            Task.WaitAll(tasks);
            Console.WriteLine("All tasks finished.");
        }

        static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }

        class TaskData
        {
            public int Number { get; }

            public TaskData(int number)
            {
                Number = number;
            }
        }
    }
}
