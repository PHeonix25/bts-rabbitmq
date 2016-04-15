using System.Text;

using BehindTheScenes.Extensions;

using RabbitMQ.Client.Events;

namespace BehindTheScenes.Messaging.Processors
{
    public interface IMessageProcessor
    {
        void ProcessMessage(BasicDeliverEventArgs message);
    }

    public class GenericMessageProcessor : IMessageProcessor
    {
        public void ProcessMessage(BasicDeliverEventArgs message)
            => Encoding.UTF8.GetString(message.Body).PrintNiceMessage("RECV");
    }
}