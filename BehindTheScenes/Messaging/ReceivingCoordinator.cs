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
        private readonly IRabbitMqCommunicator _communicator;
        private readonly IMessageProcessor _processor;

        public ReceivingCoordinator(IRabbitMqCommunicator communicator, IMessageProcessor processor)
        {
            _communicator = communicator;
            _processor = processor;
        }

        public void ActionMessage() => _communicator.Receive(ea => _processor.ProcessMessage(ea));
    }
}