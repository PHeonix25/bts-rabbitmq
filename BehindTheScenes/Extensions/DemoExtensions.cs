using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using BehindTheScenes.WebRequester;

namespace BehindTheScenes.Extensions
{
    public static class DemoExtensions
    {
        public static void LogToConsole(this Exception exception, string prefix = "")
            => Console.WriteLine($">> {prefix} Exception caught: {exception.GetType()}: {exception.Message}");

        public static string SignificantTicks(this DateTime currentDateTime)
            => new string(currentDateTime.Ticks.ToString()
                                         .Reverse().Take(4)
                                         .Reverse().ToArray());

        public static void PrintNiceMessage(this string message, string direction)
            => Console.WriteLine(
                $"{direction} \t\"{message}\" at " +
                $"{DateTime.UtcNow.SignificantTicks().PadLeft(4, '0')}");

        public static void PrintResultsOfManyRequests(this IWebRequester requester, int count)
        {
            foreach(var num in Enumerable.Range(0, count))
            {
                Console.WriteLine($"Request {num} - {requester.MakeRequest()}");
                Thread.Sleep(500);
            }
        }

        public static void WaitForAndPrintStatusOfAllTasks(this ConcurrentBag<Task> tasks)
        {
            try
            {
                Task.WaitAll(tasks.ToArray());
                Console.WriteLine("All tasks have completed successfully.");
            }
            catch(AggregateException e)
            {
                Console.WriteLine("\nAggregateException thrown with the following inner exceptions:");
                foreach(var v in e.InnerExceptions)
                {
                    var exception = v as TaskCanceledException;
                    if(exception != null)
                        Console.WriteLine($"   TaskCanceledException: Task {exception.Task.Id}");
                    else
                        Console.WriteLine($"   {v.GetType().Name}: {v.Message}");
                }
                Console.WriteLine();

                foreach(var task in tasks.OrderBy(t => t.Id))
                    Console.WriteLine($"  Task {task.Id} completed with status {task.Status}");
            }
            Console.WriteLine();
        }
    }
}