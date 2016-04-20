using BehindTheScenes.Core.RabbitMq;
using BehindTheScenes.Messaging;
using BehindTheScenes.Messaging.Processors;
using BehindTheScenes.WebRequester;

using Microsoft.Practices.Unity;

using static System.Configuration.ConfigurationManager;

namespace BehindTheScenes
{
    public class Bootstrapper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IWebRequester, SimpleWebRequester>(
                new InjectionConstructor("http://google.nl"));

            Container.RegisterType<IRabbitMqFactory, RabbitMqFactory>(
                new InjectionConstructor(AppSettings["RabbitMqHostname"]));

            Container.RegisterType<IRabbitMqChannelOperator, RabbitMqChannelOperator>(
                new InjectionConstructor(typeof(IRabbitMqFactory), AppSettings["RabbitMq_QueueName"]));

            Container.RegisterType<IRabbitMqSendingCoordinator, RabbitMqSendingCoordinator>();

            Container.RegisterType<IMessageProcessor, GenericMessageProcessor>();
            Container.RegisterType<IRabbitMqReceivingCoordinator, RabbitMqReceivingCoordinator>();
        }
    }
}