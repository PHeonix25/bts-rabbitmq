using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;

namespace BehindTheScenes.RabbitMq
{
    public interface IQueueManager : IDisposable
    {
        string ExchangeName { get; }
        IModel GetReceivingChannel(string queueName);
        IModel CreateSendingChannel();
    }

    public class QueueManager : IQueueManager
    {
        private readonly IConnection _connection;
        private readonly IDictionary<string, IModel> _channelMap = new Dictionary<string, IModel>();

        public QueueManager(string exchangeName, IConnection connection)
        {
            ExchangeName = exchangeName;
            _connection = connection;
        }

        public string ExchangeName { get; }

        public IModel CreateSendingChannel() => _connection.CreateModel();

        public IModel GetReceivingChannel(string queueName)
        {
            IModel channel;

            if (_channelMap.ContainsKey(queueName))
                channel = _channelMap[queueName];
            else
            {
                channel = _connection.CreateModel();
                _channelMap.Add(queueName, channel);
            }

            return channel;
        }

        public void Dispose()
        {
            foreach (var channel in _channelMap.Values)
                channel.Dispose();

            _connection.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
