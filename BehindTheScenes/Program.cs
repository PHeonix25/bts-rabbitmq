using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using BehindTheScenes.Core.RabbitMq;
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
                    Trace.Fail(exception.Message, exception.ToString());
                else
                    Trace.Fail(string.Join(Environment.NewLine, args));
            };

            var container = new UnityContainer().AddNewExtension<Bootstrapper>();

            Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(container.Resolve<IReceivingCoordinator>().ActionMessage);
                Task.Factory.StartNew(() =>
                {
                    var communicator = container.Resolve<IRabbitMqCommunicator>();
                    foreach(var message in Enumerable.Range(1, 500)
                                                     .Select(
                                                         i => $"Hello World! Message {i}. Sent at [{DateTime.UtcNow}]"))
                    {
                        communicator.Send(message);
                        Console.WriteLine($" SENT \t`{message}`");
                    }
                });
            }).Wait();

            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}