using System;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.Polly.Policies
{
    public class SpecificPolicy<TException> : IPolicy where TException : Exception
    {
        private readonly RetryPolicy _policy;

        public SpecificPolicy()
        {
            _policy = Policy.Handle<TException>()
                            .Or<Exception>()
                            .WaitAndRetry(GenericPolicy.GenericRetryDelays);
        }

        public void ExecuteAction(Action action) => _policy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => _policy.Execute(func);
    }
}