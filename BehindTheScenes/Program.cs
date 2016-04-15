﻿using System;
using System.Threading.Tasks;

using BehindTheScenes.Extensions;
using BehindTheScenes.Messaging;

using Microsoft.Practices.Unity;

namespace BehindTheScenes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.FirstChanceException += (_, firstChance)
                => firstChance.Exception.LogToConsole();

            AppDomain.CurrentDomain.UnhandledException += (_, unhandled) =>
                (unhandled.ExceptionObject is AggregateException
                    ? ((AggregateException)unhandled.ExceptionObject).InnerException
                    : (Exception)unhandled.ExceptionObject)
                    .LogToConsole();

            var container = new UnityContainer().AddNewExtension<Bootstrapper>();

            Action[] actions =
            {
                () => { throw new Exception("This exception is unhandled."); },
                () => { Console.WriteLine("Hello World!"); },
                () => container.Resolve<IReceivingCoordinator>().ActionMessage(),
                () => container.Resolve<ISendingCoordinator>().SendMany(5)
            };

            Parallel.ForEach(actions,
                new ParallelOptions {MaxDegreeOfParallelism = actions.Length},
                a => Task.Factory.StartNew(a));

            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}