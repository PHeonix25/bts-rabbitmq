using BehindTheScenes.Core.RabbitMq;
using BehindTheScenes.Messaging.Processors;

namespace BehindTheScenes.Messaging
{
    public interface IRabbitMqReceivingCoordinator
    {
        void ActionMessage();
    }

    public class RabbitMqReceivingCoordinator : IRabbitMqReceivingCoordinator
    {
        private readonly IRabbitMqChannelOperator _channelOperator;
        private readonly IMessageProcessor _processor;

        public RabbitMqReceivingCoordinator(IRabbitMqChannelOperator channelOperator, IMessageProcessor processor)
        {
            _channelOperator = channelOperator;
            _processor = processor;
        }

        public void ActionMessage() => _channelOperator.ProcessMessage(ea => _processor.ProcessMessage(ea));
    }
}