using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;

namespace BehindTheScenes.RabbitMq
{
    public interface IChannelFactory : IDisposable
    {
        IModel GetChannel();
    }

    public class RabbitMqChannelFactory : IChannelFactory
    {
        private readonly IConnection _connection;

        public RabbitMqChannelFactory(IConnection connection)
        {
            _connection = connection;
        }

        public IModel GetChannel()
        {
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
