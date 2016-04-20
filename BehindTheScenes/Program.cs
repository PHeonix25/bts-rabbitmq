using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using BehindTheScenes.Extensions;

namespace BehindTheScenes
{
    internal static class Program
    {
        private static readonly ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();

        private static void Main(string[] args)
        {
            Action[] actions =
            {
                () => { Console.WriteLine("Hello Behind the Scenes!"); }
            };

            Parallel.ForEach(actions,
                new ParallelOptions {MaxDegreeOfParallelism = actions.Length},
                action => _tasks.Add(Task.Factory.StartNew(action)));

            Console.ReadLine();

            _tasks.WaitForAndPrintStatusOfAllTasks();

            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}