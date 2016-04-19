using System;
using System.Diagnostics;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.Core.Polly.Policies
{
    public class SpecificForeverPolicy<TException> : IRetryPolicy where TException : Exception
    {
        private readonly RetryPolicy _policy;

        public SpecificForeverPolicy()
        {
            _policy = Policy.Handle<TException>()
                            .Or<Exception>()
                            .WaitAndRetryForever(
                                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                (exception, timespan) =>
                                    Trace.WriteLine("Retrying operation due to exception of type " +
                                        $"'{exception.GetType()}' after {timespan.Seconds} seconds."));
        }

        public void ExecuteAction(Action action) => _policy.Execute(action);

        public T ExecuteFunc<T>(Func<T> func) => _policy.Execute(func);
    }
}