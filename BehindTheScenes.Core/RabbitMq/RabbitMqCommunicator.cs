using System;

using Newtonsoft.Json;

using RabbitMQ.Client.Events;

namespace BehindTheScenes.Core.RabbitMq
{
    public interface IRabbitMqCommunicator
    {
        void Receive(Action<BasicDeliverEventArgs> deliveryAction);
        void Send<T>(T message);
    }

    public class RabbitMqCommunicator : IRabbitMqCommunicator
    {
        private readonly IRabbitMqChannelOperator _channelOperator;

        public RabbitMqCommunicator(IRabbitMqChannelOperator channelOperator)
        {
            _channelOperator = channelOperator;
        }

        public void Receive(Action<BasicDeliverEventArgs> action)
        {
            _channelOperator.ProcessMessage(action);
        }

        public void Send<T>(T message)
        {
            var serialisedMessage = JsonConvert.SerializeObject(message);
            _channelOperator.PublishMessage(serialisedMessage);
        }
    }
}