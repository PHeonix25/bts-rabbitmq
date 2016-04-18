using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BehindTheScenes
{
    internal static class Program
    {
        private static readonly ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();
        private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private static void Main(string[] args)
        {
            var cancellationToken = _cancellationTokenSource.Token;

            Action[] actions =
            {
                () => { Console.WriteLine("Hello Behind the Scenes!"); }
            };

            Parallel.ForEach(actions,
                new ParallelOptions {MaxDegreeOfParallelism = actions.Length},
                action => _tasks.Add(Task.Factory.StartNew(action, cancellationToken)));

            Console.WriteLine("Press [Enter] to request cancellation.");
            Console.ReadLine();

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            Console.WriteLine("All tasks have completed, press [Enter] one last time.");
            Console.ReadLine();
        }
    }
}