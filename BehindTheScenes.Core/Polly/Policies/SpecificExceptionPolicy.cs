using System;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.Core.Polly.Policies
{
    public class SpecificExceptionPolicy<TException> : IRetryPolicy where TException : Exception
    {
        private readonly TimeSpan[] _genericRetryDelays =
        {
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(10)
        };

        public SpecificExceptionPolicy()
        {
            GetPolicy = Policy.Handle<TException>()
                              .Or<Exception>()
                              .WaitAndRetry(_genericRetryDelays);
        }

        public RetryPolicy GetPolicy { get; }

        public void ExecuteAction(Action action) => GetPolicy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => GetPolicy.Execute(func);
    }
}