using System;
using System.Text;

using RabbitMQ.Client.Events;

namespace BehindTheScenes.Messaging.Processors
{
    public class GenericMessageProcessor : IMessageProcessor
    {
        public bool ProcessMessage(BasicDeliverEventArgs message)
        {
            Console.WriteLine($" RECV \t{Encoding.UTF8.GetString(message.Body)}");
            return true;
        }
    }
}