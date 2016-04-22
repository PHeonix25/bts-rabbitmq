using System;

using Polly.Retry;

namespace BehindTheScenes.Core.Polly.Policies
{
    public interface IRetryPolicy
    {
        RetryPolicy GetPolicy { get; }
        void ExecuteAction(Action action);
        T ExecuteFunc<T>(Func<T> func);
    }
}