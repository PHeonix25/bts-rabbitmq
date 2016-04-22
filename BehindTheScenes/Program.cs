using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

using BehindTheScenes.Extensions;
using BehindTheScenes.WebRequester;

using Microsoft.Practices.Unity;

namespace BehindTheScenes
{
    internal static class Program
    {
        private static readonly ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.FirstChanceException += (_, firstChance)
                => firstChance.Exception.LogToConsole();

            var container = new UnityContainer().AddNewExtension<Bootstrapper>();

            Action[] actions =
            {
                () => { Console.WriteLine("Hello Behind the Scenes!"); },
                () => Console.WriteLine(container.Resolve<IWebRequester>().MakeRequest()),
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