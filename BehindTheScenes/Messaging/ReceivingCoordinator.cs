using BehindTheScenes.Core.RabbitMq;
using BehindTheScenes.Messaging.Processors;

namespace BehindTheScenes.Messaging
{
    public interface IReceivingCoordinator
    {
        void ActionMessage();
    }

    public class ReceivingCoordinator : IReceivingCoordinator
    {
        private readonly IRabbitMqChannelOperator _channelOperator;
        private readonly IMessageProcessor _processor;

        public ReceivingCoordinator(IRabbitMqChannelOperator channelOperator, IMessageProcessor processor)
        {
            _channelOperator = channelOperator;
            _processor = processor;
        }

        public void ActionMessage() => _channelOperator.ProcessMessage(ea => _processor.ProcessMessage(ea));
    }
}