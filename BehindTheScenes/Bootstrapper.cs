using System.ComponentModel;

using BehindTheScenes.Core.RabbitMq;
using BehindTheScenes.Messaging;
using BehindTheScenes.Messaging.Processors;

using Coolblue.Utils.Json;

using Microsoft.Practices.Unity;

using static System.Configuration.ConfigurationManager;

namespace BehindTheScenes
{
    public class Bootstrapper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IJsonSerializer, JsonSerializer>();

            Container.RegisterType<IRabbitMqFactory, RabbitMqFactory>(
                new InjectionConstructor(AppSettings["RabbitMqHostname"]));

            Container.RegisterType<IRabbitMqChannelOperator, RabbitMqChannelOperator>(
                new InjectionConstructor(typeof(IRabbitMqFactory), AppSettings["RabbitMq_QueueName"]));

            Container.RegisterType<IRabbitMqCommunicator, RabbitMqCommunicator>();

            Container.RegisterType<IMessageProcessor, GenericMessageProcessor>();
            Container.RegisterType<IReceivingCoordinator, ReceivingCoordinator>();
        }
    }
}