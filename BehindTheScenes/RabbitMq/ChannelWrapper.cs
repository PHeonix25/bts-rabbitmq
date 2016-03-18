using System;
using System.Text;

using BehindTheScenes.MessageTypes;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BehindTheScenes.RabbitMq
{
    public interface IChannelWrapper<TResponseType>
    {
        void SubscribeToQueue(string queueName, EventHandler<BasicDeliverEventArgs> messageCallback);
        Response<TResponseType> GetMessage(string queueName);
        void PublishToQueue(string routingKey, string message);
        void Acknowledge(string queueName, ulong messageId);
    }

    public class RabbitMqWrapper : IChannelWrapper<string>
    {
        private const string SENT_MESSAGE_CONTENT_TYPE = "application/json";
        private const byte SENT_MESSAGE_MODE_PERSISTENT = 2;
        private const bool SHOULD_ACK = false;
        private readonly IQueueManager _queueManager;
        

        public RabbitMqWrapper(IQueueManager queueManager)
        {
            _queueManager = queueManager;
        }

        public void SubscribeToQueue(string queueName, EventHandler<BasicDeliverEventArgs> messageCallback)
        {
            var channel = _queueManager.GetReceivingChannel(queueName);

            var eventConsumer = new EventingBasicConsumer(channel);
            eventConsumer.Received += messageCallback;

            channel.BasicConsume(queueName, SHOULD_ACK, eventConsumer);
        }

        public Response<string> GetMessage(string queueName)
        {
            Response<string> response = null;

            var channel = _queueManager.GetReceivingChannel(queueName);

            var result = channel.BasicGet(queueName, SHOULD_ACK);

            if(result != null)
            {
                var content = Encoding.UTF8.GetString(result.Body);

                response = new Response<string>(result.DeliveryTag, content);
            }

            return response;
        }

        public void PublishToQueue(string routingKey, string message)
        {
            using(var channel = _queueManager.CreateSendingChannel())
            {
                var properties = channel.CreateBasicProperties();
                properties.ContentType = SENT_MESSAGE_CONTENT_TYPE;
                properties.DeliveryMode = SENT_MESSAGE_MODE_PERSISTENT;

                var binaryBody = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(_queueManager.ExchangeName, routingKey, properties, binaryBody);
            }
        }

        public void Acknowledge(string queueName, ulong messageId)
        {
            var channel = _queueManager.GetReceivingChannel(queueName);

            channel?.BasicAck(messageId, false);
        }
    }
}