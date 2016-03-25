using System;
using System.Threading;

using Coolblue.Utils.Json;

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
        private readonly IJsonSerializer _jsonSerializer;

        public RabbitMqCommunicator(IRabbitMqChannelOperator channelOperator, IJsonSerializer jsonSerializer)
        {
            _channelOperator = channelOperator;
            _jsonSerializer = jsonSerializer;
        }

        public void Receive(Action<BasicDeliverEventArgs> action)
        {
            _channelOperator.ProcessMessage(action);
        }

        public void Send<T>(T message)
        {
            var serialisedMessage = _jsonSerializer.Serialize(message);
            _channelOperator.PublishMessage(serialisedMessage);
        }
    }
}