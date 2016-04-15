using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BehindTheScenes.Messaging;

using Microsoft.Practices.Unity;

namespace BehindTheScenes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (_, a) =>
            {
                var exception = a.ExceptionObject as Exception;

                if(exception != null)
                    Trace.Fail(exception.Message);
                else
                    Trace.Fail(string.Join(Environment.NewLine, args));
            };

            var container = new UnityContainer().AddNewExtension<Bootstrapper>();

            Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() => container.Resolve<ISendingCoordinator>().SendMany(5));
                Task.Factory.StartNew(() => container.Resolve<IReceivingCoordinator>().ActionMessage());
            }).Wait();

            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}