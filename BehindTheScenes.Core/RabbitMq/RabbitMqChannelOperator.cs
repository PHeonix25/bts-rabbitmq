using System;
using System.Diagnostics;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BehindTheScenes.Core.RabbitMq
{
    public interface IRabbitMqChannelOperator
    {
        void ProcessMessage(Action<BasicDeliverEventArgs> deliveryAction);
        void PublishMessage(string message);
    }

    public class RabbitMqChannelOperator : IRabbitMqChannelOperator
    {
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMqChannelOperator(IRabbitMqFactory factory, string queueName)
        {
            _channel = factory.StartConnection().CreateModel();
            _queueName = queueName;

            var result = _channel.QueueDeclare(queueName, false, false, false, null);
            Trace.WriteLine($"Queue OK: '{result.QueueName}' | " +
                            $"Consumers: {result.ConsumerCount} | " +
                            $"Messages: {result.MessageCount}");
        }

        public void ProcessMessage(Action<BasicDeliverEventArgs> deliveryAction)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (_, ea) => deliveryAction(ea);
            _channel.BasicConsume(_queueName, true, consumer);
        }

        public void PublishMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(string.Empty, _queueName, null, bytes);
        }
    }
}