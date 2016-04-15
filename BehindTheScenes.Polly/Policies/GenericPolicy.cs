using System;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.Polly.Policies
{
    public class GenericPolicy : IPolicy
    {
        public static readonly TimeSpan[] GenericRetryDelays =
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(3)
        };

        private readonly RetryPolicy _policy;

        public GenericPolicy()
        {
            _policy = Policy.Handle<Exception>()
                            .WaitAndRetry(GenericRetryDelays);
        }

        public void ExecuteAction(Action action) => _policy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => _policy.Execute(func);
    }
}