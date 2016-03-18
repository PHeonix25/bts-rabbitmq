using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindTheScenes.MessageTypes
{
    public enum ResponseStatus
    {
        Ok,
        EmptyQueue,
        MalformedContent
    }

    public class Response<T>
    {
        public ulong MessageId { get; }
        public T Content { get;  }
        public ResponseStatus Status { get; }

        public bool IsDataAvailable => Status == ResponseStatus.Ok;

        public Response(ulong messageId, T content)
        {
            MessageId = messageId;
            Content = content;
            Status = ResponseStatus.Ok;
        }

        public Response(ulong messageId, ResponseStatus status)
        {
            MessageId = messageId;
            Content = default(T);
            Status = status;
        }
    }
}
