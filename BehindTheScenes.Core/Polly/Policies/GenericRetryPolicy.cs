using System;
using System.Diagnostics;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.Core.Polly.Policies
{
    public class GenericRetryPolicy : IRetryPolicy
    {
        private readonly RetryPolicy _policy;

        public GenericRetryPolicy()
        {
            _policy = Policy.Handle<Exception>()
                            .WaitAndRetry(RetryConstants.GenericRetryDelays);
        }

        public void ExecuteAction(Action action) => _policy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => _policy.Execute(func);
    }
}