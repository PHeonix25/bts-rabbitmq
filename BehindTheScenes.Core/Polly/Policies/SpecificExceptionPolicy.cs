using System;
using System.Diagnostics;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.Core.Polly.Policies
{
    public class SpecificExceptionPolicy<TException> : IRetryPolicy where TException : Exception
    {
        private readonly RetryPolicy _policy;

        public SpecificExceptionPolicy()
        {
            _policy = Policy.Handle<TException>()
                            .Or<Exception>()
                            .WaitAndRetry(RetryConstants.GenericRetryDelays);
        }

        public void ExecuteAction(Action action) => _policy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => _policy.Execute(func);
    }
}