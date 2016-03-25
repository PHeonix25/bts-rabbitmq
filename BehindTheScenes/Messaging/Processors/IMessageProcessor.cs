using System;

using RabbitMQ.Client.Events;

namespace BehindTheScenes.Messaging.Processors
{
    public interface IMessageProcessor
    {
        bool ProcessMessage(BasicDeliverEventArgs message);
    }
}