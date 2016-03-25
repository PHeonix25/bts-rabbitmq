using System;

using RabbitMQ.Client;

namespace BehindTheScenes.Core.RabbitMq
{
    public interface IRabbitMqFactory
    {
        IConnection StartConnection();
    }

    public class RabbitMqFactory : IDisposable, IRabbitMqFactory
    {
        private readonly ConnectionFactory _factory;
        private IConnection _connection;

        public RabbitMqFactory(string hostName)
        {
            _factory = new ConnectionFactory {HostName = hostName};
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IConnection StartConnection() => _connection ?? (_connection = _factory.CreateConnection());
    }
}