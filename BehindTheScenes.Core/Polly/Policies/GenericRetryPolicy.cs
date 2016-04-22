using System;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.Core.Polly.Policies
{
    public class GenericRetryPolicy : IRetryPolicy
    {
        private readonly TimeSpan[] _genericRetryDelays =
        {
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(10)
        };

        private readonly RetryPolicy _policy;

        public GenericRetryPolicy()
        {
            _policy = Policy.Handle<Exception>()
                            .WaitAndRetry(_genericRetryDelays);
        }

        public RetryPolicy GetPolicy => _policy;

        public void ExecuteAction(Action action) => _policy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => _policy.Execute(func);
    }
}