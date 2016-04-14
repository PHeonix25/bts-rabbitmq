using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using BehindTheScenes.Core.RabbitMq;
using BehindTheScenes.Extensions;
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
                Task.Factory.StartNew(container.Resolve<IReceivingCoordinator>().ActionMessage);
                Task.Factory.StartNew(() =>
                {
                    var communicator = container.Resolve<IRabbitMqChannelOperator>();
                    foreach(var message in Enumerable.Range(1, 50)
                                                     .Select(
                                                         i =>
                                                             $"Hello World! Message {i} at {DateTime.UtcNow.SignificantTicks()}")
                        )
                    {
                        communicator.PublishMessage(message);
                        message.PrintNiceMessage("SENT");
                    }
                });
            }).Wait();
        }
    }
}