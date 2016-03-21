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
        private readonly IChannelFactory _channelFactory;
        private readonly IDictionary<string, IModel> _channelMap = new Dictionary<string, IModel>();

        public QueueManager(string exchangeName, IChannelFactory channelFactory)
        {
            ExchangeName = exchangeName;
            _channelFactory = channelFactory;
        }

        public string ExchangeName { get; }

        public IModel CreateSendingChannel() => _channelFactory.GetChannel();

        public IModel GetReceivingChannel(string queueName)
        {
            IModel channel;

            if (_channelMap.ContainsKey(queueName))
                channel = _channelMap[queueName];
            else
            {
                channel = _channelFactory.GetChannel();
                _channelMap.Add(queueName, channel);
            }

            return channel;
        }

        public void Dispose()
        {
            foreach (var channel in _channelMap.Values)
                channel.Dispose();

            _channelFactory.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
