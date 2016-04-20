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
        private readonly RetryPolicy _policy;

        public SpecificExceptionPolicy()
        {
            _policy = Policy.Handle<TException>()
                            .Or<Exception>()
                            .WaitAndRetry(_genericRetryDelays);
        }

        public void ExecuteAction(Action action) => _policy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => _policy.Execute(func);
    }
}